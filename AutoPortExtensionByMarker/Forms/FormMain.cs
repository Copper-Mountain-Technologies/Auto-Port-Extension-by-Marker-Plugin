// Copyright ©2015-2016 Copper Mountain Technologies
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR
// ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using CopperMountainTech;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoPortExtensionByMarker
{
    public partial class FormMain : Form
    {
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private enum ComConnectionStateEnum
        {
            INITIALIZED,
            NOT_CONNECTED,
            CONNECTED_VNA_NOT_READY,
            CONNECTED_VNA_READY
        }

        private ComConnectionStateEnum previousComConnectionState = ComConnectionStateEnum.INITIALIZED;
        private ComConnectionStateEnum comConnectionState = ComConnectionStateEnum.NOT_CONNECTED;

        private int selectedChannel = -1;
        private bool doesSelectedChannelHaveActiveMarker = false;

        private bool isMeasuring = false;

        private enum MeasureTypeEnum
        {
            SHORT,
            OPEN
        }

        private MeasureTypeEnum measureType = MeasureTypeEnum.SHORT;

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        public FormMain()
        {
            InitializeComponent();

            // --------------------------------------------------------------------------------------------------------

            // set form icon
            Icon = Properties.Resources.app_icon;

            // set form title
            Text = Program.programName;

            // disable resizing the window
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            // position the plug-in in the lower right corner of the screen
            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new Point(workingArea.Right - Size.Width - 130,
                                 workingArea.Bottom - Size.Height - 50);

            // always display on top
            TopMost = true;

            // --------------------------------------------------------------------------------------------------------

            // disable ui
            panelMain.Enabled = false;

            // set version label text
            toolStripStatusLabelVersion.Text = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

            // init port radio buttons
            radioButtonPort1.Checked = true;
            if ((Program.vna.family == VnaFamilyEnum.R) ||
                (Program.vna.family == VnaFamilyEnum.TR))
            {
                radioButtonPort2.Enabled = false;
                radioButtonPorts1And2.Enabled = false;
            }

            // init include loss check box
            checkBoxCompensateForLoss.Checked = true;

            // init user message label
            labelUserMessage.Visible = false;
            labelUserMessage.Text = "";

            // disable ui
            enableUi(false);

            // --------------------------------------------------------------------------------------------------------

            // start the ready timer
            readyTimer.Interval = 250; // 250 ms interval
            readyTimer.Enabled = true;
            readyTimer.Start();

            // start the update timer
            updateTimer.Interval = 250; // 250 ms interval
            updateTimer.Enabled = true;
            updateTimer.Start();

            // --------------------------------------------------------------------------------------------------------
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //
        // Timers
        //
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void readyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // is vna ready?
                if (Program.vna.app.Ready)
                {
                    // yes... vna is ready
                    comConnectionState = ComConnectionStateEnum.CONNECTED_VNA_READY;
                }
                else
                {
                    // no... vna is not ready
                    comConnectionState = ComConnectionStateEnum.CONNECTED_VNA_NOT_READY;
                }
            }
            catch (COMException)
            {
                // com connection has been lost
                comConnectionState = ComConnectionStateEnum.NOT_CONNECTED;
                Application.Exit();
                return;
            }

            if (comConnectionState != previousComConnectionState)
            {
                previousComConnectionState = comConnectionState;

                switch (comConnectionState)
                {
                    default:
                    case ComConnectionStateEnum.NOT_CONNECTED:

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = Color.White;
                        toolStripStatusLabelVnaInfo.BackColor = Color.Red;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = "VNA NOT CONNECTED";

                        // disable ui
                        panelMain.Enabled = false;

                        break;

                    case ComConnectionStateEnum.CONNECTED_VNA_NOT_READY:

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = Color.White;
                        toolStripStatusLabelVnaInfo.BackColor = Color.Red;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = "VNA NOT READY";

                        // disable ui
                        panelMain.Enabled = false;

                        break;

                    case ComConnectionStateEnum.CONNECTED_VNA_READY:

                        // get vna info
                        Program.vna.PopulateInfo(Program.vna.app.NAME);

                        // update vna info text box
                        toolStripStatusLabelVnaInfo.ForeColor = SystemColors.ControlText;
                        toolStripStatusLabelVnaInfo.BackColor = SystemColors.Control;
                        toolStripStatusLabelSpacer.BackColor = toolStripStatusLabelVnaInfo.BackColor;
                        toolStripStatusLabelVnaInfo.Text = Program.vna.modelString + "   " + "SN:" + Program.vna.serialNumberString + "   " + Program.vna.versionString;

                        // enable ui
                        panelMain.Enabled = true;

                        break;
                }
            }

            // update ui enabled state
            enableUi((comConnectionState == ComConnectionStateEnum.CONNECTED_VNA_READY) && !isMeasuring);

            // update user message
            updateUserMessage();
        }

        // ------------------------------------------------------------------------------------------------------------

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (comConnectionState == ComConnectionStateEnum.CONNECTED_VNA_READY)
            {
                // update the channel combo box
                if (comboBoxChannel.DroppedDown == false)
                {
                    updateChanComboBox();
                }
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void enableUi(bool enabled)
        {
            labelChannel.Enabled = enabled;
            comboBoxChannel.Enabled = enabled;

            groupBoxPortsToExtend.Enabled = enabled;

            checkBoxCompensateForLoss.Enabled = enabled;

            buttonMeasureShort.Enabled = (enabled && doesSelectedChannelHaveActiveMarker);
            buttonMeasureOpen.Enabled = (enabled && doesSelectedChannelHaveActiveMarker);
        }

        private void updateUserMessage()
        {
            if (comConnectionState == ComConnectionStateEnum.CONNECTED_VNA_READY)
            {
                if (doesSelectedChannelHaveActiveMarker == false)
                {
                    labelUserMessage.Text = "Please Add an Active Marker\nto the Selected Channel";
                    labelUserMessage.BackColor = SystemColors.Control;
                    labelUserMessage.Visible = true;
                }
                else
                {
                    labelUserMessage.Text = "";
                    labelUserMessage.Visible = false;
                }
            }
            else
            {
                labelUserMessage.Text = "";
                labelUserMessage.Visible = false;
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //
        // Channel
        //
        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void updateChanComboBox()
        {
            // save previously selected channel index
            int selectedChannelIndex = comboBoxChannel.SelectedIndex;

            // prevent combo box from flickering when update occurs
            comboBoxChannel.BeginUpdate();

            // clear channel selection combo box
            comboBoxChannel.Items.Clear();

            long splitIndex = 0;
            long activeChannel = 0;
            try
            {
                // get the split index (needed to determine number of channels)
                splitIndex = Program.vna.app.SCPI.DISPlay.SPLit;

                // determine the active channel
                activeChannel = Program.vna.app.SCPI.SERVice.CHANnel.ACTive;
            }
            catch (COMException)
            {
            }

            // determine number of channels from the split index
            int numOfChannels = Program.vna.DetermineNumberOfChannels(splitIndex);

            // populate the channel number combo box
            for (int ch = 1; ch < numOfChannels + 1; ch++)
            {
                comboBoxChannel.Items.Add(ch.ToString());
            }

            if ((selectedChannelIndex == -1) ||
                (selectedChannelIndex >= comboBoxChannel.Items.Count))
            {
                // init channel selection to the active channel
                comboBoxChannel.Text = activeChannel.ToString();
                selectedChannel = comboBoxChannel.SelectedIndex + 1;
            }
            else
            {
                // restore previous channel selection
                comboBoxChannel.SelectedIndex = selectedChannelIndex;
            }

            // prevent combo box from flickering when update occurs
            comboBoxChannel.EndUpdate();

            // check to see if selected channel has an active marker
            doesSelectedChannelHaveActiveMarker = checkChannelForActiveMarker(selectedChannel);
        }

        private void chanComboBox_SelectedIndexChanged(object sender, EventArgs args)
        {
            // has channel selection changed?
            if (selectedChannel != comboBoxChannel.SelectedIndex + 1)
            {
                // yes... update selected channel
                selectedChannel = comboBoxChannel.SelectedIndex + 1;

                // check to see if selected channel has an active marker
                doesSelectedChannelHaveActiveMarker = checkChannelForActiveMarker(selectedChannel);
            }
        }

        private bool checkChannelForActiveMarker(int channel)
        {
            // get the active marker number for this channel
            long activeMarkerNumber = getActiveMarkerNumber(channel);

            // determine if there is an active marker
            if (activeMarkerNumber > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private long getActiveMarkerNumber(int channel)
        {
            long activeTrace = 0;
            long activeMarkerNumber = 0;
            try
            {
                // determine the active trace for this channel
                activeTrace = Program.vna.app.SCPI.SERVice.CHANnel[channel].TRACe.ACTive;

                // get the active marker number for this channel
                activeMarkerNumber = Program.vna.app.SCPI.SERVice.CHANnel(channel).TRACe(activeTrace).MARKer.ACTive;
                return activeMarkerNumber;
            }
            catch (COMException)
            {
                return activeMarkerNumber;
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void measureShortButton_Click(object sender, EventArgs e)
        {
            // set measure type
            measureType = MeasureTypeEnum.SHORT;

            // set measuring flag
            isMeasuring = true;

            // disable ui
            enableUi(false);

            // start background task
            measureBackgroundWorker.RunWorkerAsync();
        }

        private void measureOpenButton_Click(object sender, EventArgs e)
        {
            // set measure type
            measureType = MeasureTypeEnum.OPEN;

            // set measuring flag
            isMeasuring = true;

            // disable ui
            enableUi(false);

            // start background task
            measureBackgroundWorker.RunWorkerAsync();
        }

        private void measureBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // call measure method
            measure();
        }

        private void measureBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // clear measuring flag
            isMeasuring = false;

            // enable ui
            enableUi(true);

            // set active control to button just clicked
            // (otherwise active control always goes to measure open button for some strange reason)
            if (measureType == MeasureTypeEnum.SHORT)
            {
                ActiveControl = buttonMeasureShort;
            }
            else
            {
                ActiveControl = buttonMeasureOpen;
            }
        }

        private void measure()
        {
            // active marker number
            long activeMarkerNumber = getActiveMarkerNumber(selectedChannel);

            // --------------------------------------------------------------------------------------------------------

            // previous measurement parameter for trace 1
            string previousTrace1MeasParameter = "";

            // previous data format for trace 1
            string previousTrace1DataFormat = "";

            // previous trigger continuous mode
            string previousTriggerContinuousMode = "";

            // previous trigger source
            string previousTriggerSource = "";

            // --------------------------------------------------------------------------------------------------------

            try
            {
                object err;

                // make sure selected channel is active
                err = Program.vna.app.SCPI.DISPlay.WINDow[selectedChannel].ACTivate;

                // ----------------------------------------------------------------------------------------------------

                // cache the measurement parameter for trace 1
                previousTrace1MeasParameter = Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter[1].DEFine;

                // cache the data format for trace 1
                previousTrace1DataFormat = Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.FORMat;

                // ----------------------------------------------------------------------------------------------------

                // cache the trigger continuous mode
                bool isContinuous = Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous;
                previousTriggerContinuousMode = isContinuous.ToString();

                // cache the trigger source
                previousTriggerSource = Program.vna.app.SCPI.TRIGger.SEQuence.SOURce;

                // ----------------------------------------------------------------------------------------------------

                // turn off continuous trigger mode
                Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous = false;

                // set trigger source to bus
                Program.vna.app.SCPI.TRIGger.SEQuence.SOURce = "BUS";

                // ----------------------------------------------------------------------------------------------------

                // disable port extension for selected channel
                Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.STATe = false;

                // ----------------------------------------------------------------------------------------------------

                // is port 1 being extended?
                if ((radioButtonPort1.Checked == true) || (radioButtonPorts1And2.Checked == true))
                {
                    // yes...
                    ExtendPort(1, activeMarkerNumber, measureType, checkBoxCompensateForLoss.Checked);
                }
                else
                {
                    // be sure port extension delay is cleared for port 1
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[1].TIME = 0;

                    // be sure loss compensation is disabled for port 1
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[1].INCLude[1].STATe = false;
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[1].INCLude[2].STATe = false;
                }

                // ----------------------------------------------------------------------------------------------------

                // is port 2 being extended?
                if ((radioButtonPort2.Checked == true) || (radioButtonPorts1And2.Checked == true))
                {
                    // yes...
                    ExtendPort(2, activeMarkerNumber, measureType, checkBoxCompensateForLoss.Checked);
                }
                else
                {
                    // be sure port extension delay is cleared for port 2
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[2].TIME = 0;

                    // be sure loss compensation is disabled for port 2
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[2].INCLude[1].STATe = false;
                    Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[2].INCLude[2].STATe = false;
                }

                // ----------------------------------------------------------------------------------------------------

                // restore the previous measurement parameter for trace 1
                Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter[1].DEFine = previousTrace1MeasParameter;

                // restore the previous data format for trace 1
                Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.FORMat = previousTrace1DataFormat;

                // ----------------------------------------------------------------------------------------------------

                // restore the previous trigger source
                Program.vna.app.SCPI.TRIGger.SEQuence.SOURce = previousTriggerSource;

                // restore the trigger continuous mode
                Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous = Convert.ToBoolean(previousTriggerContinuousMode);

                // ----------------------------------------------------------------------------------------------------

                // enable port extension for selected channel
                Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.STATe = true;
            }
            catch (COMException e)
            {
                // attempt to restore the settings we modified
                if (string.IsNullOrEmpty(previousTrace1MeasParameter) == false)
                {
                    // restore the previous measurement parameter for trace 1
                    Program.vna.app.SCPI.CALCulate[selectedChannel].PARameter[1].DEFine = previousTrace1MeasParameter;
                }
                if (string.IsNullOrEmpty(previousTrace1DataFormat) == false)
                {
                    // restore the previous data format for trace 1
                    Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.FORMat = previousTrace1DataFormat;
                }
                if (string.IsNullOrEmpty(previousTriggerSource) == false)
                {
                    // restore the previous trigger source
                    Program.vna.app.SCPI.TRIGger.SEQuence.SOURce = previousTriggerSource;
                }
                if (string.IsNullOrEmpty(previousTriggerContinuousMode) == false)
                {
                    // restore the trigger continuous mode
                    Program.vna.app.SCPI.INITiate[selectedChannel].CONTinuous = Convert.ToBoolean(previousTriggerContinuousMode);
                }

                // display error message
                showMessageBoxForComException(e);
                return;
            }
        }

        private void ExtendPort(int portNumber, long activeMarkerNumber, MeasureTypeEnum measureType, bool includeLoss)
        {
            // validate active marker number
            if (activeMarkerNumber > 0)
            {
                object err;

                double[] markerData;
                int numberOfMarkers;

                double markerExtendedPhase;
                double markerExtendedPhaseFreq;

                double markerLogMagnitude;
                double markerLogMagnitudeFreq;

                try
                {
                    // set measurement parameter of trace 1
                    string parameter = "";
                    switch (portNumber)
                    {
                        default:
                        case 1:
                            parameter = "S11";
                            break;

                        case 2:
                            parameter = "S22";
                            break;
                    }
                    Program.vna.app.SCPI.Calculate[selectedChannel].Parameter[1].DEFine = parameter;

                    // ------------------------------------------------------------------------------------------------

                    // generate a single trigger and wait for completion
                    err = Program.vna.app.SCPI.INITiate[selectedChannel].IMMediate;
                    err = Program.vna.app.SCPI.TRIGger.SEQuence.SINGle;

                    // set trace 1 data format to expanded phase
                    Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.FORMat = "UPHase";

                    // get marker data
                    markerData = Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.MARKer.DATA;

                    // determine number of markers
                    numberOfMarkers = (int)markerData[0];

                    // validate number of markers
                    if (numberOfMarkers >= activeMarkerNumber)
                    {
                        // read frequency and delay values of active marker
                        markerExtendedPhaseFreq = markerData[(3 * (int)activeMarkerNumber) - 2];
                        markerExtendedPhase = markerData[(3 * (int)activeMarkerNumber) - 1];

                        // calculate the delay
                        double markerGroupDelay = 0.0;

                        // is short being measured?
                        if (measureType == MeasureTypeEnum.SHORT)
                        {
                            // yes...
                            markerGroupDelay = -0.5 * ((markerExtendedPhase - 180) / 360) * (1 / markerExtendedPhaseFreq);
                        }
                        else
                        {
                            // no...
                            markerGroupDelay = -0.5 * (markerExtendedPhase / 360) * (1 / markerExtendedPhaseFreq);
                        }

                        // set port extension delay
                        Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].TIME = markerGroupDelay;

                        // include loss?
                        if (checkBoxCompensateForLoss.Checked == true)
                        {
                            // yes...
                            // set trace 1 data format to logarithmic magnitude
                            Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.FORMat = "MLOGarithmic";

                            // read frequency and logarithmic magnitude values of active marker
                            markerData = Program.vna.app.SCPI.CALCulate[selectedChannel].SELected.MARKer.DATA;
                            markerLogMagnitudeFreq = markerData[(3 * (int)activeMarkerNumber) - 2];
                            markerLogMagnitude = markerData[(3 * (int)activeMarkerNumber) - 1];
                            markerLogMagnitude = -1 * (markerLogMagnitude / 2);

                            // set port frequency and loss to measured values for point 1
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].FREQuency[1] = markerLogMagnitudeFreq;
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].LOSS[1] = markerLogMagnitude;
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].INCLude[1].STATe = true;

                            // point 2 is not used
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].INCLude[2].STATe = false;
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].FREQuency[2] = 1000000000;
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].LOSS[2] = 0;
                        }
                        else
                        {
                            // be sure loss compensation is disabled for this port
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].INCLude[1].STATe = false;
                            Program.vna.app.SCPI.SENSe[selectedChannel].CORRection.EXTension.PORT[portNumber].INCLude[2].STATe = false;
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void showMessageBoxForComException(COMException e)
        {
            MessageBox.Show(Program.vna.GetUserMessageForComException(e),
                Program.programName,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    }
}