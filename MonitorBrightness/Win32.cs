using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace MonitorProfiler.Win32
{
    public class NativeMethods
    {
        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
           MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, 
            ref NativeStructures.DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hmon, ref NativeStructures.MonitorInfoEx mi);
        
        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow", SetLastError = true)]
        public static extern IntPtr MonitorFromWindow([In] IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", EntryPoint = "MonitorFromPoint", SetLastError = true)]
        public static extern IntPtr MonitorFromPoint([In] Point pt, uint dwFlags);

        [DllImport("user32.dll", EntryPoint = "MonitorFromRect", SetLastError = true)]
        public static extern IntPtr MonitorFromRect(ref Rectangle lprect, uint dwFlags);

        [DllImport("dxva2.dll", EntryPoint = "GetNumberOfPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll", EntryPoint = "GetPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor, uint dwPhysicalMonitorArraySize, [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", EntryPoint = "DestroyPhysicalMonitors", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyPhysicalMonitors(
            uint dwPhysicalMonitorArraySize, [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", EntryPoint = "DestroyPhysicalMonitor", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyPhysicalMonitor(IntPtr hMonitor);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorTechnologyType", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorTechnologyType(
            IntPtr hMonitor, ref NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE pdtyDisplayTechnologyType);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorCapabilities", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorCapabilities(
            IntPtr hMonitor, ref uint pdwMonitorCapabilities, ref uint pdwSupportedColorTemperatures);

        [DllImport("dxva2.dll", EntryPoint = "SetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMonitorBrightness([In]IntPtr hMonitor, [In]uint pdwNewBrightness);
        
        [DllImport("dxva2.dll", EntryPoint = "GetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorBrightness(
            [In]IntPtr hMonitor, ref uint pdwMinimumBrightness, ref uint pdwCurrentBrightness, ref uint pdwMaximumBrightness);

        [DllImport("dxva2.dll", EntryPoint = "SetMonitorContrast", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMonitorContrast([In]IntPtr hMonitor, [In]uint pdwNewContrast);
        
        [DllImport("dxva2.dll", EntryPoint = "GetMonitorContrast", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorContrast(
            [In]IntPtr hMonitor, ref uint pdwMinimumContrast, ref uint pdwCurrentContrast, ref uint pdwMaximumContrast);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorColorTemperature", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorColorTemperature(
        [In]IntPtr hMonitor, ref NativeStructures.MC_COLOR_TEMPERATURE pctCurrentColorTemperature);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorRedGreenOrBlueDrive", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorRedGreenOrBlueDrive(
            [In]IntPtr hMonitor, [In]NativeStructures.MC_DRIVE_TYPE dtDriveType, 
            ref uint pdwMinimumDrive, ref uint pdwCurrentDrive, ref uint pdwMaximumDrive);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorRedGreenOrBlueGain", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorRedGreenOrBlueGain(
            [In]IntPtr hMonitor, [In]NativeStructures.MC_GAIN_TYPE dtDriveType, 
            ref uint pdwMinimumGain, ref uint pdwCurrentGain, ref uint pdwMaximumGain);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    
        public static void ScrollToBottom(System.Windows.Forms.TextBox txtBox)
        {
            // Scroll to the bottom, without moving the caret position.
            NativeMethods.SendMessage(txtBox.Handle, NativeConstants.WM_VSCROLL, (IntPtr) NativeConstants.SB_BOTTOM, IntPtr.Zero);
        }
    
    
    }
    
    public class NativeConstants
    {
        public const int MONITOR_DEFAULTTOPRIMARY = 1;
        public const int MONITOR_DEFAULTTONEAREST = 2;
        public const int MONITOR_DEFAULTTONULL = 0;
        
        public const int WM_VSCROLL = 0x115;
        public const int WM_HSCROLL = 0x114;

        public const int SB_LINEDOWN = 1;
        public const int SB_LINEUP = 0;
        public const int SB_TOP = 6;
        public const int SB_BOTTOM = 7;
        public const int SB_PAGEUP = 2;
        public const int SB_PAGEDOWN = 3;

    }

    public class NativeStructures
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szPhysicalMonitorDescription;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MonitorInfoEx
        {
            public int cbSize;
            public RECT rcMonitor; // Total area  
            public RECT rcWork; // Working area  
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szDeviceName; 
        }

        public enum MC_DISPLAY_TECHNOLOGY_TYPE
        {
            MC_SHADOW_MASK_CATHODE_RAY_TUBE,
            MC_APERTURE_GRILL_CATHODE_RAY_TUBE,
            MC_THIN_FILM_TRANSISTOR,
            MC_LIQUID_CRYSTAL_ON_SILICON,
            MC_PLASMA,
            MC_ORGANIC_LIGHT_EMITTING_DIODE,
            MC_ELECTROLUMINESCENT,
            MC_MICROELECTROMECHANICAL,
            MC_FIELD_EMISSION_DEVICE,
        }
        public enum MC_COLOR_TEMPERATURE
        {
            MC_COLOR_TEMPERATURE_UNKNOWN,
            MC_COLOR_TEMPERATURE_4000K,
            MC_COLOR_TEMPERATURE_5000K,
            MC_COLOR_TEMPERATURE_6500K,
            MC_COLOR_TEMPERATURE_7500K,
            MC_COLOR_TEMPERATURE_8200K,
            MC_COLOR_TEMPERATURE_9300K,
            MC_COLOR_TEMPERATURE_10000K,
            MC_COLOR_TEMPERATURE_11500K,
        }
        public enum MC_MONITOR_CAPABILITIES
        {
            MC_CAPS_NONE = 0x00000000,
            MC_CAPS_MONITOR_TECHNOLOGY_TYPE = 0x00000001,
            MC_CAPS_BRIGHTNESS = 0x00000002,
            MC_CAPS_CONTRAST = 0x00000004,
            MC_CAPS_COLOR_TEMPERATURE = 0x00000008,
            MC_CAPS_RED_GREEN_BLUE_GAIN = 0x00000010,
            MC_CAPS_RED_GREEN_BLUE_DRIVE = 0x00000020,
            MC_CAPS_DEGAUSS = 0x00000040,
            MC_CAPS_DISPLAY_AREA_POSITION = 0x00000080,
            MC_CAPS_DISPLAY_AREA_SIZE = 0x00000100,
            MC_CAPS_RESTORE_FACTORY_DEFAULTS = 0x00000400,
            MC_CAPS_RESTORE_FACTORY_COLOR_DEFAULTS = 0x00000800,
            MC_RESTORE_FACTORY_DEFAULTS_ENABLES_MONITOR_SETTINGS = 0x00001000
        }

        public enum MC_DRIVE_TYPE
        {
            MC_RED_DRIVE = 0,
            MC_GREEN_DRIVE = 1,
            MC_BLUE_DRIVE = 2
        }

        public enum MC_GAIN_TYPE
        {
            MC_RED_GAIN = 0,
            MC_GREEN_GAIN = 1,
            MC_BLUE_GAIN = 2
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x16,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }
    }

    /*public Window1() { InitializeComponent(); }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        WindowInteropHelper helper = new WindowInteropHelper(this);

        IntPtr hMonitor = NativeMethods.MonitorFromWindow(helper.Handle, NativeConstants.MONITOR_DEFAULTTOPRIMARY);
        int lastWin32Error = Marshal.GetLastWin32Error();

        uint pdwNumberOfPhysicalMonitors = 0u;
        bool numberOfPhysicalMonitorsFromHmonitor = NativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR(
            hMonitor, ref pdwNumberOfPhysicalMonitors);
        lastWin32Error = Marshal.GetLastWin32Error();

        NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray =
            new NativeStructures.PHYSICAL_MONITOR[pdwNumberOfPhysicalMonitors];
        bool physicalMonitorsFromHmonitor = NativeMethods.GetPhysicalMonitorsFromHMONITOR(
            hMonitor, pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
        lastWin32Error = Marshal.GetLastWin32Error();

        uint pdwMonitorCapabilities = 0u;
        uint pdwSupportedColorTemperatures = 0u;
        var monitorCapabilities = NativeMethods.GetMonitorCapabilities(
            pPhysicalMonitorArray[0].hPhysicalMonitor, ref pdwMonitorCapabilities, ref pdwSupportedColorTemperatures);
        lastWin32Error = Marshal.GetLastWin32Error();

        NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE type =
            NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE.MC_SHADOW_MASK_CATHODE_RAY_TUBE;
        var monitorTechnologyType = NativeMethods.GetMonitorTechnologyType(
            pPhysicalMonitorArray[0].hPhysicalMonitor, ref type);
        lastWin32Error = Marshal.GetLastWin32Error();

        var destroyPhysicalMonitors = NativeMethods.DestroyPhysicalMonitors(
            pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
        lastWin32Error = Marshal.GetLastWin32Error();

        this.lbl.Content = type;
    }*/
}