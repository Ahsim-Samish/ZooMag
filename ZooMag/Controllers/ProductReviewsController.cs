using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStoreApi.Data;
using PetStoreApi.Models;

namespace PetStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductReviewsController(ApplicationDbContext context) => _context = context;

    // GET: api/productreviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReview>>> GetReviews()
    {
        return await _context.ProductReviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .ToListAsync();
    }

    // GET: api/productreviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReview>> GetReview(int id)
    {
        var review = await _context.ProductReviews
            .Include(r => r.Product)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);

        return review == null ? NotFound() : Ok(review);
    }

    // POST: api/productreviews
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ProductReview>> PostReview(ProductReview review)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!_context.Products.Any(p => p.Id == review.ProductId))
            return BadRequest("Product not found.");

        if (!_context.Users.Any(u => u.Id == review.UserId))
            return BadRequest("User not found.");

        review.CreatedAt = DateTime.UtcNow;
        _context.ProductReviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
    }

    // PUT: api/productreviews/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutReview(int id, ProductReview review)
    {
        if (id != review.Id) return BadRequest("ID mismatch.");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Запрещаем менять ProductId и UserId
        var existing = await _context.ProductReviews.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Rating = review.Rating;
        existing.Comment = review.Comment;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }

    // DELETE: api/productreviews/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.ProductReviews.FindAsync(id);
        if (review == null) return NotFound();

        _context.ProductReviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}