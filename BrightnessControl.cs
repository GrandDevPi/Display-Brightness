using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonitorProfiler.Models.Display;
using MonitorProfiler.Win32;

namespace MonitorProfiler
{
    public partial class BrightnessControl : UserControl
    {
        public BrightnessControl()
        {
            InitializeComponent();
            this.Font = System.Drawing.SystemFonts.DefaultFont;
            lblMonitorName.Font = new Font(System.Drawing.SystemFonts.DefaultFont, FontStyle.Bold);
        }
        private Models.Display.Monitor _currentMonitor;
        private bool IsIntialized = false;

        public Monitor Monitor
        {
            get { return this._currentMonitor; }
            set
            {
                _currentMonitor = value;
                this.lblMonitorName.Text = string.Format("{0}, Display {1}", _currentMonitor.Name, _currentMonitor.HPhysicalMonitor);

                barBrightness.Visible = _currentMonitor.Brightness.IsSupported;
                barBrightness.Minimum = (int)_currentMonitor.Brightness.Min;
                barBrightness.Maximum = (int)_currentMonitor.Brightness.Max;
                barBrightness.Value = (int)_currentMonitor.Brightness.Current;
                lblBrightness.Text = string.Format("{0}%", _currentMonitor.Brightness.Current);

                IsIntialized = true;
            }
        }

        public event EventHandler OnSetBrightnessFailed;

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {   // Assumes sender is a trackbar, and exists in the collection. Don't worry about errors :)
                if (!IsIntialized)
                    return;

                if (_currentMonitor.SetBrightness((uint)barBrightness.Value))
                    lblBrightness.Text = string.Format("{0}%", _currentMonitor.Brightness.Current);
                else
                // Report that brightness could not be changed
                {
                    OnSetBrightnessFailed?.Invoke(this, EventArgs.Empty);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            barBrightness.Value = (int)_currentMonitor.Brightness.Original;
        }


        /// <summary>
        /// Returns the name of this monitor
        /// </summary>
        public override string Text
        {
            get
            {
                return lblMonitorName.Text;
            }

            set
            {
                lblMonitorName.Text = value;
            }
        }
    }



}
