using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Context;
using MusicLibrary.Dtos;
using MusicLibrary.Entities;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistDto dto)
        {
            var newArtist = new ArtistEntity()
            {
                ArtistName = dto.name
            };
            
            await _context.Artists.AddAsync(newArtist);
            await _context.SaveChangesAsync();

            return Ok("Artist Saved Successfully");
        }

        //Read
        [HttpGet]
        public async Task<ActionResult<List<ArtistEntity>>> GetAllArtists()
        {
            var artists = await _context.Artists.ToListAsync();

            return Ok(artists);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ArtistEntity>> GetArtistByID([FromRoute] int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.ArtistId == id);

            if(artist is null)
            {
                return NotFound("Artist Not Found");
            }

            return Ok(artist);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateArtist([FromRoute] int id, [FromBody] ArtistDto dto)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.ArtistId == id);

            if (artist is null)
            {
                return NotFound("Artist Not Found");
            }

            artist.ArtistName = dto.name;

            await _context.SaveChangesAsync();

            return Ok("Artist Updated Successfully");
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] int id)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(x => x.ArtistId == id);

            if (artist is null)
            {
                return NotFound("Artist Not Found");
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok("Artist Deleted Successfully");
        }
    }
}
