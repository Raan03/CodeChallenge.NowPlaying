namespace CodeChallenge.NowPlaying.Models
{

    public class NowPlayingResponseModel
    {
        public int Page { get; set; }
        public List<MovieResponse> Results { get; set; } = [];
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

}
