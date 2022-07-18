namespace Knock
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A client to manage requests to the Knock API.
    /// </summary>
    public class KnockClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KnockClient"/> class.
        /// </summary>
        /// <param name="options">Parameters to create the client with.</param>
        public KnockClient(KnockOptions options)
        {
            if (options.ApiKey == null || options.ApiKey.Length == 0)
            {
                throw new ArgumentException("API Key is required", nameof(options.ApiKey));
            }

            ApiBaseURL = options.ApiBaseURL ?? DefaultApiBaseURL;
            ApiKey = options.ApiKey;
            HttpClient = options.HttpClient ?? DefaultHttpClient();

            // Initialize resources
            Users = new UsersResource(this);
            Workflows = new WorkflowResource(this);
            // Preferences is deprecated and will be removed
            Preferences = new PreferencesResource(this);
            Objects = new ObjectsResource(this);
            BulkOperations = new BulkOperationsResource(this);
            Messages = new MessagesResource(this);
        }

        /// <summary>
        /// Describes the .NET SDK version.
        /// </summary>
        public static string SdkVersion => "0.1.0";

        /// <summary>
        /// Default timeout for HTTP requests.
        /// </summary>
        public static TimeSpan DefaultTimeout => TimeSpan.FromSeconds(60);

        /// <summary>
        /// Default base URL for the Knock API.
        /// </summary>
        public static string DefaultApiBaseURL => "https://api.knock.app/v1";

        /// <summary>
        /// The base URL for the Knock API.
        /// </summary>
        public string ApiBaseURL { get; }

        /// <summary>
        /// The API key used to authenticate requests to the Knock API.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Access to User methods
        /// </summary>
        public UsersResource Users { get; }

        /// <summary>
        /// Access to Workflow methods
        /// </summary>
        public WorkflowResource Workflows { get; }

        /// <summary>
        /// Access to Preference methods (deprecated)
        /// </summary>
        public PreferencesResource Preferences { get; }

        /// <summary>
        /// Access to Object methods
        /// </summary>
        public ObjectsResource Objects { get; }

        /// <summary>
        /// Access to BulkOperations methods
        /// </summary>
        public BulkOperationsResource BulkOperations { get; }

        /// <summary>
        /// Access to Message methods
        /// </summary>
        public MessagesResource Messages { get; }

        /// <summary>
        /// The client used to make HTTP requests to the Knock API.
        /// </summary>
        private HttpClient HttpClient { get; }

        /// <summary>
        /// Creates a new HTTP client instance.
        /// </summary>
        /// <returns>An instance of the HTTP client to make requests.</returns>
        public HttpClient DefaultHttpClient()
        {
            return new HttpClient
            {
                Timeout = DefaultTimeout,
            };
        }

        /// <summary>
        /// Makes a request to the Knock API.
        /// </summary>
        /// <param name="request">The request to make to the Knock API.</param>
        /// <param name="cancellationToken">A token used to cancel the request.</param>
        /// <returns>The response from the Knock API.</returns>
        public async Task<HttpResponseMessage> MakeRawAPIRequest(
            KnockRequest request,
            CancellationToken cancellationToken = default)
        {
            var requestMessage = CreateHttpRequestMessage(request);

            return await HttpClient.SendAsync(requestMessage, cancellationToken);
        }

        /// <summary>
        /// Makes a request to the Knock API and parses the JSON response.
        /// </summary>
        /// <typeparam name="T">The return type from the request.</typeparam>
        /// <param name="request">The request to make to the Knock API.</param>
        /// <param name="cancellationToken">A token used to cancel the request.</param>
        /// <returns>The response from the Knock API.</returns>
        public async Task<T> MakeAPIRequest<T>(
            KnockRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRawAPIRequest(request, cancellationToken).ConfigureAwait(false);

            var reader = new StreamReader(
                await response.Content.ReadAsStreamAsync().ConfigureAwait(false));
            var data = await reader.ReadToEndAsync().ConfigureAwait(false);
            return RequestUtilities.FromJson<T>(data);
        }

        private HttpRequestMessage CreateHttpRequestMessage(KnockRequest request)
        {
            Uri uri = this.BuildUri(request);
            HttpContent content = null;

            if (request.Method != HttpMethod.Get)
            {
                content = RequestUtilities.CreateHttpContent(request);
            }

            var userAgentString = $"knock-dotnet/{SdkVersion}";
            var requestMessage = new HttpRequestMessage(request.Method, uri);

            requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.ApiKey);


            requestMessage.Headers.TryAddWithoutValidation("User-Agent", userAgentString);
            if (request.KnockHeaders != null)
            {
                foreach (var header in request.KnockHeaders)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            requestMessage.Content = content;
            return requestMessage;
        }

        private Uri BuildUri(KnockRequest request)
        {
            var builder = new StringBuilder();
            var options = request.Options;
            builder.Append(ApiBaseURL);
            builder.Append(request.Path);

            if (request.Method != HttpMethod.Post && options != null)
            {
                var queryParameters = RequestUtilities.CreateQueryString(options);
                if (queryParameters != null && queryParameters.Length > 0)
                {
                    builder.Append("?");
                    builder.Append(queryParameters);
                }
            }

            return new Uri(builder.ToString());
        }
    }
}
