using CodeChallenge.NowPlaying.Models;
using System.Text.Json;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using CodeChallenge.NowPlaying.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using CodeChallenge.NowPlaying.Models.Contracts;

namespace CodeChallenge.NowPlaying.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions jsonOptions = new()
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

            var response = await _httpClient.GetFromJsonAsync<NowPlayingResponseModel>(requestUri: $"{url}&page={pageNumber}", options: jsonOptions, cancellationToken: cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch now playing movies.");

            PopulatePosterPaths(response.Results);

            return response;
        }

        public async Task<NowPlayingResponseModel> GetPopularMoviesAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            string url = "https://api.themoviedb.org/3/movie/popular?region=BE&language=nl-BE";

            var response = await _httpClient.GetFromJsonAsync<NowPlayingResponseModel>(requestUri: $"{url}&page={pageNumber}", options: jsonOptions, cancellationToken: cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch popular movies.");

            PopulatePosterPaths(response.Results);

            return response;
        }

        private void PopulatePosterPaths(IEnumerable<MovieResponse> results)
        {
            foreach (var result in results)
            {
                PopulatePosterPaths(result);
            }
        }

        private void PopulatePosterPaths(IMovieWithImages response)
        {
            if (!string.IsNullOrEmpty(response.PosterPath))
            {
                response.PosterPath = $"{imageBaseUrl}{response.PosterPath}";
            }
            else
            {
                response.PosterPath = "/img/poster.png"; // Use a placeholder image if no poster is available
            }
        }

        private void PopulateBackdropPaths(IMovieWithImages response)
        {
            if (!string.IsNullOrEmpty(response.PosterPath))
            {
                response.BackdropPath = $"{imageBaseUrl}{response.BackdropPath}";
            }
            else
            {
                response.BackdropPath = "/img/backdrop.jpg"; // Use a placeholder image if no poster is available
            }
        }

        public async Task<NowPlayingResponseModel> SearchAsync(string queryString, CancellationToken cancellationToken = default)
        {
            string url = "https://api.themoviedb.org/3/search/movie?region=BE&language=nl-BE";

            var response = await _httpClient.GetFromJsonAsync<NowPlayingResponseModel>(requestUri: $"{url}&query={queryString}", options: jsonOptions, cancellationToken: cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch search results.");

            PopulatePosterPaths(response.Results);

            return response;

        }

        public async Task<MovieDetailsResponse> GetMovieDetailsByIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}";

            var response = await _httpClient.GetFromJsonAsync<MovieDetailsResponse>(requestUri: url, options: jsonOptions, cancellationToken: cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch movie details");

            PopulatePosterPaths(response);
            PopulateBackdropPaths(response);

            return response;
        }

        public async Task<VideoResult?> GetMovieTrailerAsync(int movieId, CancellationToken cancellationToken = default)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/videos?language=en-US";

            var response = await _httpClient.GetFromJsonAsync<MovieVideoResponse>(requestUri: url, options: jsonOptions, cancellationToken: cancellationToken) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to fetch movie trailers");

            var result = response?.Results.FirstOrDefault(d => d.Type.Contains("Trailer", StringComparison.OrdinalIgnoreCase) && d.Site.Contains("YouTube", StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}