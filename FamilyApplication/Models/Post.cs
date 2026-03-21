using System.Diagnostics.Eventing.Reader;

namespace FamilyApplication.Models
{
    public class Post
    {
        public long PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long MemberId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeletionDate { get; set; }

        public List<PostFile> Files { get; set; }

        public Post() {}
    }
}
