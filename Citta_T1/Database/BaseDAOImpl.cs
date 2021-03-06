﻿using C2.Core;
using C2.Model;
using C2.Utils;
using Hive2;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public class BaseDAOImpl : IDAO
    {
        #region 构造函数
    
        protected string Name, User, Pass, Host, Sid, Service, Port, Schema;
        public delegate void UpdateLog(string log);

        private static LogUtil log = LogUtil.GetInstance("BaseDAOImpl");
  
        protected struct QueryResult
        {
            public string content;
            public int returnNum;
        }
        protected BaseDAOImpl()
        {

        }
        public BaseDAOImpl(DatabaseItem dbi)
        {
            this.Name = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Sid = dbi.SID;
            this.Service = dbi.Service;
            this.Port = dbi.Port;
            this.Schema = dbi.Schema;
          
        }
        public BaseDAOImpl(DataItem item) : this(item.DBItem) { }
        public BaseDAOImpl(string name, string user, string pass, string host, string sid, string service, string port)
        {
            this.Name = name;
            this.User = user;
            this.Pass = pass;
            this.Host = host;
            this.Sid = sid;
            this.Service = service;
            this.Port = port;
        }
        public BaseDAOImpl Clone()
        {
            return new BaseDAOImpl
            {
                Name = this.Name,
                User = this.User,
                Pass = this.Pass,
                Host = this.Host,
                Sid = this.Sid,
                Service = this.Service,
                Port = this.Port
            };
        }
        #endregion
        #region 接口实现
        public virtual string Query(string sql, bool header = true)
        {
            throw new NotImplementedException();
        }
        public virtual bool TestConn()
        {
            return false;
        }
        #endregion
        #region 业务逻辑
        public List<string> GetUsers()
        {
            string result = this.Query(this.GetUserSQL(), false);
            return String.IsNullOrEmpty(result) ? new List<String>() : new List<string>(result.Split(OpUtil.DefaultLineSeparator));
        }
        public List<Table> GetTables(string schema)
        {
            List<Table> tables = new List<Table>();
            string result = this.Query(this.GetTablesSQL(schema), false);
            if (!String.IsNullOrEmpty(result))
                foreach (var line in result.Split(OpUtil.DefaultLineSeparator))
                {
                    if (!String.IsNullOrEmpty(line))
                        tables.Add(new Table(schema, line));
                }
            return tables;
        }
        public string GetTableContentString(Table table, int maxNum)
        {
            return this.Query(this.GetTableContentSQL(table, maxNum));
        }
        public List<List<string>> GetTableContent(Table table, int maxNum)
        {
            string result = this.GetTableContentString(table, maxNum);
            return String.IsNullOrEmpty(result) ? new List<List<string>>() : DbUtil.StringTo2DString(result);
        }
        public Dictionary<string, List<string>> GetColNameByTables(List<Table> tables)
        {
            string result = this.Query(this.GetColNameByTablesSQL(tables));
            return String.IsNullOrEmpty(result) ? new Dictionary<string, List<string>>() : DbUtil.StringToDict(result);
        }
        public string GetTableColumnNames(Table table)
        {
            return this.Query(this.GetColNameByTableSQL(table));
        }
        public void FillDGVWithTbSchema(DataGridView dataGridView, Table table)
        {
            string schemaString = this.GetTableColumnNames(table);
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
        }
        public void FillDGVWithTbContent(DataGridView dataGridView, Table table, int maxNum)
        {
            string contentString = this.GetTableContentString(table, maxNum);
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
        }
        public void FillDGVWithSQL(DataGridView dataGridView, string sql)
        {
            string contentString = this.Query(String.Format(this.LimitSQL(sql)));
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
        }


        public virtual bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            int pageIndex = 0;
            bool returnHeader = true;
            int totalRetuenNum = 0, subMaxNum;
            using (StreamWriter sw = new StreamWriter(outPutPath, false))
            {
                while (maxReturnNum == -1 ? true : totalRetuenNum < maxReturnNum)
                {
                    if (pageSize * pageIndex < maxReturnNum && pageSize * (pageIndex + 1) > maxReturnNum)
                        subMaxNum = maxReturnNum - pageIndex * pageSize;
                    else
                        subMaxNum = pageSize;
                    QueryResult contentAndNum = ExecuteSQL_Page(sqlText, pageSize, pageIndex, subMaxNum, returnHeader);

                    string result = contentAndNum.content;
                    totalRetuenNum += contentAndNum.returnNum;

                    if (returnHeader) 
                    {
                        if (String.IsNullOrEmpty(result))
                            return false;
                        returnHeader = false;
                    }
                    if (String.IsNullOrEmpty(result))
                        break;
                    sw.Write(result);
                    sw.Flush();
                    pageIndex += 1;     
                }
            }
            return true;
        }
        protected virtual QueryResult ExecuteSQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            return new QueryResult(); 
        }
        public virtual string DefaultSchema()
        {
            throw new NotImplementedException();
        }



        public bool TryOpen(IDisposable conn, int timeout, DatabaseType type)
        {
            Stopwatch sw = new Stopwatch();
            bool connectSuccess = false;
           
            Thread t = new Thread(delegate ()
            {          
                try
                {
                    sw.Start();
                    if (type == DatabaseType.Hive)
                        (conn as Connection).Open();
                    else
                        (conn as OracleConnection).Open();
                    connectSuccess = true;
                }
                catch (Exception ex)
                {
                    UpdateLog st= new UpdateLog(Action2Test);
                    st.Invoke(ex.ToString());
                }
            });

            t.IsBackground = true;
            try
            {
                t.Start();
                while (timeout > sw.ElapsedMilliseconds)
                    if (t.Join(1))
                        break;
            }
            catch(Exception ex)
            { 
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
            }
            return connectSuccess;
        }
        public void Action2Test(string ex)
        {
            log.ErrorFromDataBase(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex);
        }
        #endregion
        #region SQL
        public virtual string LimitSQL(string sql)
        {
            throw new NotImplementedException();
        }
        public virtual string GetUserSQL()
        {
            throw new NotImplementedException();
        }
        public virtual string GetTableContentSQL(Table table, int maxNum)
        {
            throw new NotImplementedException();
        }
        public virtual string GetTablesSQL(string schema)
        {
            throw new NotImplementedException();
        }
        public virtual string GetColNameByTablesSQL(List<Table> tables)
        {
            throw new NotImplementedException();
        }
        public virtual string GetColNameByTableSQL(Table table)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    class EmptyDAOImpl : BaseDAOImpl
    {
        public override bool TestConn()
        {
            return false;
        }
    }
}
