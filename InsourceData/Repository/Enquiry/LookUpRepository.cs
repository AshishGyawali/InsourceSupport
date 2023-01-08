using Dapper;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models;
using InsourceData.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InsourceData.Repository.Enquiry
{
    public class LookUpRepository : ILookupRepository
    {
        private readonly Database _db;
        public LookUpRepository(Database database)
        {
            _db = database;
        }
        public async Task<IEnumerable<KeyValueViewModel>> GetSoftwareList()
        {
            var query = "SELECT Id,Name FROM Software where IsActive = 1;";
            return await _db.ExecuteListAsync<KeyValueViewModel>(query, CommandType.Text); 
        }

        public async Task<IEnumerable<KeyValueViewModel>> GetModuleList(int softwareId)
        {
            var query = "SELECT Id,Name FROM Module where SoftwareId = @Id;";
            var pram = new DynamicParameters();
            pram.Add("Id",softwareId,DbType.Int32);
            return await _db.ExecuteListAsync<KeyValueViewModel>(query, CommandType.Text,pram);
        }
    }
}
