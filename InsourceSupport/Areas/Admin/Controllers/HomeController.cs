using InsourceData.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsourceSupport.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IIssueRepository _issueRepo;

        public HomeController(IIssueRepository issueRepo)
        {
            _issueRepo = issueRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> showEnquiryList()
        {
            var data = await _issueRepo.GetEnquiryList();
            return Ok(data);
        }
    }
}
