using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Fishes.Awaitables
{
    /// <summary>
    /// Awaits for a text respond from specified user in private chat only!
    /// </summary>
    public sealed class AwaitInComingText : SimpleAwaitableInComing<Message>
    {
        /// <summary>
        /// Awaits for a text respond from specified user in private chat only!
        /// </summary>
        /// <param name="userId">User id to look for respond from it.</param>
        /// <param name="timeOut">Time in second that should wait for respond</param>
        /// <param name="cancellationToken">Custom cancellation token to cancel await</param>
        public AwaitInComingText(
            long userId,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = null) 

            : base(new FromUsersFilter<Message>(userId) &
                   new PrivateFilter<Message>() &
                   new MessageTypeFilter(MessageType.Text),
                   
                   timeOut: timeOut,
                   cancellationToken: cancellationToken)
        { }
    }
}
