using ConcertTicketBooking.DTOs.Concert;
using ConcertTicketBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketBooking.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _concertService;

        public ConcertsController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        // GET: api/concerts
        [HttpGet]
        public async Task<ActionResult<PagedConcertResponse>> GetAll(
            [FromQuery] string? search,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(
                    "Page number must be positive and page size must be between 1 and 100.");
            }

            var result = await _concertService.GetAllAsync(
                search,
                pageNumber,
                pageSize);

            return Ok(result);
        }

        // GET: api/concerts/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ConcertResponse>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid concert ID.");
            }

            var concert = await _concertService.GetByIdAsync(id);

            if (concert == null)
            {
                return NotFound("Concert not found.");
            }

            return Ok(concert);
        }

        // POST: api/concerts
        [HttpPost]
        public async Task<ActionResult<ConcertResponse>> Create(
            CreateConcertRequest request)
        {
            try
            {
                var concert = await _concertService.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = concert.ConcertId },
                    concert);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/concerts/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ConcertResponse>> Update(
            int id,
            UpdateConcertRequest request)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid concert ID.");
            }

            try
            {
                var concert = await _concertService.UpdateAsync(id, request);

                if (concert == null)
                {
                    return NotFound("Concert not found.");
                }

                return Ok(concert);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/concerts/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid concert ID.");
            }

            try
            {
                bool deleted = await _concertService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound("Concert not found.");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}