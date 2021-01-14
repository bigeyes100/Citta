using C2.Controls;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.ChartControls.TableViews
{
    public partial class TableChartView: TableChart
    {
        public event EventHandler TablesChanged;
        public ITableRender Render { get; private set; }
        private List<Table> _Tables;

        public TableChartView()
        {
            Render = new TableRender();

            ResetChartStyle();
            ScrollToCenter();
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

        private void ResetChartStyle()
        {
            
        }
        private void OnTablesChanged()
        {
            TablesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
