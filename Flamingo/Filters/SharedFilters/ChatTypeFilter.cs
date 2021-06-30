using Flamingo.Condiments;
using Flamingo.Helpers.Types.Enums;

namespace Flamingo.Filters.SharedFilters
{
    /// <summary>
    /// Filter updates based on chat type - private, group ...
    /// </summary>
    /// <typeparam name="T">Update type</typeparam>
    public class ChatTypeFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter updates based on chat type - private, group ...
        /// </summary>
        public ChatTypeFilter(FlamingoChatType chatType) 
            : base(x=> chatType.HasFlag(x.FlamingoChatType))
        { }
    }
}
