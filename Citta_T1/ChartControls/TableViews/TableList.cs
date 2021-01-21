using C2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.ChartControls.TableViews
{
    public class TableList
    {
        private XList<TableItem> _TableItem;
        public XList<TableItem> TableItem { get { return _TableItem; } }

        public TableList()
        {
            _TableItem = new XList<TableItem>();
        }
        #region TableItem Events
        public event TableItemEventHandler TableItemAdded;
        public event TableItemEventHandler TableItemRemoved;
        #endregion
        #region event
        public void OnTableItemAdded(TableItem tableItem)
        {
            if (TableItemAdded != null)
            {
                TableItemAdded(this, new TableItemEventArgs(tableItem));
            }
        }
        public void OnTableItemRemoved(TableItem tableItem)
        {
            if (TableItemRemoved != null)
            {
                TableItemRemoved(this, new TableItemEventArgs(tableItem));
            }
        }
        #endregion
        #region
        public Color NodeBackColor = Color.White;
        #endregion
    }
}
