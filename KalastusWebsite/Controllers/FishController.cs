using KalastusWebsite.Data;
using KalastusWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KalastusWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FishController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FishController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Fish
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fish>>> GetFishes()
        {
            var fishes = await _context.Fishes.ToListAsync();
            return Ok(fishes); // Return 200 OK explicitly
        }

        // GET: api/Fish/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Fish>> GetFish(int id)
        {
            var fish = await _context.Fishes.FindAsync(id);
            if (fish == null)
            {
                return NotFound(new { Message = $"Fish with ID {id} not found." });
            }
            return Ok(fish); // Return 200 OK explicitly
        }

        // POST: api/Fish
        [HttpPost]
        public async Task<ActionResult<Fish>> AddFish([FromBody] Fish fish)
        {
            if (fish == null)
            {
                return BadRequest(new { Message = "Invalid fish data." });
            }

            _context.Fishes.Add(fish);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFish), new { id = fish.Id }, fish);
        }

        // PUT: api/Fish/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFish(int id, [FromBody] Fish fish)
        {
            if (fish == null || id != fish.Id)
            {
                return BadRequest(new { Message = "Invalid fish data or ID mismatch." });
            }

            _context.Entry(fish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishExists(id))
                {
                    return NotFound(new { Message = $"Fish with ID {id} not found." });
                }
                else
                {
                    throw; // Rethrow if it's an unexpected error
                }
            }

            return NoContent();
        }

        // DELETE: api/Fish/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFish(int id)
        {
            var fish = await _context.Fishes.FindAsync(id);
            if (fish == null)
            {
                return NotFound(new { Message = $"Fish with ID {id} not found." });
            }

            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Helper method to check if a fish exists
        private bool FishExists(int id)
        {
            return _context.Fishes.Any(f => f.Id == id);
        }
    }
}
