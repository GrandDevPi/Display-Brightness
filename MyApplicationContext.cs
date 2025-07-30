using MonitorBrightness;
using MonitorProfiler.Models.Display;
using MonitorProfiler.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MonitorProfiler
{
    class MyApplicationContext : ApplicationContext
    {
        //Component declarations
        private NotifyIcon TrayIcon;
        private ContextMenuStrip contextMenu;
        private string LogString = String.Empty;

        private readonly MonitorList monitorCollection = new MonitorList();


        public MyApplicationContext()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            TrayIcon.Visible = true;
        }

        // To be called by a delegate
        private bool MonitorEnum(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData)
        {
            monitorCollection.Add(hMonitor);
            return true;
        }

        private void InitializeComponent()
        {
            TrayIcon = new NotifyIcon();
            TrayIcon.Text = "Display Brightness";
            TrayIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            contextMenu = new ContextMenuStrip();
            contextMenu.Closed += TrayIconContextMenu_Closed;
            contextMenu.SuspendLayout();
            contextMenu.ShowCheckMargin = false;
            contextMenu.ShowImageMargin = false;

            // Add monitor controls
            AddMonitors();

            // Add a separator
            contextMenu.Items.Add(new ToolStripSeparator());

            // Add Options menu
            var optionsControl = new OptionsControl();
            optionsControl.RefreshClicked += (s, e) => RefreshMonitors();
            optionsControl.ExitClicked += (s, e) => Application.Exit();
            var optionsMenuItem = new ToolStripControlHost(optionsControl)
            {
                AutoSize = false,
                Size = optionsControl.Size
            };
            contextMenu.Items.Add(optionsMenuItem);

            contextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Removes all monitors, re-emumerates and adds monitors
        /// </summary>
        private void RefreshMonitors()
        {
            foreach (Monitor m in monitorCollection)
                if (m.Tag is ToolStripItem)
                    contextMenu.Items.Remove(m.Tag as ToolStripItem);

            AddMonitors();
        }

        private void AddMonitors()
        {
            // Dispose and remove any old monitors
            foreach (Monitor m in monitorCollection)
                m.Dispose();
            monitorCollection.Clear();

            var @delegate = new NativeMethods.MonitorEnumDelegate(MonitorEnum);
            NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, @delegate, IntPtr.Zero);
            Log("Number of physical monitors: {0}", monitorCollection.Count);

            foreach (Monitor monitor in monitorCollection)
            {
                Log("-----");
                Log(monitor.Name);
                Log("Index: {0}", monitor.Index);
                Log("Physical: {0}", monitor.HPhysicalMonitor);


                // Only add monitor if DDC is supported
                if (monitor.Brightness.IsSupported)
                {
                    BrightnessMenuItem newMenuItem = new BrightnessMenuItem();
                    newMenuItem.MonitorTrackBarControl.Monitor = monitor;
                    newMenuItem.MonitorTrackBarControl.OnSetBrightnessFailed += MonitorTrackBarControl_OnSetBrightnessFailed;
                    contextMenu.Items.Insert(0, newMenuItem);
                    monitor.Tag = newMenuItem;
                }
            }
        }

        /// <summary>
        /// Refresh sliders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonitorTrackBarControl_OnSetBrightnessFailed(object sender, EventArgs e)
        {
            RefreshMonitors();
        }




        /// <summary>
        /// Set the current monitor profile as default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrayIconContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            foreach (Monitor monitor in monitorCollection)
            {
                monitor.SetCurrentBrightnessAsOriginal();
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;
        }

        /// <summary>
        /// Destroy all monitors
        /// </summary>
        ~MyApplicationContext()
        {
            foreach (Monitor monitor in monitorCollection)
            {
                monitor.Dispose();
            }
        }

        /// Write to console        
        private void Log(object message, params object[] args)
        {
            this.LogString += string.Format(message.ToString(), args) + Environment.NewLine;

            //Console.WriteLine(Text);
        }
    }
}