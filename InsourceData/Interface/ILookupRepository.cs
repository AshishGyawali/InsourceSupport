using InsourceData.Models.ViewModel;
using InsourceData.Models.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InsourceData.Interface
{
    public interface ILookupRepository
    {
        public Task<IEnumerable<KeyValueViewModel>> GetSoftwareList();

        public Task<IEnumerable<KeyValueViewModel>> GetModuleList(int softwareId);

        public Task<IEnumerable<KeyValueViewModel>> GetStatusList();
        public Task<IEnumerable<KeyValueViewModel>> GetRoleList();
    }
}
