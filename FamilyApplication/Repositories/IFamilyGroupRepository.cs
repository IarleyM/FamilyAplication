using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.Repositories
{
    public interface IFamilyGroupRepository
    {
        Task<IEnumerable<FamilyGroup>> GetAllAsync();
        Task<FamilyGroup> GetByIdAsync(long id);
        Task<FamilyGroup> CreateAsync(FamilyGroup member);
        Task<FamilyGroup> UpdateAsync(FamilyGroup member);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}
