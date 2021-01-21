using System.Collections.Generic;

namespace C2.ChartControls.TableViews
{
    public interface ITableRender
    {
        void Paint(TableList tl, TableRenderArgs e);
        void PaintTable(TableItem ti, TableRenderArgs e);
        void PaintTables(IEnumerable<TableItem> tis, TableRenderArgs e);
    }
}
