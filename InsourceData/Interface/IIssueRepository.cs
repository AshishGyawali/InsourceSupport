using InsourceData.Models.Enquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Interface
{
    public interface IIssueRepository
    {
        public Task<IEnumerable<EnquiryListViewModel>> GetEnquiryList();
    }
}
