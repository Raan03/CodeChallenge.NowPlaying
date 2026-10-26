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

        public async Task SaveFavoritesAsync(List<MovieResponse> movies, CancellationToken cancellationToken = default)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(movies);
                await _jSRuntime.InvokeVoidAsync("localStorage.setItem", _localStorageKeyFavorites, json, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving favorites to local storage: {ex.Message}");
            }
        }
        public async Task<List<MovieResponse>> GetFavoritesAsync(CancellationToken cancellationToken = default)
        {
            List<MovieResponse> movies = new List<MovieResponse>();

            try
            {
                var result = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", _localStorageKeyFavorites, cancellationToken);

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
        public async Task AddFavoriteAsync(MovieResponse movieResponse, CancellationToken cancellationToken = default)
        {
            var favorites = await GetFavoritesAsync(cancellationToken);
            if (!favorites.Exists(m => m.Id == movieResponse.Id))
            {
                favorites.Add(movieResponse);
                await SaveFavoritesAsync(favorites, cancellationToken);
            }
        }
    }
}
