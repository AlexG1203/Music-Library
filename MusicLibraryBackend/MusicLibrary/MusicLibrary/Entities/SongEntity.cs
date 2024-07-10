using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MusicLibrary.Entities
{
    public class SongEntity
    {
        [Key]
        public int SongId { get; set; }
        public int AlbumId { get; set; }

        [ForeignKey("AlbumId")]
        [JsonIgnore]
        public AlbumEntity Album { get; set; }

        public string SongTitle { get; set; }
        public string SongLength { get; set; }
    }
}
