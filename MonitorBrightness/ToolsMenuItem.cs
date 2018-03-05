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
    public class ToolsMenuItem : ToolStripControlHost
    {
        public ToolsControl ToolsControl { get; set; }

        public ToolsMenuItem() : base(new ToolsControl())
        {
            this.ToolsControl = this.Control as ToolsControl;
        }


    }
}
