using FamilyApplication.DTOs;
using FamilyGroupApplication.Services;
using Microsoft.AspNetCore.Mvc;

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
            var FamilyGroup = await _FamilyGroupervice.GetAllFamilyGroupsAsync();
            return Ok(FamilyGroup);
        }

        // GET: api/FamilyGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyGroupDto>> GetFamilyGroupById(long id)
        {
            var FamilyGroup = await _FamilyGroupervice.GetFamilyGroupByIdAsync(id);

            if (FamilyGroup == null)
            {
                return NotFound($"Família não encontrada! Tente novamente mais tarde.");
            }

            return Ok(FamilyGroup);
        }


        // POST: api/FamilyGroup
        [HttpPost]
        public async Task<ActionResult<FamilyGroupDto>> CreateFamilyGroup(CreateFamilyGroupDto createDto)
        {
            try
            {
                var FamilyGroup = await _FamilyGroupervice.CreateFamilyGroupAsync(createDto);
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
            var result = await _FamilyGroupervice.DeleteFamilyGroupAsync(id);

            if (!result)
            {
                return NotFound($"Família não encontrada! Tente novamente mais tarde.");
            }

            return NoContent();
        }
    }
}