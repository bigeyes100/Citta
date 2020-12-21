﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Model
{
    public enum DatabaseType
    {
        Oracle,
        Hive
    }
    class DatabaseItem
    {

        public DatabaseType Type { set; get; }
        public string Server { set; get; }
        public string Service { set; get; }
        public string Port { set; get; }
        public string User { set; get; }
        public string Password { set; get; }
        public string Table { set; get; }

        public DatabaseItem()
        {

        }
    }
}