namespace MonitorProfiler
{
    partial class BrightnessControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrightnessControl));
            this.linkRevert = new System.Windows.Forms.LinkLabel();
            this.lblIconA = new System.Windows.Forms.Label();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.barBrightness = new System.Windows.Forms.TrackBar();
            this.lblMonitorName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // linkRevert
            // 
            this.linkRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRevert.AutoSize = true;
            this.linkRevert.Location = new System.Drawing.Point(216, 2);
            this.linkRevert.Name = "linkRevert";
            this.linkRevert.Size = new System.Drawing.Size(40, 13);
            this.linkRevert.TabIndex = 21;
            this.linkRevert.TabStop = true;
            this.linkRevert.Text = "Revert";
            this.linkRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // lblIconA
            // 
            this.lblIconA.AutoSize = true;
            this.lblIconA.Image = ((System.Drawing.Image)(resources.GetObject("lblIconA.Image")));
            this.lblIconA.Location = new System.Drawing.Point(10, 24);
            this.lblIconA.Name = "lblIconA";
            this.lblIconA.Size = new System.Drawing.Size(13, 13);
            this.lblIconA.TabIndex = 19;
            this.lblIconA.Text = "  ";
            // 
            // lblBrightness
            // 
            this.lblBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBrightness.Location = new System.Drawing.Point(218, 24);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(29, 23);
            this.lblBrightness.TabIndex = 18;
            this.lblBrightness.Text = "0%";
            // 
            // barBrightness
            // 
            this.barBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barBrightness.LargeChange = 10;
            this.barBrightness.Location = new System.Drawing.Point(24, 19);
            this.barBrightness.Maximum = 100;
            this.barBrightness.Name = "barBrightness";
            this.barBrightness.Size = new System.Drawing.Size(191, 45);
            this.barBrightness.TabIndex = 17;
            this.barBrightness.TickFrequency = 5;
            this.barBrightness.ValueChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            this.barBrightness.MouseCaptureChanged += new System.EventHandler(this.TrackBar_ValueChanged);
            // 
            // lblMonitorName
            // 
            this.lblMonitorName.AutoSize = true;
            this.lblMonitorName.Location = new System.Drawing.Point(10, 5);
            this.lblMonitorName.Name = "lblMonitorName";
            this.lblMonitorName.Size = new System.Drawing.Size(0, 13);
            this.lblMonitorName.TabIndex = 16;
            // 
            // BrightnessControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.linkRevert);
            this.Controls.Add(this.lblIconA);
            this.Controls.Add(this.lblBrightness);
            this.Controls.Add(this.barBrightness);
            this.Controls.Add(this.lblMonitorName);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BrightnessControl";
            this.Size = new System.Drawing.Size(258, 48);
            ((System.ComponentModel.ISupportInitialize)(this.barBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkRevert;
        private System.Windows.Forms.Label lblIconA;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.TrackBar barBrightness;
        private System.Windows.Forms.Label lblMonitorName;
    }
}
