using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PratikJwt.Context;
using PratikJwt.Jwt;
using PratikJwt.Models;

namespace PratikJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtDbContext _context;

        public AuthController(JwtDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = _context.Users.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());

            if (userEntity == null)
            {
                return BadRequest("E postta adresi ya da şifre hatalı");

            }

            if (request.Password == userEntity.Password)
            {
                //jwt üretme
                // Sadece bu metodda kullanılacak olan JWT bilgilerini almak için IConfiguration nesnesini kullanır
                var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var token = JwtHelper.GenerateJwtToken(new JwtDto
                {
                    Email = request.Email,
                    SecretKey = configuration["Jwt:SecretKey"]!,
                    Issuer = configuration["Jwt:Issuer"]!,
                    Audience = configuration["Jwt:Audience"]!,
                    ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
                });

                return Ok(token);
            }
            else
            {
                return Unauthorized("Şifre hatalı.");
            }

        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var userEmails = _context.Users.Select(x => x.Email).ToList();
            return Ok(userEmails);
        }
    }
}
