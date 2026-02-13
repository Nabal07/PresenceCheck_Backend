using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresenceCheck.Api.Infrastructure.Context;
using PresenceCheck.Api.Domain;

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
        var nomeLimpo = pedido.Nome?.Trim();

        if (string.IsNullOrWhiteSpace(nomeLimpo))
            return BadRequest("Por favor, digite seu nome.");

        var jaExiste = await _context.Convidados
            .AnyAsync(c => c.Nome.ToLower() == nomeLimpo.ToLower());

        if (jaExiste)
        {
            return BadRequest("Este nome ja confirmou presenca!");
        }

        var registro = new Convidado
        {
            Nome = nomeLimpo,
            Confirmado = pedido.Vai,
            DataConfirmacao = DateTime.UtcNow
        };

        _context.Convidados.Add(registro);
        await _context.SaveChangesAsync();

        return Ok(pedido.Vai ? "Presenca confirmada!" : "Obrigado por avisar.");
    }
}