namespace Flamingo.Fishes
{
    /// <summary>
    /// Await incoming status
    /// </summary>
    public enum AwaitableStatus
    {
        /// <summary>
        /// Operation is running yet.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Operation has been Succeeded
        /// </summary>
        Succeeded,

        /// <summary>
        /// Operation canceled
        /// </summary>
        Cancelled,

        /// <summary>
        /// Operation timed out!
        /// </summary>
        TimedOut,
    }
}
