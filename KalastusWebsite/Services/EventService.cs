using KalastusWebsite.Models;
using KalastusWebsite.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KalastusWebsite.Services
{
    public class EventService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public EventService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Events.OrderByDescending(e => e.Date).ToListAsync();
        }

        public async Task<List<Event>> GetUpcomingEventsAsync(int daysAhead = 180)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var endDate = DateTime.Now.AddDays(daysAhead);
            return await context.Events
                .Where(e => e.Date >= DateTime.Now && e.Date <= endDate)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        public async Task AddEventAsync(Event newEvent)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            newEvent.CreatedAt = DateTime.Now;
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
        }

        public async Task RemoveEventAsync(int id, string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var eventToRemove = await context.Events.FirstOrDefaultAsync(e => e.Id == id && e.CreatedBy == username);
            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);
                await context.SaveChangesAsync();
            }
        }
    }
}

