using FamilyApplication.DTOs;
using FamilyApplication.Models;
using FamilyApplication.Repositories;
using FamilyGroupApplication.Repositories;

namespace FamilyGroupApplication.Services
{
    public class FamilyGroupService : IFamilyGroupService
    {
        private readonly IFamilyGroupRepository _repository;

        public FamilyGroupService(IFamilyGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FamilyGroupDto>> GetAllFamilyGroupsAsync()
        {
            var FamilyGroups = await _repository.GetAllAsync();
            return FamilyGroups.Select(m => MapToDto(m));
        }

        public async Task<FamilyGroupDto> GetFamilyGroupByIdAsync(long id)
        {
            var FamilyGroup = await _repository.GetByIdAsync(id);
            return FamilyGroup != null ? MapToDto(FamilyGroup) : null;
        }

        public async Task<FamilyGroupDto> CreateFamilyGroupAsync(CreateFamilyGroupDto createDto, string fileName)
        {
            var FamilyGroup = new FamilyGroup
            {
                FamilyGroupName = createDto.FamilyGroupName,
                Photo = fileName
            };

            var created = await _repository.CreateAsync(FamilyGroup);
            return MapToDto(created);
        }

        public async Task<FamilyGroupDto> UpdateFamilyGroupAsync(long id, UpdateFamilyGroupDto updateDto)
        {
            var existingFamilyGroup = await _repository.GetByIdAsync(id);
            if (existingFamilyGroup == null)
                return null;

            existingFamilyGroup.FamilyGroupName = updateDto.FamilyGroupName ?? existingFamilyGroup.FamilyGroupName;
            existingFamilyGroup.QuantityMember = (updateDto.QuantityMember ?? existingFamilyGroup.QuantityMember);
            existingFamilyGroup.Photo = updateDto.Photo ?? existingFamilyGroup.Photo;

            var updated = await _repository.UpdateAsync(existingFamilyGroup);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteFamilyGroupAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        private FamilyGroupDto MapToDto(FamilyGroup FamilyGroup)
        {
            return new FamilyGroupDto
            {
                FamilyGroupId = FamilyGroup.FamilyGroupId,
                FamilyGroupName = FamilyGroup.FamilyGroupName,
                QuantityMember = FamilyGroup.QuantityMember,
                CreationDate = FamilyGroup.CreationDate,
                Photo = FamilyGroup.Photo,
                Families = FamilyGroup.Families
            };
        }
    }
}