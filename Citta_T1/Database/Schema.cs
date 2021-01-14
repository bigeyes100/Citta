using System;
using System.Collections.Generic;
using System.Text;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace C2.Database
{
    public class Schema
    {
        public string Name;
        public OraConnection ParentConnection;

        public Schema(OraConnection Parent)
        {
            ParentConnection = Parent;
        }

        List<Table> _Tables = null;
        /// <summary>
        /// The list of tables for this schema
        /// </summary>
        public List<Table> Tables
        {
            get
            {
                /*
                 * 1. ����û������б�
                 * 2. ��ÿ�����������
                 */
                if (_Tables == null)
                {
                    // select distinct owner from sys.all_objects
                    using (OracleConnection conn = new OracleConnection(ParentConnection.ConnectionString))
                    {
                        string sql = String.Format(@"
                            select object_name, object_type
                            from sys.all_objects
                            where owner='{0}' and object_type in ('TABLE','VIEW')
                            order by object_name",
                          DbHelper.Sanitise(Name));
                        using (OracleCommand comm = new OracleCommand(sql, conn))
                        {
                            using (OracleDataReader rdr = comm.ExecuteReader())
                            {
                                _Tables = new List<Table>();
                                while (rdr.Read())
                                {
                                    //Table table = new Table(this);
                                    string table_name = rdr.GetString(0);
                                    Table table = new Table(this.Name, table_name);
                                    table.View = rdr.GetString(1) == "VIEW";
                                    _Tables.Add(table);
                                }
                            }
                        }
                    }
                }
                return _Tables;
            }
        }
    }
}
