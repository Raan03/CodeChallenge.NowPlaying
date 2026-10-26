using CodeChallenge.NowPlaying.Models;

namespace CodeChallenge.NowPlaying.Services.Contracts
{
    public interface ILocalStorageService
    {
        /// <summary>
        /// Retrieves the Favorites from Local Storage
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<MovieResponse>> GetFavoritesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Saves the favorites to LocalStorage
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveFavoritesAsync(List<MovieResponse> movies, CancellationToken cancellationToken = default);
        /// <summary>
        /// Add a movie to the favorites list in localStorage
        /// </summary>
        /// <param name="movieResponse"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddFavoriteAsync(MovieResponse movieResponse, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes a movie from favorites list in localStorage
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveFavoriteAsync(MovieResponse movie, CancellationToken cancellationToken = default);
        /// <summary>
        /// Is a certain movieId in our favoriteslist?
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsFavoriteAsync(int movieId, CancellationToken cancellationToken = default);
    }
}