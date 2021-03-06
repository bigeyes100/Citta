﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Database
{
    public interface IDAO
    {
        bool TestConn();
        string Query(string sql, bool header=true);
        List<string> GetUsers();
        List<Table> GetTables(string schema);
        string GetTableContentString(Table table, int maxNum);
        List<List<string>> GetTableContent(Table table, int maxNum);
        Dictionary<string, List<string>> GetColNameByTables(List<Table> tables);
        string GetTableColumnNames(Table table);
        void FillDGVWithTbSchema(DataGridView dataGridView, Table table);
        void FillDGVWithTbContent(DataGridView dataGridView, Table table, int maxNum);
        void FillDGVWithSQL(DataGridView dataGridView, string SQL);
        bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000);
        string DefaultSchema();
    }
}
