using ConcertTicketBooking.DTOs.Concert;
using ConcertTicketBooking.Models;
using ConcertTicketBooking.Models.Enums;
using ConcertTicketBooking.Repositories.Interfaces;
using ConcertTicketBooking.Services.Interfaces;

namespace ConcertTicketBooking.Services.Implementations
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _repository;

        public ConcertService(IConcertRepository repository)
        {
            _repository = repository;
        }

        // CREATE
        public async Task<ConcertResponse> CreateAsync(
            CreateConcertRequest request)
        {
            if (!Enum.IsDefined(typeof(ConcertStatus), request.Status))
            {
                throw new ArgumentException("Invalid concert status.");
            }

            var concert = new Concert
            {
                ConcertName = request.ConcertName.Trim(),
                ArtistName = request.ArtistName.Trim(),
                Description = request.Description?.Trim(),
                Genre = request.Genre.Trim(),
                Language = request.Language.Trim(),
                ImageUrl = request.ImageUrl?.Trim(),
                Status = request.Status,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _repository.AddAsync(concert);

            await _repository.SaveChangesAsync();

            return MapToResponse(concert);
        }

        // GET BY ID
        public async Task<ConcertResponse?> GetByIdAsync(int id)
        {
            var concert = await _repository.GetByIdAsync(id);

            if (concert == null)
            {
                return null;
            }

            return MapToResponse(concert);
        }

        // GET ALL WITH SEARCH AND PAGINATION
        public async Task<PagedConcertResponse> GetAllAsync(
            string? search,
            int pageNumber,
            int pageSize)
        {
            var result = await _repository.GetPagedAsync(
                search,
                pageNumber,
                pageSize);

            var concertResponses = result.Concerts
                .Select(MapToResponse)
                .ToList();

            return new PagedConcertResponse
            {
                Concerts = concertResponses,
                TotalCount = result.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(
                    (double)result.TotalCount / pageSize)
            };
        }

        // UPDATE
        public async Task<ConcertResponse?> UpdateAsync(
            int id,
            UpdateConcertRequest request)
        {
            if (!Enum.IsDefined(typeof(ConcertStatus), request.Status))
            {
                throw new ArgumentException("Invalid concert status.");
            }

            var concert = await _repository.GetByIdAsync(id);

            if (concert == null)
            {
                return null;
            }

            concert.ConcertName = request.ConcertName.Trim();
            concert.ArtistName = request.ArtistName.Trim();
            concert.Description = request.Description?.Trim();
            concert.Genre = request.Genre.Trim();
            concert.Language = request.Language.Trim();
            concert.ImageUrl = request.ImageUrl?.Trim();
            concert.Status = request.Status;

            concert.UpdatedAtUtc = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return MapToResponse(concert);
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var concert = await _repository.GetByIdAsync(id);

            if (concert == null)
            {
                return false;
            }

            // Only Draft concerts can be deleted
            if (concert.Status != ConcertStatus.Draft)
            {
                throw new InvalidOperationException(
                    "Only Draft concerts can be deleted.");
            }

            _repository.Delete(concert);

            await _repository.SaveChangesAsync();

            return true;
        }

        // Convert Entity into Response DTO
        private static ConcertResponse MapToResponse(Concert concert)
        {
            return new ConcertResponse
            {
                ConcertId = concert.ConcertId,
                ConcertName = concert.ConcertName,
                ArtistName = concert.ArtistName,
                Description = concert.Description,
                Genre = concert.Genre,
                Language = concert.Language,
                ImageUrl = concert.ImageUrl,
                Status = concert.Status,
                CreatedAtUtc = concert.CreatedAtUtc,
                UpdatedAtUtc = concert.UpdatedAtUtc
            };
        }
    }
}