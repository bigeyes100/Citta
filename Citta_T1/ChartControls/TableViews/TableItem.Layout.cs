using C2.Database;
using System.Drawing;

namespace C2.ChartControls.TableViews
{
    public partial class TableItem
    {
        public int? Width;
        public int? Height;
        Rectangle _Bounds;
        //const int MaxWidth = 10000;
        //const int MinWidth = 10;
        //const int MaxHeight = 10000;
        //const int MinHeight = 10;

        //Rectangle _TextBounds;


        public Rectangle TextBounds;
        public Rectangle PictureBounds;
        public Rectangle HelpBounds;
        public Rectangle Bounds;

        public Point Location 
        { 
            get { return this.Bounds.Location; } 
            set
            {
                Bounds = new Rectangle(value, Size);
            }
        }
        public Size Size
        {
            get { return Bounds.Size; }
            set
            {
                Bounds = new Rectangle(Location, value);
            }
        }
    }
}
