using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.DB
{
    public static class DatabaseHelper
    {
        public static DynamicParameters AddParam(this DynamicParameters container, string key, string value)
        {
            container.Add(key, value, DbType.String);
            return container;
        }
        public static DynamicParameters AddParam(this DynamicParameters container, string key, int value)
        {
            container.Add(key, value, DbType.Int32);
            return container;
        }
        public static DynamicParameters AddParam(this DynamicParameters container, string key, bool value)
        {
            container.Add(key, value, DbType.Boolean);
            return container;
        }
        public static DynamicParameters AddParam(this DynamicParameters container, string key, DateTime value)
        {
            container.Add(key, value, DbType.DateTime);
            return container;
        }
    }
}
