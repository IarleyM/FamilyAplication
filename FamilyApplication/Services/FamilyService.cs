using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using FamilyApplication.Models;
using FamilyApplication.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _repository;

        public FamilyService(IFamilyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FamilyDto>> GetAllFamilysAsync()
        {
            var Familys = await _repository.GetAllAsync();
            return Familys.Select(m => MapToDto(m));
        }

        public async Task<FamilyDto> GetFamilyByIdAsync(long id)
        {
            var Family = await _repository.GetByIdAsync(id);
            return Family != null ? MapToDto(Family) : null;
        }

        public async Task<FamilyDto> CreateFamilyAsync(CreateFamilyDto createDto, string filepath)
        {
            var Family = new Family
            {
                FamilyName = createDto.FamilyName,
                Photo = filepath,
                FamilyGroupId = createDto.FamilyGroupId
            };

            var created = await _repository.CreateAsync(Family);
            return MapToDto(created);
        }

        public async Task<FamilyDto> UpdateFamilyAsync(long id, UpdateFamilyDto updateDto)
        {
            var existingFamily = await _repository.GetByIdAsync(id);
            if (existingFamily == null)
                return null;

            existingFamily.FamilyName = updateDto.FamilyName ?? existingFamily.FamilyName;
            existingFamily.QuantityMember = updateDto.QuantityMember ?? existingFamily.QuantityMember;
            existingFamily.Photo = updateDto.Photo ?? existingFamily.Photo;

            var updated = await _repository.UpdateAsync(existingFamily);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteFamilyAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }

        private FamilyDto MapToDto(Family Family)
        {
            return new FamilyDto
            {
                FamilyId = Family.FamilyId,
                FamilyName = Family.FamilyName,
                Photo = Family.Photo
            };
        }
    }
}