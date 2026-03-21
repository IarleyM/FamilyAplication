using FamilyApplication.Models;

namespace FamilyApplication.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<IEnumerable<Post>> GetByMemberIdAsync(long id);
        Task<IEnumerable<Post>> GetByFamilyIdAsync(long id);
        Task<IEnumerable<Post>> GetByFamilyGroupIdAsync(long id);

        Task<Post> AddNewPostAsync(Post post);

        Task<Post> DeletePostAsync(long id);
    }
}
