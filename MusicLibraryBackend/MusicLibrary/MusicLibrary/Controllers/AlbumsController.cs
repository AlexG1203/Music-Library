using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Context;
using MusicLibrary.Dtos;
using MusicLibrary.Entities;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] AlbumDto dto)
        {
            var artistExists = await _context.Artists.AnyAsync(x => x.ArtistId == dto.ArtistId);
            if (!artistExists)
            {
                return NotFound("Artist does not exist");
            }

            var newAlbum = new AlbumEntity()
            {
                AlbumTitle = dto.title,
                AlbumDescription = dto.description,
                ArtistId = dto.ArtistId
            };

            await _context.Albums.AddAsync(newAlbum);
            await _context.SaveChangesAsync();

            return Ok("Album Saved Successfully");
        }

        //Read
        [HttpGet]
        public async Task<ActionResult<List<AlbumEntity>>> GetAllAlbums()
        {
            var albums = await _context.Albums.ToListAsync();

            return Ok(albums);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AlbumEntity>> GetAlbumByID([FromRoute] int id)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumId == id);

            if (album is null)
            {
                return NotFound("Album Not Found");
            }

            return Ok(album);
        }

        [HttpGet]
        [Route("artist/{artistId}")]
        public async Task<ActionResult<List<AlbumEntity>>> GetAlbumByArtistID([FromRoute] int artistId)
        {
            var artistExists = await _context.Artists.AnyAsync(x => x.ArtistId == artistId);
            if (!artistExists)
            {
                return NotFound("Artist does not exist");
            }

            var albums = await _context.Albums.Where(x => x.ArtistId == artistId).ToListAsync();

            if (albums is null || albums.Count == 0)
            {
                return NotFound("Album Not Found");
            }

            return Ok(albums);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAlbum([FromRoute] int id, [FromBody] AlbumDto dto)
        {
            var artistExists = await _context.Artists.AnyAsync(x => x.ArtistId == dto.ArtistId);
            if (!artistExists)
            {
                return NotFound("Artist does not exist");
            }

            var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumId == id);

            if (album is null)
            {
                return NotFound("Album Not Found");
            }

            album.ArtistId = dto.ArtistId;
            album.AlbumTitle = dto.title;
            album.AlbumDescription = dto.description;

            await _context.SaveChangesAsync();

            return Ok("Album Updated Successfully");
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int id)
        {
            var album = await _context.Albums.FirstOrDefaultAsync(x => x.AlbumId == id);

            if (album is null)
            {
                return NotFound("Album Not Found");
            }

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return Ok("Album Deleted Successfully");
        }
    }
}
