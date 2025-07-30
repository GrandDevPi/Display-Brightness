using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MonitorProfiler
{
    public class OptionsControl : UserControl
    {
        public CheckBox StartupCheckBox;
        public Button RefreshButton;
        public Button ExitButton;

        public event EventHandler RefreshClicked;
        public event EventHandler ExitClicked;

        public OptionsControl()
        {
            InitializeComponent();
            StartupCheckBox.Checked = IsStartupEnabled();
            StartupCheckBox.CheckedChanged += StartupCheckBox_CheckedChanged;
            RefreshButton.Click += (s, e) => RefreshClicked?.Invoke(this, EventArgs.Empty);
            ExitButton.Click += (s, e) => ExitClicked?.Invoke(this, EventArgs.Empty);
        }

        private void StartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (StartupCheckBox.Checked)
                EnableStartup();
            else
                DisableStartup();
        }

        private bool IsStartupEnabled()
        {
            using (var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false))
            {
                return key?.GetValue(Application.ProductName) != null;
            }
        }

        private void EnableStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(Application.ProductName, Application.ExecutablePath);
            }
        }

        private void DisableStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(Application.ProductName, false);
            }
        }

        private void InitializeComponent()
        {
            this.StartupCheckBox = new CheckBox();
            this.RefreshButton = new Button();
            this.ExitButton = new Button();
            this.SuspendLayout();
            // 
            // StartupCheckBox
            // 
            this.StartupCheckBox.Text = "Start at login";
            this.StartupCheckBox.Location = new System.Drawing.Point(4, 4);
            this.StartupCheckBox.Size = new System.Drawing.Size(120, 24);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Location = new System.Drawing.Point(130, 4);
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            // 
            // ExitButton
            // 
            this.ExitButton.Text = "Exit";
            this.ExitButton.Location = new System.Drawing.Point(210, 4);
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            // 
            // OptionsControl
            // 
            this.Controls.Add(this.StartupCheckBox);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.ExitButton);
            this.Size = new System.Drawing.Size(300, 32);
            this.ResumeLayout(false);
        }
    }
}
