namespace MusicLibrary.Dtos
{
    public class ArtistDto
    {
        public string name { get; set; }
        public List<AlbumDto> albums { get; set; }
    }
}
