using Flamingo.Condiments;

namespace Flamingo.Filters.SharedFilters
{
    /// <summary>
    /// Filter updates sent by a bot
    /// </summary>
    /// <typeparam name="T">Update type</typeparam>
    public class BotFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter updates sent by a bot
        /// </summary>
        public BotFilter() 
            : base(x=> x.Sender?.IsBot?? false)
        { }
    }
}
