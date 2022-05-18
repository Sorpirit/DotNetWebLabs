using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheaterApp.Model;

namespace TheaterApp.Data
{
    public interface ITheaterRepository
    {
        Task<TheaterEvent?> GetEventById(int id);

        Task<IEnumerable<TheaterEvent?>> GetEventsByAuthor(string author);
        
        Task<IEnumerable<TheaterEvent?>> GetEventsByDate(DateTime date);
        
        Task<IEnumerable<TheaterEvent?>> GetEventsByName(string name);
        
        Task<IEnumerable<TheaterEvent?>> GetEventsByGenre(string genre);

        Task<List<TheaterEvent?>> GetAllEvents();

        Task AddTheaterEvent(TheaterEvent theaterEvent);

        Task<bool> RemoveTheaterEventById(int id);

        Task UpdateTheaterEvent(TheaterEvent theaterEvent);

        Task<Ticket?> GetTicketById(int id);

        Task<bool> BookTicket(Ticket ticket);

        Task<bool> BuyTicket(Ticket ticket, bool buyBooked = false);

        Task<bool> AddOrUpdateTicket(Ticket ticket, bool allowBooked = false);

        Task<bool> RefundTicket(Ticket ticket);
    }
}