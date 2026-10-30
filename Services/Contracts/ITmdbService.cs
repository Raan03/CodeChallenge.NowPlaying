using CodeChallenge.NowPlaying.Models;

namespace CodeChallenge.NowPlaying.Services.Contracts
{
    public interface ITmdbService
    {
        Task<NowPlayingResponseModel> GetNowPlayingMoviesAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<NowPlayingResponseModel> GetPopularMoviesAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<NowPlayingResponseModel> SearchAsync(string queryString, CancellationToken cancellationToken = default);
    }
}