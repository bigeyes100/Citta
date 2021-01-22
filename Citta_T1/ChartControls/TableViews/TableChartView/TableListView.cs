using C2.Controls.TableViews;
using C2.Core;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace C2.ChartControls.TableViews
{
    public partial class TableListView: TableChart
    {
        const int TableItemHeight = 10;
        const int TableItemWidth = 10;
        private List<Table> _TableList;
        private List<TableItem> _TableItemList;
        public ITableRender Render { get; private set; }

        public TableListView()
        {
            Render = new TableRender();

            ResetChartStyle();
            ScrollToCenter();
        }
        public List<Table> TableList
        {
            get { return this._TableList; }
            set
            {
                if (_TableList != value)
                {
                    List<Table> old = _TableList;
                    _TableList = value;
                    OnTableListChanged(old);
                }
            }

        }

        private void OnTableListChanged(List<Table> old)
        {
            // 当TableList改变时，new出新的TableItems
            UpdateTableItems(old);
            UpdateView(ChangeTypes.All);
        }

        private void UpdateTableItems(List<Table> old)
        {
            this._TableItemList.Clear();
            for (int i = 0; i < TableList.Count; i++)
            {
                Table table = TableList[i];
                Point loc = new Point(0, TableItemHeight * (1 + i));
                this._TableItemList.Add(new TableItem(table, loc));
            }
        }

        private void ResetChartStyle()
        {
            
        }
    }
}
