using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.DTOs
{
    public class FamilyGroupDto
    {
        public long FamilyGroupId { get; set; }
        public string FamilyGroupName { get; set; }
        public int QuantityMember { get; set; }
        public List<Family> Families { get; set; }
        public DateTime CreationDate { get; set; }
        public string Photo { get; set; }
    }

    public class CreateFamilyGroupDto
    {
        public string FamilyGroupName { get; set; }
        public string Photo { get; set; }
    }

    public class UpdateFamilyGroupDto
    {
        public string? FamilyGroupName { get; set; }
        public int? QuantityMember { get; set; }
        public string? Photo { get; set; }
    }
}