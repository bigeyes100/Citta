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
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(new Point((int)(-3), (int)(-3)), new Size((int)(10m), (int)(10))));
            Rectangle contentRect = new Rectangle(Point.Empty, new Size((int)(size.Width), (int)(size.Height)));
            contentRect.Inflate(-3, -3);
            e.Graphics.DrawRectangle(Pens.Blue, contentRect);
            Rectangle viewPortRect = new Rectangle(new Point((int)(viewPort.Location.X), (int)(viewPort.Location.Y)), new Size((int)(viewPort.Width), (int)(viewPort.Height)));
            viewPortRect.Inflate(-4, -4);
            e.Graphics.DrawRectangle(Pens.Green, viewPortRect);
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
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(new Point((int)(-3), (int)(-3)), new Size((int)(10), (int)(10))));
                contentRect = new Rectangle(Point.Empty, new Size((int)(size.Width), (int)(size.Height)));
                contentRect.Inflate(-5, -5);
                e.Graphics.DrawRectangle(Pens.Black, contentRect);
                viewPortRect = new Rectangle(new Point((int)(viewPort.Location.X), (int)(viewPort.Location.Y)), new Size((int)(viewPort.Width), (int)(viewPort.Height)));
                viewPortRect.Inflate(-6, -6);
                e.Graphics.DrawRectangle(Pens.Red, viewPortRect);

                TableRenderArgs args = new TableRenderArgs(Tables, e.Graphics, Font);
                Render.Paint(args);
            }
        }
    }
}
