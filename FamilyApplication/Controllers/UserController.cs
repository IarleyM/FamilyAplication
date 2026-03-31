using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyApplication.DTOs;
using FamilyApplication.Services;
using FamilyApplication.utils;
using Microsoft.AspNetCore.Mvc;

namespace FamilyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");
            var validation = UserValidation.IsValidUser(createUserDTO);
            if (!validation.IsValidMember)
                return BadRequest(validation.Message);

            await _userService.CreateUserAsync(createUserDTO);

            return Ok("Usuario adicionado com Sucesso");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUser()
        {
            var Users = await _userService.GetUsersAsync();

            if (Users == null || !Users.Any())
                return BadRequest("Não usuários adicionados!");

            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuario não encontrado!");
            return Ok(user);
        }
    }
}
