using C2.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.ChartControls.TableViews
{
    partial class TableChartView
    {
        protected override void DrawChart(ChartPaintEventArgs e)
        {
            // transfter
            Rectangle viewPort = ViewPort; // 当前Chart的视野的大小
            Point ptTran = Point.Empty;
            SizeF size = ContentSize;// TODO new SizeF(ContentSize.Width * Zoom, ContentSize.Height * Zoom);  ContentSize
            if (!HorizontalScroll.Enabled && viewPort.Width > ContentSize.Width)
            {
                ptTran.X = (int)Math.Ceiling((viewPort.Width - size.Width) / 2);
            }
            if (!VerticalScroll.Enabled && viewPort.Height > ContentSize.Height)
            {
                ptTran.Y = (int)Math.Ceiling((viewPort.Height - size.Height) / 2);
            }

            if (!ptTran.IsEmpty)
            {
                e.Graphics.TranslateTransform(ptTran.X, ptTran.Y);
                TranslatePoint = ptTran;
            }
            else
            {
                TranslatePoint = Point.Empty;
            }

            if (Render != null && Tables != null)
            {
                TableRenderArgs args = new TableRenderArgs(Tables, e.Graphics, Font);
                Render.Paint(args);
            }
        }
    }
}
