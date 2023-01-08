using Dapper;
using InsourceData.Context;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models;
using System.Data;

namespace InsourceData.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly Database _db;

        public CompanyRepository(Database database)
        {
            _db = database;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await _db.ExecuteListAsync<Company>("SELECT * FROM Companies", CommandType.Text);

        }
    }
}
