namespace AuthyDotNet.AuthyHttpClientResponses
{
    /// <summary>
    /// Status of an Authy Response
    /// </summary>
    public enum AuthyStatus
    {
        /// <summary>
        /// Successful Response (200)
        /// </summary>
        Success,
        /// <summary>
        /// Bad Request (400)
        /// </summary>
        BadRequest,
        /// <summary>
        /// Unauthorized Request (401)
        /// </summary>
        Unauthorized,
        /// <summary>
        /// Service Unavailable Request (503)
        /// </summary>
        ServiceUnavailable
    }
}
