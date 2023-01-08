using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsourceData.Models.ViewModel
{
    public class SoftwareViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string Shortcode { get; set; }

    }
}
