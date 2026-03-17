using FamilyApplication.DTOs;
using FamilyApplication.utils;
using FamilyGroupApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FamilyGroupApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService _FamilyGroupervice;

        public FamilyGroupController(IFamilyGroupService FamilyGroupervice)
        {
            _FamilyGroupervice = FamilyGroupervice;
        }

        // GET: api/FamilyGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyGroupDto>>> GetFamilyGroup()
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var FamilyGroup = await _FamilyGroupervice.GetAllFamilyGroupsAsync();

            if (FamilyGroup.IsNullOrEmpty())
                return Ok("Não há nenhum grupo familiar registrado.");

            return Ok(FamilyGroup);
        }

        // GET: api/FamilyGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyGroupDto>> GetFamilyGroupById(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var FamilyGroup = await _FamilyGroupervice.GetFamilyGroupByIdAsync(id);

            if (FamilyGroup == null)
            {
                return NotFound($"Família não encontrada! Tente novamente mais tarde.");
            }

            return Ok(FamilyGroup);
        }


        // POST: api/FamilyGroup
        [HttpPost]
        public async Task<ActionResult<FamilyGroupDto>> CreateFamilyGroup([FromForm] CreateFamilyGroupDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(createDto.Photo.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                createDto.Photo.CopyTo(fileStream);

                var Validation = FamilyGroupValidation.IsValidFamilyGroup(createDto);

                if (!Validation.IsValidMember)
                    return BadRequest(Validation.Message);

                var FamilyGroup = await _FamilyGroupervice.CreateFamilyGroupAsync(createDto, filePath);
                return CreatedAtAction(nameof(GetFamilyGroup), new { id = FamilyGroup.FamilyGroupId }, FamilyGroup);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar membro: {ex.Message}");
            }
        }

        // PUT: api/FamilyGroup/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamilyGroup(long id, UpdateFamilyGroupDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                var updated = await _FamilyGroupervice.UpdateFamilyGroupAsync(id, updateDto);

                if (updated == null)
                {
                    return NotFound($"Família não encontrada! Tente novamente mais tarde.");
                }

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar membro: {ex.Message}");
            }
        }

        // DELETE: api/FamilyGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyGroup(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var result = await _FamilyGroupervice.DeleteFamilyGroupAsync(id);

            if (!result)
            {
                return NotFound($"Família não encontrada! Tente novamente mais tarde.");
            }

            return NoContent();
        }
    }
}