namespace MusicLibrary.Dtos
{
    public class AlbumDto
    {
        public int ArtistId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<SongDto> songs { get; set; }
    }
}
