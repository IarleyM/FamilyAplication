using System.ComponentModel.DataAnnotations;

namespace FamilyApplication.Models
{
    public class FamilyGroup
    {
        public long FamilyGroupId { get; set; }
        [Required]
        public string FamilyGroupName { get; set; }
        public int QuantityMember { get; set; }
        public string Photo {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletionDate { get; set; }

        public List<Family> Families { get; set; }
        public FamilyGroup() { }
    }
}
