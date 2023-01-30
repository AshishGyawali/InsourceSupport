
using Dapper;
using InsourceData.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.DB
{
    public class Database
    {
        private readonly DapperContext _context;
        public Database( DapperContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<T>> ExecuteListAsync<T>(string query, CommandType cType = CommandType.StoredProcedure, DynamicParameters? parameters = null) where T:class 
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(query, parameters, commandType: cType);
        }

        public async Task<T?> ExecuteObject<T>(string query, CommandType cType = CommandType.StoredProcedure, DynamicParameters? parameters = null) where T:class
        {
            var data = await ExecuteListAsync<T>(query, cType, parameters);
            return data?.FirstOrDefault();
        }

        public async Task<DbResponse> ExecuteNonQueryAsync(string query,CommandType cType = CommandType.StoredProcedure, DynamicParameters? parameters = null)
        {
            DbResponse response = new DbResponse();
            try
            {
                using var connection = _context.CreateConnection();
                await connection.ExecuteAsync(query, parameters, commandType: cType);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.Message;
            }
            return(response);
        }

        public async Task<Object> ExecuteScalarAsync(string query, CommandType cType = CommandType.StoredProcedure, DynamicParameters? parameters = null)
        {
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync(query, parameters, commandType: cType);
        }

       
    }
}
