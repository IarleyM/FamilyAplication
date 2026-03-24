using FamilyApplication.Data;
using FamilyApplication.DTOs;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Post> AddNewPostAsync(Post post)
        {
            post.CreationDate = DateTime.Now;

            await _context.Post.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public Task<Post> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            return await _context.Post
                 .Where(m => m.DeletionDate == null)
                 .ToListAsync();
        }

        public Task<IEnumerable<Post>> GetByFamilyGroupIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByFamilyIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetByMemberIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostFile>> GetPostFileByPostId(long id)
        {
            return await _context.PostFile
                .Where(pf => pf.PostId == id)
                 .ToListAsync();
        }
    }
}
