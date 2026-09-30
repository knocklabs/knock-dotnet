namespace Knock
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
            Tenants = new TenantsResource(this);
            BulkOperations = new BulkOperationsResource(this);
            Messages = new MessagesResource(this);
            Schedules = new SchedulesResource(this);
            Audiences = new AudiencesResource(this);
            WorkflowRecipientRuns = new WorkflowRecipientRunsResource(this);
            Channels = new ChannelsResource(this);
            Providers = new ProvidersResource(this);
            Integrations = new IntegrationsResource(this);
        }

        /// <summary>
        /// Describes the .NET SDK version.
        /// </summary>
        public static string SdkVersion => "0.2.0";

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
        /// Access to Tenant methods
        /// </summary>
        public TenantsResource Tenants { get; }

        /// <summary>
        /// Access to BulkOperations methods
        /// </summary>
        public BulkOperationsResource BulkOperations { get; }

        /// <summary>
        /// Access to Message methods
        /// </summary>
        public MessagesResource Messages { get; }

        /// <summary>
        /// Access to Schedule methods
        /// </summary>
        public SchedulesResource Schedules { get; }

        /// <summary>
        /// Access to Audience methods
        /// </summary>
        public AudiencesResource Audiences { get; }

        /// <summary>
        /// Access to Workflow Recipient Run methods
        /// </summary>
        public WorkflowRecipientRunsResource WorkflowRecipientRuns { get; }

        /// <summary>
        /// Access to Channel methods
        /// </summary>
        public ChannelsResource Channels { get; }

        /// <summary>
        /// Access to chat Provider (Slack, Microsoft Teams) methods
        /// </summary>
        public ProvidersResource Providers { get; }

        /// <summary>
        /// Access to reverse ETL Integration (Census, Hightouch) methods
        /// </summary>
        public IntegrationsResource Integrations { get; }

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
            response.EnsureSuccessStatusCode();

            var reader = new StreamReader(
                await response.Content.ReadAsStreamAsync().ConfigureAwait(false));
            var data = await reader.ReadToEndAsync().ConfigureAwait(false);

            return RequestUtilities.FromJson<T>(data);
        }

        /// <summary>
        /// Makes a request to the Knock API that does not return a response body.
        /// </summary>
        /// <param name="request">The request to make to the Knock API.</param>
        /// <param name="cancellationToken">A token used to cancel the request.</param>
        /// <returns>A task that completes when the request succeeds.</returns>
        public async Task MakeAPIRequest(
            KnockRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await MakeRawAPIRequest(request, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
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
            builder.Append(ApiBaseURL);
            builder.Append(request.Path);

            var queryParts = new List<string>();

            if (request.Method == HttpMethod.Get && request.Options != null)
            {
                queryParts.Add(RequestUtilities.CreateQueryString(request.Options));
            }

            if (request.QueryParams != null)
            {
                queryParts.Add(RequestUtilities.CreateQueryString(request.QueryParams));
            }

            var queryString = string.Join("&", queryParts.Where(part => !string.IsNullOrEmpty(part)));
            if (queryString.Length > 0)
            {
                builder.Append("?");
                builder.Append(queryString);
            }

            return new Uri(builder.ToString());
        }
    }
}
