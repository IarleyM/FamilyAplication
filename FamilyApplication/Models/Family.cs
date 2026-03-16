using System.ComponentModel.DataAnnotations;

namespace FamilyApplication.Models
{
    public class Family
    {
        public long FamilyId { get; set; }
        public string FamilyName { get; set; }
        public int QuantityMember { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        [Required]
        public string Photo { get; set; }

        public long FamilyGroupId { get; set; }

        public List<Member> Members { get; set; }

        public Family() { }
    }
}
