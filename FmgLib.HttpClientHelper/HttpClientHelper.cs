using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FmgLib.HttpClientHelper;

public static class HttpClientHelper
{
    public static ClientResponse Send(string serviceUrl, HttpMethod method, string contentModel, ClientContentType contentType, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers)
    {
        try
        {
            if (string.IsNullOrEmpty(contentModel))
                return Send(serviceUrl, method, null, queryParams, authModel, headers);

            var content = new StringContent(contentModel, Encoding.UTF8, contentType == ClientContentType.Json ? "application/json" : "application/xml");

            return Send(serviceUrl, method, content, queryParams, authModel, headers);
        }
        catch (Exception ex)
        {
            return new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Header = null,
                IsSuccess = false,
                ResponseStr = string.Empty,
                ErrorMessage = ex.Message
            };
        }
    }

    public static ClientResponse Send(string serviceUrl, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers)
    {
        try
        {
            using HttpClient _httpClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, serviceUrl);

            if (queryParams != null && queryParams.Count > 0)
            {
                var queryString = new StringBuilder("?");

                foreach (var queryParam in queryParams)
                {
                    if (queryString.Length > 1)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append($"{Uri.EscapeDataString(queryParam.Key)}={Uri.EscapeDataString(queryParam.Value)}");
                }

                httpRequestMessage.RequestUri = new Uri(httpRequestMessage.RequestUri + queryString.ToString());
            }

            if (headers != null && headers.Length > 0)
            {
                foreach (var (headerName, headerValue) in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
                }
            }

            if (authModel != null)
            {
                string credentials = $"{authModel.UserName}:{authModel.Password}";
                byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
                string base64Credentials = Convert.ToBase64String(credentialsBytes);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            }

            httpRequestMessage.Content = content;

            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);

            return new ClientResponse
            {
                StatusCode = response.StatusCode,
                Header = response.Content.Headers,
                IsSuccess = response.IsSuccessStatusCode,
                ResponseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                ErrorMessage = null
            };
        }
        catch (Exception ex)
        {
            return new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Header = null,
                IsSuccess = false,
                ResponseStr = string.Empty,
                ErrorMessage = ex.Message
            };
        }
    }

    public static async Task<ClientResponse> SendAsync(string serviceUrl, HttpMethod method, string contentModel, ClientContentType contentType, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers)
    {
        try
        {
            if (string.IsNullOrEmpty(contentModel))
                return await SendAsync(serviceUrl, method, null, queryParams, authModel, headers);

            var content = new StringContent(contentModel, Encoding.UTF8, contentType == ClientContentType.Json ? "application/json" : "application/xml");

            return await SendAsync(serviceUrl, method, content, queryParams, authModel, headers);
        }
        catch (Exception ex)
        {
            return new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Header = null,
                IsSuccess = false,
                ResponseStr = string.Empty,
                ErrorMessage = ex.Message
            };
        }
    }

    public static async Task<ClientResponse> SendAsync(string serviceUrl, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers)
    {
        try
        {
            using HttpClient _httpClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, serviceUrl);

            if (queryParams != null && queryParams.Count > 0)
            {
                var queryString = new StringBuilder("?");

                foreach (var queryParam in queryParams)
                {
                    if (queryString.Length > 1)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append($"{Uri.EscapeDataString(queryParam.Key)}={Uri.EscapeDataString(queryParam.Value)}");
                }

                httpRequestMessage.RequestUri = new Uri(httpRequestMessage.RequestUri + queryString.ToString());
            }

            if (headers != null && headers.Length > 0)
            {
                foreach (var (headerName, headerValue) in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
                }
            }

            if (authModel != null)
            {
                string credentials = $"{authModel.UserName}:{authModel.Password}";
                byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
                string base64Credentials = Convert.ToBase64String(credentialsBytes);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            }

            httpRequestMessage.Content = content;

            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

            return new ClientResponse
            {
                StatusCode = response.StatusCode,
                Header = response.Content.Headers,
                IsSuccess = response.IsSuccessStatusCode,
                ResponseStr = await response.Content.ReadAsStringAsync(),
                ErrorMessage = null
            };
        }
        catch (Exception ex)
        {
            return new ClientResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Header = null,
                IsSuccess = false,
                ResponseStr = string.Empty,
                ErrorMessage = ex.Message
            };
        }
    }


    public static TResponse? Send<TResponse>(string serviceUrl, HttpMethod method, string contentModel, ClientContentType contentType, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers) where TResponse : class
    {
        try
        {
            if (string.IsNullOrEmpty(contentModel))
                return Send<TResponse>(serviceUrl, method, null, queryParams, authModel, headers);

            var content = new StringContent(contentModel, Encoding.UTF8, contentType == ClientContentType.Json ? "application/json" : "application/xml");

            return Send<TResponse>(serviceUrl, method, content, queryParams, authModel, headers);
        }
        catch (Exception)
        {
            return default!;
        }
    }

    public static TResponse? Send<TResponse>(string serviceUrl, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers) where TResponse : class
    {
        try
        {
            using HttpClient _httpClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, serviceUrl);

            if (queryParams != null && queryParams.Count > 0)
            {
                var queryString = new StringBuilder("?");

                foreach (var queryParam in queryParams)
                {
                    if (queryString.Length > 1)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append($"{Uri.EscapeDataString(queryParam.Key)}={Uri.EscapeDataString(queryParam.Value)}");
                }

                httpRequestMessage.RequestUri = new Uri(httpRequestMessage.RequestUri + queryString.ToString());
            }

            if (headers != null && headers.Length > 0)
            {
                foreach (var (headerName, headerValue) in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
                }
            }

            if (authModel != null)
            {
                string credentials = $"{authModel.UserName}:{authModel.Password}";
                byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
                string base64Credentials = Convert.ToBase64String(credentialsBytes);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            }

            httpRequestMessage.Content = content;

            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);

            var clientResponse = new ClientResponse
            {
                StatusCode = response.StatusCode,
                Header = response.Content.Headers,
                IsSuccess = response.IsSuccessStatusCode,
                ResponseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                ErrorMessage = null
            };

            return clientResponse.TryGenerateModel<TResponse>(clientResponse.ContentType);
        }
        catch (Exception)
        {
            return default!;
        }
    }

    public static async Task<TResponse?> SendAsync<TResponse>(string serviceUrl, HttpMethod method, string contentModel, ClientContentType contentType, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers) where TResponse : class
    {
        try
        {
            if (string.IsNullOrEmpty(contentModel))
                return await SendAsync<TResponse>(serviceUrl, method, null, queryParams, authModel, headers);

            var content = new StringContent(contentModel, Encoding.UTF8, contentType == ClientContentType.Json ? "application/json" : "application/xml");

            return await SendAsync<TResponse>(serviceUrl, method, content, queryParams, authModel, headers);
        }
        catch (Exception)
        {
            return default!;
        }
    }

    public static async Task<TResponse?> SendAsync<TResponse>(string serviceUrl, HttpMethod method, HttpContent? content = null, Dictionary<string, string>? queryParams = null, BasicAuthModel? authModel = null, params (string HeaderName, string HeaderValue)[] headers) where TResponse : class
    {
        try
        {
            using HttpClient _httpClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, serviceUrl);

            if (queryParams != null && queryParams.Count > 0)
            {
                var queryString = new StringBuilder("?");

                foreach (var queryParam in queryParams)
                {
                    if (queryString.Length > 1)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append($"{Uri.EscapeDataString(queryParam.Key)}={Uri.EscapeDataString(queryParam.Value)}");
                }

                httpRequestMessage.RequestUri = new Uri(httpRequestMessage.RequestUri + queryString.ToString());
            }

            if (headers != null && headers.Length > 0)
            {
                foreach (var (headerName, headerValue) in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
                }
            }

            if (authModel != null)
            {
                string credentials = $"{authModel.UserName}:{authModel.Password}";
                byte[] credentialsBytes = Encoding.UTF8.GetBytes(credentials);
                string base64Credentials = Convert.ToBase64String(credentialsBytes);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
            }

            httpRequestMessage.Content = content;

            HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);

            var clientResponse = new ClientResponse
            {
                StatusCode = response.StatusCode,
                Header = response.Content.Headers,
                IsSuccess = response.IsSuccessStatusCode,
                ResponseStr = await response.Content.ReadAsStringAsync(),
                ErrorMessage = null
            };

            return clientResponse.TryGenerateModel<TResponse>(clientResponse.ContentType);
        }
        catch (Exception)
        {
            return default!;
        }
    }
}
