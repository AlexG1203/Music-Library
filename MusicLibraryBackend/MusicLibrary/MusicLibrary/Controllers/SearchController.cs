using Microsoft.AspNetCore.Mvc;
using MusicLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return BadRequest("Query string cannot be empty");
            }

            var artists = await _context.Artists
                .Where(a => a.ArtistName.Contains(q))
                .ToListAsync();

            var albums = await _context.Albums
                .Where(a => a.AlbumTitle.Contains(q))
                .ToListAsync();

            var songs = await _context.Songs
                .Where(s => s.SongTitle.Contains(q))
                .ToListAsync();

            return Ok(new { artists, albums, songs });
        }
    }
}
