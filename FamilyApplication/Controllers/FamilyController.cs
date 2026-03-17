using FamilyApplication.DTOs;
using FamilyApplication.Models;
using FamilyApplication.Services;
using FamilyGroupApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _Familyervice;
        private readonly IFamilyGroupService _FamilyGroupservice;


        public FamilyController(IFamilyService Familyervice, IFamilyGroupService FamilyGroupservice)
        {
            _Familyervice = Familyervice;
            _FamilyGroupservice = FamilyGroupservice;
        }

        // GET: api/Family
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetFamily()
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }


            var Family = await _Familyervice.GetAllFamilysAsync();

            if (Family.IsNullOrEmpty())
                return Ok("Não há nenhuma família registrado.");

            return Ok(Family);
        }

        // GET: api/Family/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyDto>> GetFamily(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var Family = await _Familyervice.GetFamilyByIdAsync(id);

            if (Family == null)
            {
                return NotFound($"Familia nao encontrada. Tente com outro grupo.");
            }

            return Ok(Family);
        }


        // POST: api/Family
        [HttpPost]
        public async Task<ActionResult<FamilyDto>> CreateFamily(CreateFamilyDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                var FamilyGroupExists = await _FamilyGroupservice.GetFamilyGroupByIdAsync(createDto.FamilyGroupId);

                if (FamilyGroupExists == null)
                {
                    return BadRequest($"Familia nao encontrada. Tente com outro grupo.");
                }
                var updateDto = new UpdateFamilyGroupDto
                {
                    QuantityMember = FamilyGroupExists.QuantityMember + 1
                };

                FamilyGroupExists = await _FamilyGroupservice.UpdateFamilyGroupAsync(FamilyGroupExists.FamilyGroupId, updateDto);

                var Family = await _Familyervice.CreateFamilyAsync(createDto);
                return CreatedAtAction(nameof(GetFamily), new { id = Family.FamilyId }, Family);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar membro: {ex.Message}");
            }
        }

        // PUT: api/Family/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamily(long id, UpdateFamilyDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                var updated = await _Familyervice.UpdateFamilyAsync(id, updateDto);

                if (updated == null)
                {
                    return NotFound($"Familia nao encontrada. Tente com outro grupo.");
                }

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar membro: {ex.Message}");
            }
        }

        // DELETE: api/Family/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var result = await _Familyervice.DeleteFamilyAsync(id);

            if (!result)
            {
                return NotFound($"Familia nao encontrada. Tente com outro grupo.");
            }

            return NoContent();
        }
    }
}