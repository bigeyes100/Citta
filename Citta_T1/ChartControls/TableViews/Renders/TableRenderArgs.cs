using C2.Canvas;
using C2.Canvas.GdiPlus;
using C2.Controls.TableViews;
using System.Collections.Generic;
using System.Drawing;

namespace C2.ChartControls.TableViews
{
    public class TableRenderArgs
    {
        public TableList TableList { get; }
        public IGraphics Graphics { get; private set; }
        public TableList Chart { get; private set; }
        public IFont Font { get; private set; }
        public TableRenderArgs(TableList tableList, Graphics graphics, Font font)
        {
            TableList = tableList;
            Graphics = new GdiGraphics(graphics);
            Font = new GdiFont(font);
        }
    }
}
