using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository _userRepo;
        private readonly IConfiguration _config;
        public UserController(IRepository userRepo,IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }
        [HttpPost]
        public IActionResult Get(User valUser)
        {
            User user = _userRepo.Login(valUser);
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Login(User credentials)
        {
            try
            {
                User user = _userRepo.Login(credentials);
                if (user != null)
                {
                    var token = GenerateJWT();
                    return Ok(new {token = token});
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return NotFound();
            }
        }
        private string GenerateJWT()
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires:DateTime.Now.AddMinutes(30),
                signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}