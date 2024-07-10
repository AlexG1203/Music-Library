using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Entities
{
    public class ArtistEntity
    {
        [Key]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
