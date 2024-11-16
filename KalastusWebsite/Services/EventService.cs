using KalastusWebsite.Models;

namespace KalastusWebsite.Services
{
    public class EventService
    {
        private readonly List<Event> _events = new();

        public IEnumerable<Event> GetEvents() => _events;

        public void AddEvent(Event newEvent)
        {
            newEvent.Id = _events.Count + 1;
            _events.Add(newEvent);
        }

        public void RemoveEvent(int id)
        {
            var ev = _events.FirstOrDefault(e => e.Id == id);
            if (ev != null)
                _events.Remove(ev);
        }
    }
}
