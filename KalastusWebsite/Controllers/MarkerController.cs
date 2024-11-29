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
            Console.WriteLine($"Received Marker Data: Latitude = {marker.Latitude}, Longitude = {marker.Longitude}, UserId = {marker.UserId}, MarkerName = {marker.MarkerName}");

            if (!ModelState.IsValid || marker.UserId <= 0)
            {
                Console.WriteLine("Invalid marker data.");
                return BadRequest("Invalid marker data.");
            }

            // Check if the user already has 10 markers
            var markerCount = await _context.Markers.CountAsync(m => m.UserId == marker.UserId);
            if (markerCount >= 10)
            {
                Console.WriteLine("Marker limit reached.");
                return BadRequest("You can only place up to 10 markers.");
            }

            try
            {
                _context.Markers.Add(marker);
                await _context.SaveChangesAsync();
                Console.WriteLine("Marker saved successfully.");
                return CreatedAtAction(nameof(GetMarkers), new { id = marker.Id }, marker);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving marker: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
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
