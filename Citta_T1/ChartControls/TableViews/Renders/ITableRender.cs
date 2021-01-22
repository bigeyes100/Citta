using System.Collections.Generic;

namespace C2.ChartControls.TableViews
{
    public interface ITableRender
    {
        void Paint(List<TableItem> tis, TableRenderArgs e);
        void PaintTable(TableItem ti, TableRenderArgs e);
    }
}
