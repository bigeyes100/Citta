using C2.Canvas;
using C2.Canvas.GdiPlus;
using C2.Controls.TableViews;
using System.Collections.Generic;
using System.Drawing;

namespace C2.ChartControls.TableViews
{
    public class TableRenderArgs
    {
        public List<TableItem> TableItems { get; }
        public IGraphics Graphics { get; private set; }
        public TableChart Chart { get; private set; }
        public IFont Font { get; private set; }
        public TableRenderArgs(List<TableItem> tis, Graphics graphics, Font font)
        {
            TableItems = tis;
            Graphics = new GdiGraphics(graphics);
            Font = new GdiFont(font);
        }
    }
}
