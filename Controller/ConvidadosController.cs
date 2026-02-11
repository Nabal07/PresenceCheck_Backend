using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresenceCheck.Api.Infrastructure.Context;
using PresenceCheck.Api.Domain;
// Adicione o using abaixo se criou a pasta DTOs:
// using PresenceCheck.Api.Domain.DTOs; 

namespace PresenceCheck.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConvidadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConvidadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Convidado>>> Get()
    {
        return await _context.Convidados.ToListAsync();
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RespostaConvidado pedido)
    {
        if (string.IsNullOrWhiteSpace(pedido.Nome))
            return BadRequest("Por favor, digite seu nome.");

        var registro = new Convidado
        {
            Nome = pedido.Nome,
            Confirmado = pedido.Vai, 
            DataConfirmacao = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
        };

        _context.Convidados.Add(registro);
        await _context.SaveChangesAsync();

        return Ok(pedido.Vai ? "Presença confirmada!" : "Obrigado por avisar que não virá.");
    }
}