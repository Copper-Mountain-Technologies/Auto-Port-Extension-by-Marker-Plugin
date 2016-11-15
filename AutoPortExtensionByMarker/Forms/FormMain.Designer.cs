namespace AutoPortExtensionByMarker
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonMeasureOpen = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.readyTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonMeasureShort = new System.Windows.Forms.Button();
            this.comboBoxChannel = new System.Windows.Forms.ComboBox();
            this.labelChannel = new System.Windows.Forms.Label();
            this.groupBoxPortsToExtend = new System.Windows.Forms.GroupBox();
            this.radioButtonPorts1And2 = new System.Windows.Forms.RadioButton();
            this.radioButtonPort2 = new System.Windows.Forms.RadioButton();
            this.radioButtonPort1 = new System.Windows.Forms.RadioButton();
            this.checkBoxCompensateForLoss = new System.Windows.Forms.CheckBox();
            this.measureBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelVnaInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelUserMessage = new System.Windows.Forms.Label();
            this.groupBoxPortsToExtend.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonMeasureOpen
            // 
            this.buttonMeasureOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMeasureOpen.Location = new System.Drawing.Point(145, 194);
            this.buttonMeasureOpen.Name = "buttonMeasureOpen";
            this.buttonMeasureOpen.Size = new System.Drawing.Size(127, 35);
            this.buttonMeasureOpen.TabIndex = 10;
            this.buttonMeasureOpen.Text = "Measure &Open";
            this.buttonMeasureOpen.UseVisualStyleBackColor = true;
            this.buttonMeasureOpen.Click += new System.EventHandler(this.measureOpenButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // readyTimer
            // 
            this.readyTimer.Interval = 1000;
            this.readyTimer.Tick += new System.EventHandler(this.readyTimer_Tick);
            // 
            // buttonMeasureShort
            // 
            this.buttonMeasureShort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMeasureShort.Location = new System.Drawing.Point(12, 194);
            this.buttonMeasureShort.Name = "buttonMeasureShort";
            this.buttonMeasureShort.Size = new System.Drawing.Size(127, 35);
            this.buttonMeasureShort.TabIndex = 9;
            this.buttonMeasureShort.Text = "Measure &Short";
            this.buttonMeasureShort.UseVisualStyleBackColor = true;
            this.buttonMeasureShort.Click += new System.EventHandler(this.measureShortButton_Click);
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannel.FormattingEnabled = true;
            this.comboBoxChannel.Location = new System.Drawing.Point(12, 25);
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Size = new System.Drawing.Size(260, 21);
            this.comboBoxChannel.TabIndex = 2;
            this.comboBoxChannel.SelectedIndexChanged += new System.EventHandler(this.chanComboBox_SelectedIndexChanged);
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(11, 9);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(49, 13);
            this.labelChannel.TabIndex = 1;
            this.labelChannel.Text = "&Channel:";
            // 
            // groupBoxPortsToExtend
            // 
            this.groupBoxPortsToExtend.Controls.Add(this.radioButtonPorts1And2);
            this.groupBoxPortsToExtend.Controls.Add(this.radioButtonPort2);
            this.groupBoxPortsToExtend.Controls.Add(this.radioButtonPort1);
            this.groupBoxPortsToExtend.Enabled = false;
            this.groupBoxPortsToExtend.Location = new System.Drawing.Point(12, 68);
            this.groupBoxPortsToExtend.Name = "groupBoxPortsToExtend";
            this.groupBoxPortsToExtend.Size = new System.Drawing.Size(260, 54);
            this.groupBoxPortsToExtend.TabIndex = 3;
            this.groupBoxPortsToExtend.TabStop = false;
            this.groupBoxPortsToExtend.Text = "Ports to &Extend ";
            // 
            // radioButtonPorts1And2
            // 
            this.radioButtonPorts1And2.AutoSize = true;
            this.radioButtonPorts1And2.Location = new System.Drawing.Point(169, 22);
            this.radioButtonPorts1And2.Name = "radioButtonPorts1And2";
            this.radioButtonPorts1And2.Size = new System.Drawing.Size(76, 17);
            this.radioButtonPorts1And2.TabIndex = 6;
            this.radioButtonPorts1And2.Text = "&Ports 1 && 2";
            this.radioButtonPorts1And2.UseVisualStyleBackColor = true;
            // 
            // radioButtonPort2
            // 
            this.radioButtonPort2.AutoSize = true;
            this.radioButtonPort2.Location = new System.Drawing.Point(92, 22);
            this.radioButtonPort2.Name = "radioButtonPort2";
            this.radioButtonPort2.Size = new System.Drawing.Size(53, 17);
            this.radioButtonPort2.TabIndex = 5;
            this.radioButtonPort2.Text = "Port &2";
            this.radioButtonPort2.UseVisualStyleBackColor = true;
            // 
            // radioButtonPort1
            // 
            this.radioButtonPort1.AutoSize = true;
            this.radioButtonPort1.Checked = true;
            this.radioButtonPort1.Location = new System.Drawing.Point(15, 22);
            this.radioButtonPort1.Name = "radioButtonPort1";
            this.radioButtonPort1.Size = new System.Drawing.Size(53, 17);
            this.radioButtonPort1.TabIndex = 4;
            this.radioButtonPort1.TabStop = true;
            this.radioButtonPort1.Text = "Port &1";
            this.radioButtonPort1.UseVisualStyleBackColor = true;
            // 
            // checkBoxCompensateForLoss
            // 
            this.checkBoxCompensateForLoss.AutoSize = true;
            this.checkBoxCompensateForLoss.Checked = true;
            this.checkBoxCompensateForLoss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCompensateForLoss.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.checkBoxCompensateForLoss.Location = new System.Drawing.Point(12, 132);
            this.checkBoxCompensateForLoss.Name = "checkBoxCompensateForLoss";
            this.checkBoxCompensateForLoss.Size = new System.Drawing.Size(125, 17);
            this.checkBoxCompensateForLoss.TabIndex = 7;
            this.checkBoxCompensateForLoss.Text = "Compensate for &Loss";
            this.checkBoxCompensateForLoss.UseVisualStyleBackColor = true;
            // 
            // measureBackgroundWorker
            // 
            this.measureBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.measureBackgroundWorker_DoWork);
            this.measureBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.measureBackgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelVnaInfo,
            this.toolStripStatusLabelSpacer,
            this.toolStripStatusLabelVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 240);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(284, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 28;
            // 
            // toolStripStatusLabelVnaInfo
            // 
            this.toolStripStatusLabelVnaInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelVnaInfo.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelVnaInfo.Name = "toolStripStatusLabelVnaInfo";
            this.toolStripStatusLabelVnaInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripStatusLabelVnaInfo.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabelVnaInfo.Text = "     ";
            // 
            // toolStripStatusLabelSpacer
            // 
            this.toolStripStatusLabelSpacer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelSpacer.Name = "toolStripStatusLabelSpacer";
            this.toolStripStatusLabelSpacer.Size = new System.Drawing.Size(206, 17);
            this.toolStripStatusLabelSpacer.Spring = true;
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabelVersion.ForeColor = System.Drawing.SystemColors.GrayText;
            this.toolStripStatusLabelVersion.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabelVersion.Text = "v ---";
            this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.labelUserMessage);
            this.panelMain.Controls.Add(this.comboBoxChannel);
            this.panelMain.Controls.Add(this.buttonMeasureOpen);
            this.panelMain.Controls.Add(this.buttonMeasureShort);
            this.panelMain.Controls.Add(this.groupBoxPortsToExtend);
            this.panelMain.Controls.Add(this.checkBoxCompensateForLoss);
            this.panelMain.Controls.Add(this.labelChannel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(284, 262);
            this.panelMain.TabIndex = 36;
            // 
            // labelUserMessage
            // 
            this.labelUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserMessage.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelUserMessage.Location = new System.Drawing.Point(12, 154);
            this.labelUserMessage.Name = "labelUserMessage";
            this.labelUserMessage.Size = new System.Drawing.Size(260, 37);
            this.labelUserMessage.TabIndex = 11;
            this.labelUserMessage.Text = "< user message goes here >";
            this.labelUserMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "< application title goes here >";
            this.groupBoxPortsToExtend.ResumeLayout(false);
            this.groupBoxPortsToExtend.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonMeasureOpen;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Timer readyTimer;
        private System.Windows.Forms.Button buttonMeasureShort;
        private System.Windows.Forms.ComboBox comboBoxChannel;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.GroupBox groupBoxPortsToExtend;
        private System.Windows.Forms.RadioButton radioButtonPorts1And2;
        private System.Windows.Forms.RadioButton radioButtonPort2;
        private System.Windows.Forms.RadioButton radioButtonPort1;
        private System.Windows.Forms.CheckBox checkBoxCompensateForLoss;
        private System.ComponentModel.BackgroundWorker measureBackgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVnaInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpacer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelUserMessage;
    }
}

