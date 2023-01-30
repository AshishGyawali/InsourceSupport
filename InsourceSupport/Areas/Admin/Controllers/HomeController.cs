using InsourceData.DB;
using InsourceData.Interface;
using InsourceData.Models.Enquiry;
using InsourceData.Models.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InsourceSupport.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IIssueRepository _issueRepo;
        private readonly IEnquiryRepository _enquiryRepo;
        private readonly ILookupRepository _lookupRepo;
        private readonly IAuthenticationRepository _authRepo;

        public HomeController(IIssueRepository issueRepo, IEnquiryRepository enquiryRepo, ILookupRepository lookupRepo, IAuthenticationRepository authRepo)
        {
            _issueRepo = issueRepo;
            _enquiryRepo = enquiryRepo;
            _lookupRepo = lookupRepo;
            _authRepo = authRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
             return View(userId);
        }

        public async Task<IActionResult> showEnquiryList(EnquiryViewBySearch enquiry)
        {            
            return Ok(await _issueRepo.GetEnquiryList(enquiry));
        }
        public async Task<IActionResult> ProcessIssue(int id)
        {
            var res = await _enquiryRepo.ProcessIssue(id);
            return Ok(res);
        }
        public async Task<IActionResult> RejectIssue(int id)
        {
            var res = await _enquiryRepo.RejectIssue(id);
            return Ok(res);
        }
        public async Task<IActionResult> getSoftwareList()
        {
            return Ok(await _lookupRepo.GetSoftwareList());
        }
        public async Task<IActionResult> getModuleList(int softwareId)
        {
            return Ok(await _lookupRepo.GetModuleList(softwareId));
        }
        public async Task<IActionResult> getStatusList()
        {
            return Ok(await _lookupRepo.GetStatusList());
        }
        
    }
}
