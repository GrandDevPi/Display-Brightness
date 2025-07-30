using MonitorProfiler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MonitorBrightness
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip |
                                   ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class BrightnessMenuItem : ToolStripControlHost
    {
        public BrightnessControl MonitorTrackBarControl { get; set; }

        public BrightnessMenuItem() : base(new BrightnessControl())
        {
            this.MonitorTrackBarControl = this.Control as BrightnessControl;
        }


    }
}
