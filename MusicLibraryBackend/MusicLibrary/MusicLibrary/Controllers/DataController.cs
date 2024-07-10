using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Context;
using MusicLibrary.Dtos;
using MusicLibrary.Entities;
using System.Text.Json;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("load-json")]
        public async Task<IActionResult> ImportArtists()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "Utils", "data.json");

            if (!System.IO.File.Exists(file))
            {
                return NotFound("File not found.");
            }

            try
            {
                var jsonString = await System.IO.File.ReadAllTextAsync(file);

                var artists = JsonSerializer.Deserialize<List<ArtistDto>>(jsonString);

                if (artists == null || artists.Count == 0)
                {
                    return BadRequest("No valid data found in the file");
                }

                foreach (var artistDto in artists)
                {
                    var existingArtist = await _context.Artists.FirstOrDefaultAsync(a => a.ArtistName == artistDto.name);

                    if (existingArtist == null)
                    {
                        var newArtist = new ArtistEntity { ArtistName = artistDto.name };
                        _context.Artists.Add(newArtist);

                        await _context.SaveChangesAsync();

                        foreach (var albumDto in artistDto.albums)
                        {
                            var newAlbum = new AlbumEntity
                            {
                                ArtistId = newArtist.ArtistId,
                                AlbumTitle = albumDto.title,
                                AlbumDescription = albumDto.description
                            };
                            _context.Albums.Add(newAlbum);

                            await _context.SaveChangesAsync();

                            foreach (var songDto in albumDto.songs)
                            {
                                var newSong = new SongEntity
                                {
                                    AlbumId = newAlbum.AlbumId,
                                    SongTitle = songDto.title,
                                    SongLength = songDto.length
                                };
                                _context.Songs.Add(newSong);
                            }
                        }
                    }
                    else
                    {
                        foreach (var albumDto in artistDto.albums)
                        {
                            var newAlbum = new AlbumEntity
                            {
                                ArtistId = existingArtist.ArtistId,
                                AlbumTitle = albumDto.title,
                                AlbumDescription = albumDto.description
                            };
                            _context.Albums.Add(newAlbum);

                            await _context.SaveChangesAsync();

                            foreach (var songDto in albumDto.songs)
                            {
                                var newSong = new SongEntity
                                {
                                    AlbumId = newAlbum.AlbumId,
                                    SongTitle = songDto.title,
                                    SongLength = songDto.length
                                };
                                _context.Songs.Add(newSong);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();

                return Ok("Data imported successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
