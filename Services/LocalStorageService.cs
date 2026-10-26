using CodeChallenge.NowPlaying.Models;
using CodeChallenge.NowPlaying.Services.Contracts;
using Microsoft.JSInterop;

namespace CodeChallenge.NowPlaying.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly string _localStorageKeyFavorites = "favorites";
        private readonly IJSRuntime _jSRuntime;

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        /// <summary>
        /// Saves the favorites to LocalStorage
        /// </summary>
        /// <param name="movies"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveFavoritesAsync(List<MovieResponse> movies, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(movies);
                await _jSRuntime.InvokeVoidAsync(identifier: "localStorage.setItem", cancellationToken: cancellationToken, args: [_localStorageKeyFavorites, json]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving favorites to local storage: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves the Favorites from Local Storage
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<MovieResponse>> GetFavoritesAsync(CancellationToken cancellationToken = default)
        {
            List<MovieResponse> movies = new List<MovieResponse>();

            try
            {
                var result = await _jSRuntime.InvokeAsync<string>(identifier: "localStorage.getItem", cancellationToken: cancellationToken, args: [_localStorageKeyFavorites] );

                if (!string.IsNullOrEmpty(result))
                {
                    movies = System.Text.Json.JsonSerializer.Deserialize<List<MovieResponse>>(result) ?? new List<MovieResponse>();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving favorites from local storage: {ex.Message}");
                await SaveFavoritesAsync(new List<MovieResponse>(), cancellationToken);
            }

            return movies;
        }

        /// <summary>
        /// Add a movie to the favorites list in localStorage
        /// </summary>
        /// <param name="movieResponse"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AddFavoriteAsync(MovieResponse movieResponse, CancellationToken cancellationToken = default)
        {
            var favorites = await GetFavoritesAsync(cancellationToken);
            if (!favorites.Exists(m => m.Id == movieResponse.Id))
            {
                favorites.Add(movieResponse);
                await SaveFavoritesAsync(favorites, cancellationToken);
            }
        }

        /// <summary>
        /// Removes a movie from favorites list in localStorage
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveFavoriteAsync(MovieResponse movie, CancellationToken cancellationToken = default)
        {
            var favorites = await GetFavoritesAsync(cancellationToken);
            favorites = favorites.Where(d => d.Id != movie.Id).ToList();
            await SaveFavoritesAsync(favorites, cancellationToken);
        }

        /// <summary>
        /// Is a certain movieId in our favoriteslist?
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> IsFavoriteAsync(int movieId, CancellationToken cancellationToken = default)
        {
            var favorites = await GetFavoritesAsync(cancellationToken);
            
            return favorites.Any(m => m.Id == movieId);
        }
    }
}
