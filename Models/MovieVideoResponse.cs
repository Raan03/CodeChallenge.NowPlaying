namespace CodeChallenge.NowPlaying.Models
{

    public class MovieVideoResponse
    {
        public int Id { get; set; }
        public List<VideoResult> Results { get; set; } = [];
    }

    public class VideoResult
    {
        public string Name { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Site { get; set; } = string.Empty;
        public int Size { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
        public string Id { get; set; } = string.Empty;
    }

}
