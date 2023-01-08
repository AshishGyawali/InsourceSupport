using InsourceData.Interface;
using Microsoft.AspNetCore.Mvc;

namespace InsourceSupport.Controllers
{
    public class LookupController : Controller
    {
        private readonly ILookupRepository repo;

        public LookupController(ILookupRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IActionResult> GetModulesbySoftwareId(int id) {
            return Ok(await repo.GetModuleList(id));
        }
    }
}
