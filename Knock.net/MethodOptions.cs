namespace Knock
{
    using System.Net.Http;

    /// <summary>
    /// Describes options for any Knock API method.
    /// </summary>
    public class MethodOptions
    {
        /// <summary>
        /// The Idempotency Key to use for the request.
        /// </summary>
        public string IdempotencyKey { get; set; }

    }
}
