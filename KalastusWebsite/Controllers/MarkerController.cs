using KalastusWebsite.Data;
using KalastusWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KalastusWebsite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarkerController(AppDbContext context)
        {
            _context = context;
        }

        // Get all markers for the logged-in user
        [HttpGet]
        public async Task<IActionResult> GetMarkers(int userId)
        {
            var markers = await _context.Markers
                .Where(m => m.UserId == userId)
                .ToListAsync();
            return Ok(markers);
        }

        // Add a new marker
        [HttpPost]
        public async Task<IActionResult> AddMarker([FromBody] Marker marker)
        {
            if (ModelState.IsValid)
            {
                _context.Markers.Add(marker);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMarkers), new { marker.Id }, marker);
            }
            return BadRequest(ModelState);
        }

        // Delete a marker
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarker(int id, int userId)
        {
            var marker = await _context.Markers.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (marker == null)
            {
                return NotFound("Marker not found or you do not have permission to delete it.");
            }

            _context.Markers.Remove(marker);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
