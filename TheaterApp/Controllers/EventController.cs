using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterApp.Data;
using TheaterApp.Model;

namespace TheaterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ITheaterRepository _repository;
        
        public EventController(ITheaterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TheaterEvent?>> Get()
        {
            return await _repository.GetAllEvents();
        }
        
        [HttpGet("id")]
        [ProducesResponseType(typeof(TheaterEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var theaterEvent = await _repository.GetEventById(id);
            return theaterEvent == null ? NotFound() : Ok(theaterEvent);
        }
        
        [HttpGet("author")]
        [ProducesResponseType(typeof(IEnumerable<TheaterEvent?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByAuthor(string author)
        {
            var theaterEvent = await _repository.GetEventsByAuthor(author);
            return Ok(theaterEvent);
        }
        
        [HttpGet("date")]
        [ProducesResponseType(typeof(IEnumerable<TheaterEvent?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var theaterEvent = await _repository.GetEventsByDate(date);
            return Ok(theaterEvent);
        }
        
        [HttpGet("name")]
        [ProducesResponseType(typeof(IEnumerable<TheaterEvent?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName(string name)
        {
            var theaterEvent = await _repository.GetEventsByName(name);
            return Ok(theaterEvent);
        }
        
        [HttpGet("genre")]
        [ProducesResponseType(typeof(IEnumerable<TheaterEvent?>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            var theaterEvent = await _repository.GetEventsByGenre(genre);
            return Ok(theaterEvent);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(TheaterEvent? theaterEvent)
        {
            await _repository.AddTheaterEvent(theaterEvent);
            return CreatedAtAction(nameof(GetById), new {id = theaterEvent.Id}, theaterEvent);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, TheaterEvent theaterEvent)
        {
            if (id != theaterEvent.Id)
                return BadRequest();
            
            await _repository.UpdateTheaterEvent(theaterEvent);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _repository.RemoveTheaterEventById(id);
            if (!res)
                return NotFound();
            return NoContent();
        }
    }
} 