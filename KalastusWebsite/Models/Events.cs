using System;

namespace KalastusWEbsite.Models
{
    public class Event
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
    
    public class Events
    {
        // Lista tapahtumista
        public List<Event> EventList { get; set; } = new List<Event>();

        // Lisää uusi tapahtuma
        public void AddEvent(string name, DateTime time)
        {
            EventList.Add(new Event { Name = name, Time = time });
        }
    }
}
