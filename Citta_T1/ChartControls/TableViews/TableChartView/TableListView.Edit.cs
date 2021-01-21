using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.ChartControls.TableViews
{
    public partial class TableListView
    {
        public void AddTableItem(TableItem item)
        {
            item.Location = this._TableList.Count * 20;
            this._TableList.TableItems.Add(item);
        }
        public void RemoveTableItem(TableItem item)
        {
            this._TableList.TableItems.Remove(item);
        }
    }
}
