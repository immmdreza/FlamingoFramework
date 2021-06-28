using Flamingo.Fishes;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo
{
    /// <summary>
    /// Manage all inComing here
    /// </summary>
    public class InComingManager
    {
        #region Messages
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingMessages()
        {
            _inComingMessages = _inComingMessages.OrderBy(x => x.Value)
                .ToDictionary(x=> x.Key, x=> x.Value);
        }

        private Dictionary<IFish<Message>, int> _inComingMessages;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<Message>, int> InComingMessages
        {
            get
            {
                if (_inComingMessages == null)
                    _inComingMessages = new Dictionary<IFish<Message>, int>();

                return _inComingMessages;
            }
        }
        #endregion

        #region CallbackQuery
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingCallbackQuery()
        {
            _inComingCallbackQueries = _inComingCallbackQueries.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<CallbackQuery>, int> _inComingCallbackQueries;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<CallbackQuery>, int> InComingCallbackQueries
        {
            get
            {
                if (_inComingCallbackQueries == null)
                    _inComingCallbackQueries = new Dictionary<IFish<CallbackQuery>, int>();

                return _inComingCallbackQueries;
            }
        }
        #endregion

        #region InlineQuery
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingInlineQuery()
        {
            _inComingInlineQueries = _inComingInlineQueries.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<InlineQuery>, int> _inComingInlineQueries;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<InlineQuery>, int> InComingInlineQueries
        {
            get
            {
                if (_inComingInlineQueries == null)
                    _inComingInlineQueries = new Dictionary<IFish<InlineQuery>, int>();

                return _inComingInlineQueries;
            }
        }
        #endregion

        #region ChatMembers
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingChatMember()
        {
            _inComingChatMembers = _inComingChatMembers.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<ChatMemberUpdated>, int> _inComingChatMembers;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<ChatMemberUpdated>, int> InComingChatMembers
        {
            get
            {
                if (_inComingChatMembers == null)
                    _inComingChatMembers = new Dictionary<IFish<ChatMemberUpdated>, int>();

                return _inComingChatMembers;
            }
        }
        #endregion

        #region ChosenInlineResult
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingInlineResultChosen()
        {
            _inComingInlineResultChosen = _inComingInlineResultChosen.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<ChosenInlineResult>, int> _inComingInlineResultChosen;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<ChosenInlineResult>, int> InComingInlineResultChosen
        {
            get
            {
                if (_inComingInlineResultChosen == null)
                    _inComingInlineResultChosen = new Dictionary<IFish<ChosenInlineResult>, int>();

                return _inComingInlineResultChosen;
            }
        }
        #endregion

        #region Poll
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingPoll()
        {
            _inComingPoll = _inComingPoll.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<Poll>, int> _inComingPoll;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<Poll>, int> InComingPoll
        {
            get
            {
                if (_inComingPoll == null)
                    _inComingPoll = new Dictionary<IFish<Poll>, int>();

                return _inComingPoll;
            }
        }
        #endregion

        #region PollAnswer
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingPollAnswer()
        {
            _inComingPollAnswer = _inComingPollAnswer.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<PollAnswer>, int> _inComingPollAnswer;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<PollAnswer>, int> InComingPollAnswer
        {
            get
            {
                if (_inComingPollAnswer == null)
                    _inComingPollAnswer = new Dictionary<IFish<PollAnswer>, int>();

                return _inComingPollAnswer;
            }
        }
        #endregion

        #region ShippingQuery
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingShippingQuery()
        {
            _inComingShippingQuery = _inComingShippingQuery.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<ShippingQuery>, int> _inComingShippingQuery;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public Dictionary<IFish<ShippingQuery>, int> InComingShippingQuery
        {
            get
            {
                if (_inComingShippingQuery == null)
                    _inComingShippingQuery = new Dictionary<IFish<ShippingQuery>, int>();

                return _inComingShippingQuery;
            }
        }
        #endregion

        #region PreCheckoutQuery
        /// <summary>
        /// Order inComing by handling group
        /// </summary>
        public void OrderInComingPreCheckoutQuery()
        {
            _inComingPreCheckoutQuery = _inComingPreCheckoutQuery.OrderBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<IFish<PreCheckoutQuery>, int> _inComingPreCheckoutQuery;

        /// <summary>
        /// Dictionary of updates and handling group
        /// </summary>
        public Dictionary<IFish<PreCheckoutQuery>, int> InComingPreCheckoutQuery
        {
            get
            {
                if (_inComingPreCheckoutQuery == null)
                    _inComingPreCheckoutQuery = new Dictionary<IFish<PreCheckoutQuery>, int>();

                return _inComingPreCheckoutQuery;
            }
        }
        #endregion
    }
}
