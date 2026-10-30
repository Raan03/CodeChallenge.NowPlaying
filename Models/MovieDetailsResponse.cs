namespace CodeChallenge.NowPlaying.Models
{

    public class MovieDetailsResponse : IMovieWithImages
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; } = string.Empty;
        public int Budget { get; set; }
        public List<Genre> Genres { get; set; } = [];
        public string Homepage { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ImdbId { get; set; } = string.Empty;
        public string[] OriginCountry { get; set; } = [];
        public string OriginalLanguage { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public float Popularity { get; set; }
        public string PosterPath { get; set; } = string.Empty;
        public List<ProductionCompanies> ProductionCompanies { get; set; } = [];
        public List<ProductionCountries> ProductionCountries { get; set; } = [];
        public string ReleaseDate { get; set; } = string.Empty;
        public int Revenue { get; set; }
        public int Runtime { get; set; }
        public List<SpokenLanguages> SpokenLanguages { get; set; } = [];
        public string Status { get; set; } = string.Empty;
        public string Tagline { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Video { get; set; }
        public float VoteAverage { get; set; }
        public int VoteCount { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductionCompanies
    {
        public int Id { get; set; }
        public string LogoPath { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OriginCountry { get; set; } = string.Empty;
    }

    public class ProductionCountries
    {
        public string Name { get; set; } = string.Empty;
    }

    public class SpokenLanguages
    {
        public string EnglishName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}
