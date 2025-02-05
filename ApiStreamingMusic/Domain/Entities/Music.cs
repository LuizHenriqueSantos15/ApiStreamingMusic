namespace ApiStreamingMusic.Domain.Entities
{
    public class Music
    {
        public Guid Id { get; private set; }
        public string Title { get;  set; }
        public string Artist { get;  set; }
        public string Genre { get;  set; }

        public string Url { get; set; }
        public int Year { get;  set; }

        public Music()
        {
            
        }

        public Music(string title, string artist, string genre, int year, string fileUrl)
        {
            Id = Guid.NewGuid();
            Title = title;
            Artist = artist;
            Genre = genre;
            Year = year;
            Url = fileUrl;
        }
    }
}
