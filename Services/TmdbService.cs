using CodeChallenge.NowPlaying.Models;
using System.Text.Json;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using CodeChallenge.NowPlaying.Services.Contracts;

namespace CodeChallenge.NowPlaying.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        private readonly string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var apiKey = configuration["TMDB:ApiKey"] ?? throw new InvalidOperationException("TMDB API key is not configured."); ;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<NowPlayingResponseModel> GetNowPlayingMoviesAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            string url = "https://api.themoviedb.org/3/movie/now_playing?region=BE&language=nl-BE";

            var response = await _httpClient.GetFromJsonAsync<NowPlayingResponseModel>($"{url}&page={pageNumber}", jsonOptions, cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch now playing movies.");

            PopulatePosterPaths(response);

            return response;
        }

        public async Task<NowPlayingResponseModel> GetPopularMoviesAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            string url = "https://api.themoviedb.org/3/movie/popular?region=BE&language=nl-BE";

            var response = await _httpClient.GetFromJsonAsync<NowPlayingResponseModel>($"{url}&page={pageNumber}", jsonOptions, cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch popular movies.");

            PopulatePosterPaths(response);

            return response;
        }

        private void PopulatePosterPaths(NowPlayingResponseModel response)
        {
            foreach (var movie in response.Results)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    movie.PosterPath = $"{imageBaseUrl}{movie.PosterPath}";
                }
                else
                {
                    movie.PosterPath = "/img/poster.png"; // Use a placeholder image if no poster is available
                }
            }
        }
    }
}
