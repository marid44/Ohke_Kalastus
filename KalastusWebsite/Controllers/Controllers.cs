using Microsoft.AspNetCore.Mvc;
using KalastusWebsite.Models;
using KalastusWebsite.Data;

namespace KalastusWebsite.Controllers
{
    [ApiController]  // Tämä määrittää controllerin Web API -kontrolleriksi
    [Route("api/[controller]")]
    public class EventsController : ControllerBase  // Perii ControllerBase, ei MVC:stä
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        // GET api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(eventItem);
        }

        // POST api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {
            if (newEvent == null)
                return BadRequest();

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        // PUT api/events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            if (updatedEvent == null || updatedEvent.Id != id)
                return BadRequest();

            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            eventItem.Name = updatedEvent.Name;
            eventItem.Description = updatedEvent.Description;
            eventItem.Date = updatedEvent.Date;
            eventItem.Location = updatedEvent.Location;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null)
                return NotFound();

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
