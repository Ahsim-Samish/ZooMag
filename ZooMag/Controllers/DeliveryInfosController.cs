using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStoreApi.Data;
using ProductStoreApi.Models;

namespace ProductStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryInfosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DeliveryInfosController(ApplicationDbContext context) => _context = context;

    // GET: api/deliveryinfos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeliveryInfo>>> GetDeliveryInfos()
    {
        return await _context.DeliveryInfos
            .Include(d => d.User)
            .ToListAsync();
    }

    // GET: api/deliveryinfos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DeliveryInfo>> GetDeliveryInfo(int id)
    {
        var info = await _context.DeliveryInfos
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.Id == id);

        return info == null ? NotFound() : Ok(info);
    }

    // POST: api/deliveryinfos
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<DeliveryInfo>> PostDeliveryInfo(DeliveryInfo info)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_context.Users.Any(u => u.Id == info.UserId))
            return BadRequest("User not found.");

        _context.DeliveryInfos.Add(info);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDeliveryInfo), new { id = info.Id }, info);
    }

    // PUT: api/deliveryinfos/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutDeliveryInfo(int id, DeliveryInfo info)
    {
        if (id != info.Id) return BadRequest("ID mismatch.");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.Entry(info).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.DeliveryInfos.Any(e => e.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/deliveryinfos/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDeliveryInfo(int id)
    {
        var info = await _context.DeliveryInfos.FindAsync(id);
        if (info == null) return NotFound();

        _context.DeliveryInfos.Remove(info);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}