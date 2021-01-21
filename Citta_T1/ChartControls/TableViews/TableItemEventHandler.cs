using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.ChartControls.TableViews
{
    public delegate void TableItemEventHandler(object sender, TableItemEventArgs e);
    public class TableItemEventArgs : EventArgs
    {
        TableItem _TableItem;
        public TableItemEventArgs(TableItem item)
        {
            _TableItem = item;
        }
    }
}
