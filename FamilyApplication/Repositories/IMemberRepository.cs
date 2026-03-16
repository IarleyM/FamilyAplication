using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.Repositories
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(long id);
        Task<Member> CreateAsync(Member member);
        Task<Member> UpdateAsync(Member member);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
        Task<IEnumerable<Member>> GetByCategoryAsync(FamilyCategory category);
    }
}