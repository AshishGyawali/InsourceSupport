using Dapper;
using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models.Enquiry;
using InsourceData.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Repository.admin
{
    public class IssueRepository : IIssueRepository
    {
        private readonly Database _db;
        public IssueRepository(Database database)
        {
            _db = database;
        }
        public async Task<IEnumerable<EnquiryListViewModel>> GetEnquiryList(EnquiryViewBySearch enquiry)
        {
            var query = "getEnquiry_sp";
            var pram = new DynamicParameters();
            pram.Add("softwareId", enquiry.SoftwareId, DbType.Int32);
            pram.Add("moduleId", enquiry.ModuleId, DbType.Int32);
            pram.Add("statusId", enquiry.StatusId, DbType.Int32);
            pram.Add("dateFrom", enquiry.DateFrom, DbType.Date);
            pram.Add("dateTo", enquiry.DateTo, DbType.Date);
            return await _db.ExecuteListAsync<EnquiryListViewModel>(query, CommandType.StoredProcedure,pram);
        }
    }
}
