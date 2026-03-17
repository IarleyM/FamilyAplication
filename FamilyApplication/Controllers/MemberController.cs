using FamilyApplication.DTOs;
using FamilyApplication.Models;
using FamilyApplication.Services;
using FamilyApplication.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        private readonly IFamilyService _familyService;

        public MembersController(IMemberService memberService, IFamilyService familyService)
        {
            _memberService = memberService;
            _familyService = familyService;
        }

        // GET: api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var members = await _memberService.GetAllMembersAsync();

            if (members.IsNullOrEmpty())
                return Ok("Não há nenhum membro familiar registrado.");

            return Ok(members);
        }

        // GET: api/members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetMemberById(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var member = await _memberService.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound($"Membro com ID {id} não encontrado.");
            }

            return Ok(member);
        }

        // GET: api/members/category/Father
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembersByCategory(string category)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var members = await _memberService.GetMembersByCategoryAsync(category);

            if (members.IsNullOrEmpty())
                return Ok("Não há nenhum membro familiar registrado.");

            return Ok(members);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult<MemberDto>> AddNewMember(CreateMemberDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                var Validation = MemberValidation.IsValidMember(createDto);


                 if (await _familyService.GetFamilyByIdAsync(createDto.FamilyId) == null)
                    return BadRequest("Familia selecionada não existe!");

                if (!Validation.IsValidMember)
                    return BadRequest(Validation.Message);

                var member = await _memberService.CreateMemberAsync(createDto);
                return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberId }, Validation.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar membro: {ex.Message}");
            }
        }

        // PUT: api/members/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(long id, UpdateMemberDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos.");
                }

                if (await _memberService.GetMemberByIdAsync(id) == null)
                    return BadRequest("Membro selecionado não existe!");

                var Validation = MemberValidation.IsValidUpdateMember(updateDto);

                if(!Validation.IsValidMember)
                    return BadRequest(Validation.Message);

                var updated = await _memberService.UpdateMemberAsync(id, updateDto);

                if (updated == null)
                {
                    return NotFound($"Membro com ID {id} não encontrado.");
                }

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar membro: {ex.Message}");
            }
        }

        // DELETE: api/members/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(long id)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Dados inválidos.");
            }

            var result = await _memberService.DeleteMemberAsync(id);

            if (!result)
            {
                return NotFound($"Membro com ID {id} não encontrado.");
            }

            return NoContent();
        }
    }
}