using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheaterApp.Model;

namespace TheaterApp.Data
{
    public class TheaterDbRepository : ITheaterRepository
    {
        private readonly EventDbContext _context;

        public TheaterDbRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task<TheaterEvent?> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<TheaterEvent?>> GetEventsByAuthor(string author)
        {
            var list = await _context.Events.ToListAsync();
            var searchRegex = new Regex(author);
            return list.Where(e => e != null && searchRegex.Match(e.Author).Success);
        }

        public async Task<IEnumerable<TheaterEvent?>> GetEventsByDate(DateTime date)
        {
            var list = await _context.Events.ToListAsync();
            return list.Where(e => e != null && e.Date.Equals(date));
        }

        public async Task<IEnumerable<TheaterEvent?>> GetEventsByName(string name)
        {
            var list = await _context.Events.ToListAsync();
            var searchRegex = new Regex(name);
            return list.Where(e => e != null && searchRegex.Match(e.Title).Success);
        }

        public async Task<IEnumerable<TheaterEvent?>> GetEventsByGenre(string genre)
        {
            var list = await _context.Events.ToListAsync();
            return list.Where(e => e != null && genre.Equals(e.Genre));
        }

        public async Task<List<TheaterEvent?>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task AddTheaterEvent(TheaterEvent theaterEvent)
        {
            await _context.Events.AddAsync(theaterEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveTheaterEventById(int id)
        {
            var tEvent = await GetEventById(id);
            if (tEvent != null)
            {
                _context.Events.Remove(tEvent);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task UpdateTheaterEvent(TheaterEvent theaterEvent)
        {
            _context.Events.Update(theaterEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetTicketById(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<bool> BookTicket(Ticket ticket)
        {
            ticket.State = TicketState.Booked;

            return await AddOrUpdateTicket(ticket);
        }

        public async Task<bool> BuyTicket(Ticket ticket, bool buyBooked = false)
        {
            ticket.State = TicketState.Sold;

            return await AddOrUpdateTicket(ticket, buyBooked);
        }

        public async Task<bool> AddOrUpdateTicket(Ticket ticket, bool allowBooked = false)
        {
            var dbTicket = await GetTicketById(ticket.Id);
            if (dbTicket == null)
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
                return true;
            }

            if (dbTicket.State != TicketState.Available || (allowBooked && dbTicket.State != TicketState.Booked))
                return false;

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RefundTicket(Ticket ticket)
        {
            ticket.State = TicketState.Available;

            return await AddOrUpdateTicket(ticket, true);
        }
    }
}