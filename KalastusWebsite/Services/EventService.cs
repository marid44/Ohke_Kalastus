using KalastusWebsite.Models;
using KalastusWebsite.Data;
using System.Linq;
using System.Collections.Generic;

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

        public void RemoveEvent(int id, string username, string role)
        {
            var ev = _context.Events.Find(id);
            if (ev != null && (role == "Admin" || ev.CreatedBy == username))
            {
                _context.Events.Remove(ev);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Sinulla ei ole oikeuksia poistaa tätä tapahtumaa.");
            }
        }
    }
}
