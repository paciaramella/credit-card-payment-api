using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CreditLinesController : ControllerBase
{
    private readonly CreditLineDbContext _context;

    public CreditLinesController(CreditLineDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CreditLine>>> GetCreditLines()
    {
        return await _context.CreditLines.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CreditLine>> CreateCreditLine(CreditLine creditLine)
    {
        _context.CreditLines.Add(creditLine);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCreditLine), new { id = creditLine.Id }, creditLine);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditLine>> GetCreditLine(int id)
    {
        var creditLine = await _context.CreditLines.FindAsync(id);

        if (creditLine == null)
        {
            return NotFound();
        }

        return creditLine;
    }

    // Implement other CRUD actions: Update and Delete
}
