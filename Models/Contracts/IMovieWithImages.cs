namespace CodeChallenge.NowPlaying.Models.Contracts
{
    public interface IMovieWithImages
    {
        public string BackdropPath { get; set; }
        public string PosterPath { get; set; }

    }
}
