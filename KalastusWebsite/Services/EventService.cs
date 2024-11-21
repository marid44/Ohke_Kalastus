using KalastusWebsite.Models;
using KalastusWebsite.Data;
using System.Linq;

namespace KalastusWebsite.Services
{
    public class EventService
    {
        private readonly List<Event> _events = new List<Event>();

        public void AddEvent(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public void RemoveEvent(int id, string username)
        {
            var eventToRemove = _events.FirstOrDefault(e => e.Id == id && e.CreatedBy == username);
            
            if (eventToRemove != null)
            {
                _events.Remove(eventToRemove);
            }
        }
        public List<Event> GetEvents()
        {
            return _events;
        }
    }
}
