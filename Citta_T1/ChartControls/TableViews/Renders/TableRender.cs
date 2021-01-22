using C2.Canvas;
using C2.ChartControls.Shapes;
using C2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using C2.ChartControls.FillTypes;

namespace C2.ChartControls.TableViews
{
    class TableRender : ITableRender
    {
        #region IRender Members
        public void Paint(List<TableItem> tis, TableRenderArgs e)
        {
            PaintTables(tis, e);
        }

        public void PaintTable(TableItem ti, TableRenderArgs e)
        {
            throw new NotImplementedException();
        }

        public void PaintTables(IEnumerable<TableItem> tis, TableRenderArgs e)
        {
            if (tis.IsNullOrEmpty())
                return;

            foreach (var ti in tis)
            {
                var pt = ti.Location;
                e.Graphics.TranslateTransform(pt.X, pt.Y);
                _PaintItem(ti, e);
                e.Graphics.TranslateTransform(-pt.X, -pt.Y);
            }
        }
        #endregion
        private void _PaintItem(TableItem ti, TableRenderArgs e)
        {
            // draw background
            PaintNodeBackground(ti, e);

            // calculate paint bounds
            var rect = new Rectangle(Point.Empty, ti.Bounds.Size); // 修改参照原点
        }

        private void PaintNodeBackground(TableItem ti, TableRenderArgs e)
        {
            var grf = e.Graphics;
            var rect = new Rectangle(Point.Empty, ti.Bounds.Size);


            using (Shape shaper = new RectangleShape())
            {
                // draw background
                Color backColor = Color.White;
                var ft = FillType.DefaultFillType;
                IBrush brushBack = ft.CreateBrush(e.Graphics, backColor, rect);

                shaper.Fill(e.Graphics, brushBack, rect);
            }

        }
    }
}
