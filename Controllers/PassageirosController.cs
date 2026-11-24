using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoAutenticacaoBasica.Api.Data;
using ProjetoAutenticacaoBasica.Api.Models;
using ProjetoAutenticacaoBasica.Api.Models.Dtos;

namespace ProjetoAutenticacaoBasica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageirosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassageirosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarPassageiro")]
        public async Task<IActionResult> CriarPassageiro(CriarPassageiroDto passageirosDto)
        {
            var novoPassageiro = new Passageiro
            {
                Nome = passageirosDto.Nome,
                Cpf = passageirosDto.Cpf
            };

            _context.Passageiros.Add(novoPassageiro);
            await _context.SaveChangesAsync();

            return Ok("Passageiro Criado com sucesso");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassageiroInfoDto>>> GetPassageiros([FromQuery] string? nome)
        {
            var passageiros = await _context.Passageiros.AsNoTracking().Where(p => string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)).OrderBy(p => p.Nome).Select(p => new PassageiroInfoDto
            {
                Id = p.Id,
                Nome = p.Nome
            }).ToListAsync();

            return Ok(passageiros);

        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PassageiroInfoDto>>GetPassageiroPorCpf(string cpf)
        {
            var passasgeiro = await _context.Passageiros.AsNoTracking().Where(p => p.Cpf == cpf).Select(p => new PassageiroInfoDto
            {
                Id=p.Id,
                Nome =p.Nome
            }).FirstOrDefaultAsync();

            if (passasgeiro is null)
            {
                return NotFound(new { mensagem = "Passageiro Não Encontrado" });
            }

            return Ok(passasgeiro);
        }




    }
}
