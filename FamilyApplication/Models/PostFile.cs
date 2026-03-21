using FamilyApplication.Enums;
using System.Text.Json.Serialization;

namespace FamilyApplication.Models
{
    public class PostFile
    {
        public Guid PostFileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public string ContentType { get; set; }

        public PostFile() { }   
    }
}
