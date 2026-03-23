using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using FamilyApplication.Models;
using FamilyApplication.Repositories;
using FamilyApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _environment;


        public PostController(IPostService postService, IMemberService memberService, IWebHostEnvironment environment)
        {
            _postService = postService;
            _memberService = memberService;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPostAsync([FromForm] CreatePostDTO createPostDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var member = await _memberService.GetMemberByIdAsync(createPostDTO.MemberId);

            if (member == null)
                return BadRequest("Membro não encontrado.");

            var webRootPath = _environment.WebRootPath;
            var folderPath = Path.Combine(webRootPath, "uploads", "posts");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filesToSave = new List<PostFileDTO>();

            foreach (var file in createPostDTO.Files)
            {
                if (file.Length == 0)
                    continue;

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                filesToSave.Add(new PostFileDTO
                {
                    PostFileId = Guid.NewGuid(),
                    FileName = fileName,
                    FilePath = $"uploads/posts/{fileName}",
                    ContentType = file.ContentType
                });
            }

            var post = new PostDTO
            {
                Title = createPostDTO.Title,
                Description = createPostDTO.Description,
                MemberId = createPostDTO.MemberId,
                Files = filesToSave
            };

            await _postService.AddNewPostAsync(post);

            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPostsAsync()
        {
            var posts = await _postService.GetAllPostAsync();

            foreach (var post in posts)
            {
                post.Files = (List<PostFileDTO>) await _postService.GetPostFileByPostId(post.PostId);
            }

            return Ok(posts);
        }
    }
}
