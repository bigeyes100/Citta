using C2.ChartControls.TablePanel;
using C2.Controls.MapViews;
using C2.Core;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls.C1.Left
{
    public abstract class TableChart : ScrollableControlBase
    {
        private ChartBox _ChartBox;
        private List<Table> _Tables;
        public TableChart()
        {
            ChartBox = CreateChartBox();
            ChartBox.AllowDrop = true;
            ChartBox.Bounds = DisplayRectangle;
            ChartBox.Paint += new PaintEventHandler(ChartBox_Paint);
            ChartBox.MouseDown += new MouseEventHandler(ChartBox_MouseDown);
            ChartBox.MouseUp += new MouseEventHandler(ChartBox_MouseUp);
            ChartBox.MouseMove += new MouseEventHandler(ChartBox_MouseMove);
            ChartBox.MouseLeave += new EventHandler(ChartBox_MouseLeave);
            ChartBox.MouseWheel += new MouseEventHandler(ChartBox_MouseWheel);
            ChartBox.KeyDown += new KeyEventHandler(ChartBox_KeyDown);
            ChartBox.KeyUp += new KeyEventHandler(ChartBox_KeyUp);
            ChartBox.KeyPress += new KeyPressEventHandler(ChartBox_KeyPress);
            ChartBox.DragDrop += new DragEventHandler(ChartBox_DragDrop);
            Controls.Add(ChartBox);

            SetPaintStyles();
            Render = new TableRender();
        }
        #region property
        Color _ChartBackColor = Color.White;
        Color _ChartForeColor = Color.Black;
        Point _TranslatePoint;
        bool _HighQualityRender = true;

        public event EventHandler TablesChanged;
        public ITableRender Render { get; private set; }
        protected Rectangle ViewPort
        {
            get
            {
                Rectangle rect = ChartBox.ClientRectangle;

                Point pt = Point.Empty;
                if (HorizontalScroll.Enabled)
                    pt.X = HorizontalScroll.Value;
                if (VerticalScroll.Enabled)
                    pt.Y = VerticalScroll.Value;

                if (Margin.Left != 0)
                    pt.X -= Margin.Left;
                if (Margin.Top != 0)
                    pt.Y -= Margin.Top;

                rect.Offset(pt.X, pt.Y);
                rect.Width -= Margin.Horizontal;
                rect.Height -= Margin.Vertical;

                return rect;
            }
        }
        public Point TranslatePoint
        {
            get { return _TranslatePoint; }
            protected set { _TranslatePoint = value; }
        }
        public virtual bool CustomDoubleBuffer
        {
            get { return false; }
        }
        protected ChartBox ChartBox
        {
            get { return _ChartBox; }
            private set { _ChartBox = value; }
        }
        public Color ChartBackColor
        {
            get { return _ChartBackColor; }
            set
            {
                if (_ChartBackColor != value)
                {
                    _ChartBackColor = value;
                }
            }
        }
        public Color ChartForeColor
        {
            get { return _ChartForeColor; }
            set
            {
                if (_ChartForeColor != value)
                {
                    _ChartForeColor = value;
                }
            }
        }
        public List<Table> Tables
        {
            get { return _Tables; }
            set
            {
                if (_Tables != value)
                {
                    _Tables = value;
                    OnTablesChanged();
                }
            }
        }
        #endregion
        #region 事件
        private void ChartBox_DragDrop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_KeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_MouseWheel(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_MouseLeave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_MouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_MouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChartBox_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region scroll value

        protected override void OnHScrollValueChanged()
        {
            base.OnHScrollValueChanged();
            this.InvalidateChart();
        }

        protected override void OnVScrollValueChanged()
        {
            base.OnVScrollValueChanged();
            this.InvalidateChart();
        }

        #endregion
        #region paint
        private Bitmap BmpBuffer;

        public Image DoubleBufferImage
        {
            get { return BmpBuffer; }
        }

        public virtual void InvalidateChart()
        {
            InvalidateChart(false);
        }

        public virtual void InvalidateChart(bool realTime)
        {
            if (CustomDoubleBuffer && !realTime)
                DisposeBmpBuffer();

            if (ChartBox != null)
                ChartBox.Invalidate();
        }

        public void InvalidateChart(Rectangle rect)
        {
            InvalidateChart(rect, false);
        }

        public virtual void InvalidateChart(Rectangle rect, bool realTime)
        {
            if (CustomDoubleBuffer && !realTime)
                DisposeBmpBuffer();

            if (ChartBox != null)
                ChartBox.Invalidate(rect);
        }

        public void InvalidateChart(Region region, bool realTime)
        {
            if (CustomDoubleBuffer && !realTime)
                DisposeBmpBuffer();

            if (ChartBox != null)
                ChartBox.Invalidate(region);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics grf = e.Graphics;
            grf.Clear(ChartBackColor);

            if (ShowBorder)
            {
                grf.DrawRectangle(new Pen(BorderColor), 0, 0, Width - 1, Height - 1);
            }
        }

        private void DisposeBmpBuffer()
        {
            if (BmpBuffer != null)
            {
                BmpBuffer.Dispose();
                BmpBuffer = null;
            }
        }

        public GraphicsState TranslateGraphics(Graphics graphics)
        {
            Point pt = Point.Empty;
            if (TranslatePoint.X > 0)
                pt.X = TranslatePoint.X;
            if (TranslatePoint.Y > 0)
                pt.Y = TranslatePoint.Y;
            if (HorizontalScroll.Enabled)
                pt.X -= HorizontalScroll.Value;
            if (VerticalScroll.Enabled)
                pt.Y -= VerticalScroll.Value;

            if (ChartBox.Margin.Left != 0)
                pt.X += ChartBox.Margin.Left;
            if (ChartBox.Margin.Top != 0)
                pt.Y += ChartBox.Margin.Top;

            GraphicsState gs = graphics.Save();
            graphics.TranslateTransform(pt.X, pt.Y);

            return gs;
        }

        void ChartBox_Paint(object sender, PaintEventArgs e)
        {
            Point pt = Point.Empty;
            if (HorizontalScroll.Enabled)
                pt.X = HorizontalScroll.Value;
            if (VerticalScroll.Enabled)
                pt.Y = VerticalScroll.Value;

            if (ChartBox.Margin.Left != 0)
                pt.X -= ChartBox.Margin.Left;
            if (ChartBox.Margin.Top != 0)
                pt.Y -= ChartBox.Margin.Top;

            if (!CustomDoubleBuffer || BmpBuffer == null)
            {
                ChartPaintEventArgs cpe;
                if (CustomDoubleBuffer)
                {
                    BmpBuffer = new Bitmap(ChartBox.Width, ChartBox.Height);
                    Graphics grf = Graphics.FromImage(BmpBuffer);
                    cpe = new ChartPaintEventArgs(grf, ChartBox.ClientRectangle);
                }
                else
                {
                    cpe = new ChartPaintEventArgs(e);
                }

                cpe.LogicViewPort = ChartBox.ClientRectangle;
                cpe.BackBrush = new SolidBrush(ChartBackColor);
                cpe.ForeBrush = new SolidBrush(ChartForeColor);
                cpe.Font = ChartBox.DefaultChartFont;

                PaintHelper.SetHighQualityRender(cpe.Graphics);

                if (!pt.IsEmpty)
                {
                    cpe.Graphics.TranslateTransform(-pt.X, -pt.Y);
                    Rectangle rect = cpe.LogicViewPort;
                    rect.Offset(pt.X, pt.Y);
                    cpe.LogicViewPort = rect;
                }

                // Draw Chart
                DrawChart(cpe);

                if (CustomDoubleBuffer)
                {
                    cpe.Graphics.Dispose();
                }
            }

            if (CustomDoubleBuffer && BmpBuffer != null)
            {
                e.Graphics.DrawImage(BmpBuffer,
                    new Rectangle(0, 0, ChartBox.Width, ChartBox.Height),
                    0, 0, BmpBuffer.Width, BmpBuffer.Height, GraphicsUnit.Pixel);
            }

            if (CustomDoubleBuffer)
            {
                //e.Graphics.TranslateTransform(-pt.X, -pt.Y);
                //e.Graphics.ScaleTransform(Zoom, Zoom);
                PaintHelper.SetHighQualityRender(e.Graphics);
            }

            OnAfterPaint(e);
        }

        private void DrawChart(ChartPaintEventArgs e)
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
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(new Point((int)(-3 ), (int)(-3 )), new Size((int)(10 ), (int)(10 ))));
                contentRect = new Rectangle(Point.Empty, new Size((int)(size.Width ), (int)(size.Height )));
                contentRect.Inflate(-5, -5);
                e.Graphics.DrawRectangle(Pens.Black, contentRect);
                viewPortRect = new Rectangle(new Point((int)(viewPort.Location.X ), (int)(viewPort.Location.Y )), new Size((int)(viewPort.Width ), (int)(viewPort.Height )));
                viewPortRect.Inflate(-6, -6);
                e.Graphics.DrawRectangle(Pens.Red, viewPortRect);

                TableRenderArgs args = new TableRenderArgs(Tables, e.Graphics, Font);
                Render.Paint(args);
            }
        }

        protected virtual void OnAfterPaint(PaintEventArgs e)
        {
        }

        public override void UpdateView(ChangeTypes ut)
        {
            if ((ut & (ChangeTypes.Layout | ChangeTypes.Visual)) != ChangeTypes.None)
            {
                InvalidateChart();
            }

            base.UpdateView(ut);
        }
        #endregion
        #region methods
        private ChartBox CreateChartBox()
        {
            return new ChartBox();
        }

        private void OnTablesChanged()
        {
            TablesChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }

}
