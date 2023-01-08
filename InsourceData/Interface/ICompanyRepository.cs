using InsourceData.Models;

namespace InsourceData.Interface
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
    }
}
