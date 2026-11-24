using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAutenticacaoBasica.Api.Models;
using ProjetoAutenticacaoBasica.Api.Models.Dtos;
using ProjetoAutenticacaoBasica.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjetoAutenticacaoBasica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerdto)
        {
            var novoUsuarion = new Usuario
            {
                Nome = registerdto.Nome,
                Username = registerdto.Username,
                Password = registerdto.Password,
                CreatedAt = DateTime.Now
            };

            _context.Usuarios.Add(novoUsuarion);
            await _context.SaveChangesAsync();

            return Ok("Usuario Criado com sucesso");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logindto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == logindto.Username && u.Password == logindto.Password);

            if (usuario == null)
            {
                return NotFound("Credenciais Invalidas");
            }

            return Ok(new { Nome = usuario.Nome, });
        }

    }
}
