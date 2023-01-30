using Azure;
using InsourceData.Interface;
using InsourceData.Models;
using InsourceData.Models.Enquiry;
using InsourceData.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Reflection;
using InsourceData.DB;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Utility;
using Microsoft.AspNetCore.Http;

namespace InsourceSupport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ILookupRepository _lookupRepo;

        private readonly IEnquiryRepository _enquiryRepo;

        private readonly IIssueRepository _issueRepo;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(ILogger<HomeController> logger, ILookupRepository lookupRepo, IEnquiryRepository enquiryRepo, IIssueRepository issueRepo, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _lookupRepo = lookupRepo;
            _enquiryRepo = enquiryRepo;
            _issueRepo = issueRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> IndexAsync()
        {
            EnquiryViewModel model = new EnquiryViewModel();
            model.Softwares = await _lookupRepo.GetSoftwareList();
            return PartialView(model);
        }



        [HttpPost]
        public async Task<IActionResult> Create(EnquiryViewModel enquiry)
        {
            enquiry.Softwares = await _lookupRepo.GetSoftwareList();
            if (ModelState.IsValid)
            {
                if (enquiry.Files != null && enquiry.Files.Count > 0)
                {
                    List<string> files = new List<string>();
                    foreach (var file in enquiry.Files)
                    {

                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/IssuesScreenshot");

                        //create folder if not exist
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string fileNameWithPath = Path.Combine(path, fileName);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        files.Add(fileName);
                    }
                    enquiry.FilePath = string.Join(",", files);
                }
                var response = await _enquiryRepo.SaveEnquiry(enquiry);
                if (response.HasError == false)
                {
                    TempData["success"] = "Your issue has been registered and is being checked by our team. Please check your email for furtur information.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errorMessage = "Opps Something went wrong";
                    return PartialView("Index", enquiry);
                }
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            return BadRequest(new DbResponse() { HasError = true, Message = "Something went wrong" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            return PartialView();
        }

        //public IActionResult login()
        //{
        //    return PartialView();
        //}


    }
}