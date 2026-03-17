using FamilyApplication.DTOs;

namespace FamilyGroupApplication.Services
{
    public interface IFamilyGroupService
    {
        Task<IEnumerable<FamilyGroupDto>> GetAllFamilyGroupsAsync();
        Task<FamilyGroupDto> GetFamilyGroupByIdAsync(long id);
        Task<FamilyGroupDto> CreateFamilyGroupAsync(CreateFamilyGroupDto FamilyGroup, string FileName);
        Task<FamilyGroupDto> UpdateFamilyGroupAsync(long id, UpdateFamilyGroupDto FamilyGroup);
        Task<bool> DeleteFamilyGroupAsync(long id);
    }
}
