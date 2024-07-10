using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MusicLibrary.Entities
{
    public class AlbumEntity
    {
        [Key]
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        [JsonIgnore]
        public ArtistEntity Artist { get; set; }

        public string AlbumTitle { get; set; }
        public string AlbumDescription { get; set; }
    }
}
