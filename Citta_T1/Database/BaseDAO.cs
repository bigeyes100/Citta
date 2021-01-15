using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Database
{
    public abstract class BaseDAO
    {
        public abstract bool TestConn();
        public abstract string Query(string sql);
        public abstract string GenGetTableContentSQL(Table table, int maxNum);
        public abstract string GetTablesByUserSQL();
        public abstract string GetSchemaByTablesSQL(List<Table> tables);
        public abstract string GetTableContentSQL(Table table, int maxNum);
        public abstract string GetUserSQL();
    }
}
