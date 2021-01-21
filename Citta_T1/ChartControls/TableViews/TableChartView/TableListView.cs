using C2.Controls.TableViews;
using C2.Core;
using System;
using System.Collections.Generic;

namespace C2.ChartControls.TableViews
{
    public partial class TableListView: TableChart
    {
        public event EventHandler TablesChanged;
        private TableList _TableList;
        public ITableRender Render { get; private set; }

        public TableListView(TableList tableList)
        {
            Render = new TableRender();
            _TableList = tableList;
            _TableList.TableItemAdded += new TableItemEventHandler(List_TableItemAdded);
            _TableList.TableItemAdded += new TableItemEventHandler(List_TableItemRemoved);

            ResetChartStyle();
            ScrollToCenter();
        }

        private void List_TableItemRemoved(object sender, TableItemEventArgs e)
        {
            UpdateView(ChangeTypes.All);
        }

        private void List_TableItemAdded(object sender, TableItemEventArgs e)
        {
            UpdateView(ChangeTypes.All);
        }

        private void ResetChartStyle()
        {
            
        }
        private void OnTablesChanged()
        {
            TablesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
