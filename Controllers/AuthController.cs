﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using trading_app_3_api.Model.User;
using trading_app_3_api.Repositories;
using trading_app_3_api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trading_app_3_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService = new AuthService();
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;    
        }

        [HttpPost("register")]
        [AllowAnonymous]
        //public async Task<ActionResult> Register(User user)
        public async Task<ActionResult> Register(Register userInput)
        {
            User newUserData = new()
            {
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                Email = userInput.Email,
                Password = userInput.Password,
                UserName = userInput.UserName
            };

            string hashedPassword = _authService.GenerateHash(newUserData, newUserData.Password!);
            newUserData.Password = hashedPassword;

            User? user = await _userRepository.PostNewUser(newUserData);

            if (user == null)
            {
                return BadRequest("Dados inválidos");
            }

            user.Password = "";

            var token = _authService.GenerateToken(user);

            return Ok(new
            {
                user,
                token
            });
        }


        // DELETE api/<ValuesController>/5
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] Login loginInput)
        {
            // Recupera o usuário
            var user = await _userRepository.GetUser(loginInput.Email!);
            var hashedPassword = user.Password;
            bool isAValidPassword = _authService.VerifyHash(user, loginInput.Password, hashedPassword);

            // Verifica se o usuário existe
            // Usar a função de verificação de hash do AuthService no lugar da terceira verificação (faltam ajustes)
            if (user == null || loginInput.Password == null || !isAValidPassword)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Oculta a senha
            user.Password = "";

            // Gera o Token
            var token = _authService.GenerateToken(user);

            // Retorna os dados
            return Ok(new
            {
                user,
                token
            });
        }
    }
}
