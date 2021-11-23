namespace Knock
{
    /// <summary>
    /// A serializable resouce used to represent JSON
    /// </summary>
    public class BaseResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResource"/> class.
        /// </summary>
        /// <param name="client">A client used to make requests to Knock.</param>
        protected BaseResource(KnockClient client)
        {
            Client = client;
        }

        /// <summary>
        /// The client used to make requests to Knock.
        /// </summary>
        protected KnockClient Client { get; set; }
    }
}
