using InsourceData.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Models.Enquiry
{
    public class EnquiryViewBySearch
    {
        public int? SoftwareId { get; set; }
        public int? ModuleId { get; set; }
        public int? StatusId { get; set;}
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set;}

    }
}
