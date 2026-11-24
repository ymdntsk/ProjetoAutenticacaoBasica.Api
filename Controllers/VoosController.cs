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
    public class VoosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public VoosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("CriarVoo")]
        public async Task<IActionResult> CriarVoo(CriarVooDto criarVoodto)
        {
            var novoVoo = new Voo
            {
                Codigo = criarVoodto.Codigo,
                Origem = criarVoodto.Origem,
                Destino = criarVoodto.Destino,

            };


            _context.Voos.Add(novoVoo);
            await _context.SaveChangesAsync();

            return Ok("Voo Criado Com sucesso");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumoVooDto>>> GetVoos()
        {
            var voos = await _context.Voos.AsNoTracking().OrderBy(v => v.Codigo).Select(v => new ResumoVooDto
            {
                Id = v.Id,
                Codigo = v.Codigo,
                Trajeto = v.Origem + "->" + v.Destino
            }).ToListAsync();

            return Ok(voos);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<VOoManifestoDto>> GetVooPorCodigo(int codigo)
        {
            var manifesto = await _context.Voos.AsNoTracking().Where(v => v.Codigo == codigo).Select(v => new VOoManifestoDto
            {
                Id = v.Id,
                Codigo = v.Codigo,
                Trajeto = v.Origem + "->" + v.Destino,
                Passageiro = v.Passagens.Select(p => new PassageiroNoVooDto
                {
                    Nome = p.Passageiro.Nome,
                    Assento = p.Assento
                }).ToList()
            }).FirstOrDefaultAsync();

            if (manifesto == null)
            {
                return NotFound("Voo Não Encontrado");
            }

            return Ok(manifesto);
         }

    }
}



