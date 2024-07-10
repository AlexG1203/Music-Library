using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Context;
using MusicLibrary.Dtos;
using MusicLibrary.Entities;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] SongDto dto)
        {
            var albumExists = await _context.Albums.AnyAsync(x => x.AlbumId == dto.AlbumId);
            if (!albumExists)
            {
                return NotFound("Album does not exist");
            }

            var newSong = new SongEntity()
            {
                AlbumId = dto.AlbumId,
                SongTitle = dto.title,
                SongLength = dto.length
            };

            await _context.Songs.AddAsync(newSong);
            await _context.SaveChangesAsync();

            return Ok("Song Saved Successfully");
        }

        //Read
        [HttpGet]
        public async Task<ActionResult<List<SongEntity>>> GetAllSongs()
        {
            var songs = await _context.Songs.ToListAsync();

            return Ok(songs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SongEntity>> GetSongByID([FromRoute] int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.SongId == id);

            if (song is null)
            {
                return NotFound("Song Not Found");
            }

            return Ok(song);
        }

        [HttpGet]
        [Route("album/{albumId}")]
        public async Task<ActionResult<List<SongEntity>>> GetSongByAlbumID([FromRoute] int albumId)
        {
            var albumExists = await _context.Albums.AnyAsync(x => x.AlbumId == albumId);
            if (!albumExists)
            {
                return NotFound("Album does not exist");
            }

            var songs = await _context.Songs.Where(x => x.AlbumId == albumId).ToListAsync();

            if (songs is null || songs.Count == 0)
            {
                return NotFound("Song Not Found");
            }

            return Ok(songs);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateSong([FromRoute] int id, [FromBody] SongDto dto)
        {
            var albumExists = await _context.Albums.AnyAsync(x => x.AlbumId == dto.AlbumId);
            if (!albumExists)
            {
                return NotFound("Album does not exist");
            }

            var song = await _context.Songs.FirstOrDefaultAsync(x => x.SongId == id);

            if (song is null)
            {
                return NotFound("Song Not Found");
            }

            song.AlbumId = dto.AlbumId;
            song.SongTitle = dto.title;
            song.SongLength = dto.length;

            await _context.SaveChangesAsync();

            return Ok("Song Updated Successfully");
        }

        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSong([FromRoute] int id)
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.SongId == id);

            if (song is null)
            {
                return NotFound("Song Not Found");
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return Ok("Song Deleted Successfully");
        }
    }
}
