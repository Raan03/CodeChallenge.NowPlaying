using CodeChallenge.NowPlaying.Models;

namespace CodeChallenge.NowPlaying.Services.Contracts
{
    public interface ILocalStorageService
    {
        Task<List<MovieResponse>> GetFavoritesAsync(CancellationToken cancellationToken = default);
        Task SaveFavoritesAsync(List<MovieResponse> movies, CancellationToken cancellationToken = default);
        Task AddFavoriteAsync(MovieResponse movieResponse, CancellationToken cancellationToken = default);
    }
}