using System;
using System.Collections.Generic;
using System.Text;
using MonitorProfiler.Win32;

namespace MonitorProfiler.Models.Display
{
    public class MonitorList : List<Monitor>
    {
        public bool Add(IntPtr hMonitor)
        {

            uint monitorCount = 0;
            NativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, ref monitorCount);
            if (monitorCount <= 0)
                return false;

            var monitorArray = new NativeStructures.PHYSICAL_MONITOR[monitorCount];
            NativeMethods.GetPhysicalMonitorsFromHMONITOR(hMonitor, monitorCount, monitorArray);

            foreach (var physicalMonitor in monitorArray)
            {
                Monitor newMonitor = new Monitor(physicalMonitor);
                this.Add(newMonitor);
                newMonitor.Index = this.IndexOf(newMonitor);
            }
            return true;
        }
    }
}
