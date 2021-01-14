using C2.Canvas;
using C2.Canvas.GdiPlus;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.ChartControls.TableViews
{
    public class TableRenderArgs
    {
        public List<Table> Tables { get; }
        public IGraphics Graphics { get; private set; }
        public IFont Font { get; private set; }
        public TableRenderArgs(List<Table> tables, Graphics graphics, Font font)
        {
            Tables = tables;
            Graphics = new GdiGraphics(graphics);
            Font = new GdiFont(font);
        }
    }
}
