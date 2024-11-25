using KalastusWebsite.Models;
using KalastusWebsite.Data;
using System.Linq;

namespace KalastusWebsite.Services
{
    public class EventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Event> GetEvents() => _context.Events.ToList();

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public void RemoveEvent(int id)
        {
            var ev = _context.Events.Find(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                _context.SaveChanges();
            }
        }
    }
}

