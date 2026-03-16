using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.Repositories
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetAllAsync();
        Task<Family> GetByIdAsync(long id);
        Task<Family> CreateAsync(Family member);
        Task<Family> UpdateAsync(Family member);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}
