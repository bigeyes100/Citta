using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.ChartControls.TablePanel
{
    public interface ITableRender
    {
        void Paint(TableRenderArgs trArgs);
        void PaintTable();
        void PaintTables();
    }
}
