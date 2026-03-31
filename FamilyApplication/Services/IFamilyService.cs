using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public interface IFamilyService
    {
        Task<IEnumerable<FamilyDto>> GetAllFamilysAsync();
        Task<FamilyDto> GetFamilyByIdAsync(long id);
        Task<FamilyDto> CreateFamilyAsync(CreateFamilyDto Family, string filepath);
        Task<FamilyDto> UpdateFamilyAsync(long id, UpdateFamilyDto Family);
        Task<bool> DeleteFamilyAsync(long id);
    }
}
