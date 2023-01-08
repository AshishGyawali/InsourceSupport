using InsourceData.DB;
using InsourceData.Models;
using InsourceData.Models.Enquiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Interface
{
    public interface IEnquiryRepository
    {
        public Task<DbResponse> SaveEnquiry(EnquiryViewModel enquiry);
    }
}
