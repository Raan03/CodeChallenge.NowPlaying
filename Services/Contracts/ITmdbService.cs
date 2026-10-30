using CodeChallenge.NowPlaying.Models;

namespace CodeChallenge.NowPlaying.Services.Contracts
{
    public interface ITmdbService
    {
        /// <summary>
        /// Gets details for the now playing movies
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<NowPlayingResponseModel> GetNowPlayingMoviesAsync(int pageNumber, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets a list of popular movies
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<NowPlayingResponseModel> GetPopularMoviesAsync(int pageNumber, CancellationToken cancellationToken = default);
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        Task<NowPlayingResponseModel> SearchAsync(string queryString, CancellationToken cancellationToken = default);
        /// <summary>
        /// Get details for a movie by MovieId
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        Task<MovieDetailsResponse> GetMovieDetailsByIdAsync(int movieId, CancellationToken cancellation = default);
        /// <summary>
        /// Gets a movie trailer for a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<VideoResult?> GetMovieTrailerAsync(int movieId, CancellationToken cancellationToken = default);
    }
}