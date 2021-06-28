using Flamingo.Condiments;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Filters.SharedFilters
{
    /// <summary>
    /// Filter updates from channels
    /// </summary>
    /// <typeparam name="T">Update type</typeparam>
    public class ChannelFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter updates from channels
        /// </summary>
        public ChannelFilter()
            : base(x =>
            {
                if (x.Chat != null) return false;

                return x.Chat.Type == ChatType.Channel;
            })
        { }
    }
}
