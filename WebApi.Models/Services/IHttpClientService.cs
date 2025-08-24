namespace WebApi.Models.Services
{
    /// <summary>
    /// Defines a generic HTTP client service for executing RESTful requests.
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// Sends a GET request to the specified URL and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="url">The target URL.</param>
        /// <param name="headers">Optional request headers.</param>
        /// <returns>The deserialized response object, or null if the request fails.</returns>
        Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a POST request with a JSON body and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="url">The target URL.</param>
        /// <param name="data">The object to serialize as the request body.</param>
        /// <param name="headers">Optional request headers.</param>
        /// <returns>The deserialized response object, or null if the request fails.</returns>
        Task<T?> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a PUT request with a JSON body and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="url">The target URL.</param>
        /// <param name="data">The object to serialize as the request body.</param>
        /// <param name="headers">Optional request headers.</param>
        /// <returns>The deserialized response object, or null if the request fails.</returns>
        Task<T?> PutAsync<T>(string url, object data, Dictionary<string, string>? headers = null);

        /// <summary>
        /// Sends a DELETE request to the specified URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="headers">Optional request headers.</param>
        /// <returns><c>true</c> if the request was successful; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(string url, Dictionary<string, string>? headers = null);
    }
}
