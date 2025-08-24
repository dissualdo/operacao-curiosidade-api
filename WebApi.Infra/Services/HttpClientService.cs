using System.Text;
using System.Text.Json; 
using WebApi.Models.Services;

namespace WebApi.Infra.Services
{
    /// <summary>
    /// Provides generic methods for sending HTTP requests (GET, POST, PUT, DELETE),
    /// supporting optional headers and JSON serialization/deserialization.
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientService"/> class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> injected via dependency injection.</param>
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return await ProcessResponse<T>(response);
        }

        /// <inheritdoc />
        public async Task<T?> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return await ProcessResponse<T>(response);
        }

        public async Task<HttpResponseMessage> PostAsHttpResponseAsync(string url, object data, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            return await _httpClient.SendAsync(request);
        }


        /// <inheritdoc />
        public async Task<T?> PutAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return await ProcessResponse<T>(response);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(string url, Dictionary<string, string>? headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Adds custom headers to the HTTP request.
        /// </summary>
        /// <param name="request">The HTTP request to modify.</param>
        /// <param name="headers">The headers to include in the request.</param>
        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers == null) return;

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Processes the HTTP response and deserializes its content into the specified type.
        /// </summary>
        /// <typeparam name="T">The expected response type.</typeparam>
        /// <param name="response">The HTTP response received from the request.</param>
        /// <returns>The deserialized object or <c>null</c> if the response is not successful or contains no data.</returns>
        private async Task<T?> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
            {
                object result = json;
                return (T)result;
            }

            if (string.IsNullOrWhiteSpace(json)) return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
