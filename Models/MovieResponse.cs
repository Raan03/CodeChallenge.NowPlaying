namespace CodeChallenge.NowPlaying.Models
{
    public class MovieResponse
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; } = string.Empty;
        public List<int> GenreIds { get; set; } = [];
        public int Id { get; set; }
        public string OriginalLanguage { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public float Popularity { get; set; }
        public string PosterPath { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Video { get; set; }
        public float VoteAverage { get; set; }
        public int VoteCount { get; set; }
    }
}
