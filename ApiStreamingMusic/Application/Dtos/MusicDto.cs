namespace ApiStreamingMusic.Application.Dtos
{
    public class MusicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public MusicDto(Guid id, string title, string artist, string genre, int year)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            Year = year;
        }

        public MusicDto()
        {
            
        }
    }
}
