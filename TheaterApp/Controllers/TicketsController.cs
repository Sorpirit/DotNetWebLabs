using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheaterApp.Data;
using TheaterApp.Model;

namespace TheaterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : Controller
    {
        private readonly ITheaterRepository _repository;
        
        public TicketsController(ITheaterRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("id")]
        [ProducesResponseType(typeof(TheaterEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var theaterEvent = await _repository.GetTicketById(id);
            return theaterEvent == null ? NotFound() : Ok(theaterEvent);
        }
        
        [HttpPut("{action}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Buy(TicketAction action, Ticket ticket)
        {
            bool res = action switch
            {
                TicketAction.Buy => await _repository.BuyTicket(ticket, ticket.State == TicketState.Booked),
                TicketAction.Book => await _repository.BookTicket(ticket),
                TicketAction.Refund => await _repository.RefundTicket(ticket),
                _ => false
            };
            
            if (!res)
                return Forbid();

            return Ok();
        }
        
        public enum TicketAction
        {
            Buy,
            Book,
            Refund
        }
    }
}