using FamilyApplication.Enums;
using FamilyApplication.Models;

namespace FamilyApplication.DTOs
{
    public class FamilyDto
    {
        public long FamilyId { get; set; }
        public string FamilyName { get; set; }
        public int QuantityMember { get; set; }
        public List<Member> Members { get; set; }

        public string Photo { get; set; }
    }

    public class CreateFamilyDto
    {
        public string FamilyName { get; set; }
        public string Photo { get; set; }
        public long FamilyGroupId { get; set; }
    }

    public class UpdateFamilyDto
    {
        public string FamilyName { get; set; }
        public int? QuantityMember { get; set; }
        public string Photo { get; set; }
    }
}