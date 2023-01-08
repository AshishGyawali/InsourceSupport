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
        public async Task<IEnumerable<EnquiryListViewModel>> GetEnquiryList()
        {
            var query = "SELECT s.[Name] as SoftwareName,m.[Name] as ModuleName,e.[UserName],e.[FullName],e.[Email],e.[ContactNumber],e.[Issue],e.[Files] FROM [Enquiry] e INNER JOIN [Software] s ON e.[SoftwareId]=s.[Id] INNER JOIN [Module] m ON e.[ModuleId]=m.[Id];";
            return await _db.ExecuteListAsync<EnquiryListViewModel>(query, CommandType.Text);
        }
    }
}
