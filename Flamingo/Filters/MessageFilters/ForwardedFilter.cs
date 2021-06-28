using Flamingo.Condiments;
using Telegram.Bot.Types;

namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Filter messages that are forwarded.
    /// </summary>
    public class ForwardedFilter : FilterBase<ICondiment<Message>>
    {
        /// <summary>
        /// Filter messages that are forwarded.
        /// </summary>
        public ForwardedFilter() : 
            base(x=> x.InComing.ForwardFrom   != null ||
                 x.InComing.ForwardFromChat   != null ||
                 x.InComing.ForwardSenderName != null ||
                 x.InComing.ForwardSignature  != null )
        {
        }
    }
}
