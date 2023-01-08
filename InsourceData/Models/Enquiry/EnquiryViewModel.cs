using InsourceData.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InsourceData.Models.Enquiry
{
    public class EnquiryViewModel
    {
        [DisplayName("Software Name")]
        public int SoftwareId { get; set; }

        [DisplayName("Module Name")]
        public int ModuleId { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Fullname")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invaliid Email")]
        public string Email { get; set; }

        public string ContactNumber { get; set; }

        [Required]
        [MinLength(15, ErrorMessage ="Please specify the problem in atleast 15 letters.")]
        public string Issue { get; set; }

        public List<IFormFile> Files { get; set; }
        public string FilePath { get; set; }

        public IEnumerable<KeyValueViewModel> Softwares { get; set; }

    }

    public class EnquiryListViewModel
    {
        public int SoftwareId { get; set; }
        public int ModuleId { get; set; }
        public string SoftwareName { get; set; }
        public string ModuleName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Issue { get; set; }
        public string Files { get; set; }
    }
}
