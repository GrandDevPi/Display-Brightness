using System;
using System.Collections.Generic;
using System.Text;
using MonitorProfiler.Win32;

namespace MonitorProfiler.Models.Display
{
    public struct MonitorFeature
    {
        public bool IsSupported;
        public uint Min, Max, Current, Original;
    }

    public enum FeatureType
    {
        Brightness,
        Contrast,
        RedDrive, GreenDrive, BlueDrive,
        RedGain, GreenGain, BlueGain
    }

    public class Monitor: IDisposable
    {
        #region Declarations
        public IntPtr HPhysicalMonitor { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Object Tag { get; set; }

        public MonitorFeature Brightness;
        #endregion
        
        public Monitor(NativeStructures.PHYSICAL_MONITOR physicalMonitor)
        {
            HPhysicalMonitor = physicalMonitor.hPhysicalMonitor;
            Name = physicalMonitor.szPhysicalMonitorDescription;

            Brightness.IsSupported = NativeMethods.GetMonitorBrightness(HPhysicalMonitor, ref Brightness.Min, ref Brightness.Current, ref Brightness.Max);
            Brightness.Original = Brightness.Current;
        }

        /// <summary>
        /// Disposes the monitor
        /// </summary>
        public void Dispose()
        {
            NativeMethods.DestroyPhysicalMonitor(HPhysicalMonitor);
        }

        /// <summary>
        /// Sets brightness to a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetBrightness(uint value)
        {            
            bool retVal =  NativeMethods.SetMonitorBrightness(HPhysicalMonitor, Brightness.Current);
            if (retVal)
                Brightness.Current = value;

            return retVal;
        }
        
        public void SetCurrentBrightnessAsOriginal()
        {
            Brightness.Original = Brightness.Current;
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
