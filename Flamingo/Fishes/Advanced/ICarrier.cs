namespace Flamingo.Fishes.Advanced
{
    /// <summary>
    /// Helper interface to make advanced incoming handlers
    /// </summary>
    /// <typeparam name="T">This is the adv handler to build</typeparam>
    public interface ICarrier<T>
    {
        /// <summary>
        /// The carrier of this handler
        /// </summary>
        public Carrier<T> Carrier { get; }

        /// <summary>
        /// Setup callback function and hander requirements after filters matched
        /// and before sending to callback function
        /// </summary>
        public void SetupFish();

        /// <summary>
        /// When handling is done, clear what is clear able
        /// </summary>
        public void ClearFish();
    }
}
