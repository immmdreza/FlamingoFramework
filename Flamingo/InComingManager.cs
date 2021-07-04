using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo
{
    /// <summary>
    /// Manage all inComing here
    /// </summary>
    public class InComingManager
    {
        /// <summary>
        /// Check how many incoming handlers you may added
        /// </summary>
        public int addCount = 0;

        /// <summary>
        /// Get a list of added InComing handlers based on update type
        /// </summary>
        /// <typeparam name="T">Update type</typeparam>
        /// <returns>A Dictionary of inComing handlers and their group</returns>
        public SortedSet<GroupedInComing<T>> GetInComingList<T>()
        {
            var type = typeof(T);
            object result;

            if (type == typeof(Message)) result = InComingMessages;
            else if (type == typeof(CallbackQuery)) result = InComingCallbackQueries;
            else if (type == typeof(InlineQuery)) result = InComingInlineQueries;
            else if (type == typeof(ChosenInlineResult)) result = InComingInlineResultChosen;
            else if (type == typeof(ChatMemberUpdated)) result = InComingChatMembers;
            else if (type == typeof(Poll)) result = InComingPoll;
            else if (type == typeof(PollAnswer)) result = InComingPollAnswer;
            else if (type == typeof(ShippingQuery)) result = InComingShippingQuery;
            else if (type == typeof(PreCheckoutQuery)) result = InComingPreCheckoutQuery;
            else result = null;

            return result as SortedSet<GroupedInComing<T>>;
        }

        private IComparer<GroupedInComing<T>> CreateComparer<T>()
        {
            return Comparer<GroupedInComing<T>>.Create(
                (a, b) => a.CompareTo(b));
        }

        private readonly object _lock = new object();


        #region For Thread Safety

        /// <summary>
        /// Add to handlers thread-safe
        /// </summary>
        public void SafeAdd<T>(GroupedInComing<T> item)
        {
            // Request the lock, and block until it is obtained.
            var groupedIns = GetInComingList<T>();

            lock(groupedIns)
            {
                // When the lock is obtained, add an element.
                var b = groupedIns.Add(item);
            }
        }

        /// <summary>
        /// Add quickly
        /// </summary>
        public bool TryAdd<T>(
            SortedSet<GroupedInComing<T>> groupedIns,
            GroupedInComing<T> item)
        {
            // Request the lock.
            if (Monitor.TryEnter(groupedIns))
            {
                try
                {
                    groupedIns.Add(item);
                }
                finally
                {
                    // Ensure that the lock is released.
                    Monitor.Exit(groupedIns);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Wait to add
        /// </summary>
        public bool TryAdd<T>(
            SortedSet<GroupedInComing<T>> groupedIns,
            GroupedInComing<T> item, int waitTime)
        {
            // Request the lock.
            if (Monitor.TryEnter(groupedIns, waitTime))
            {
                try
                {
                    groupedIns.Add(item);
                }
                finally
                {
                    // Ensure that the lock is released.
                    Monitor.Exit(groupedIns);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Add to handlers thread-safe
        /// </summary>
        public void SafeRemove<T>(
            SortedSet<GroupedInComing<T>> groupedIns,
            GroupedInComing<T> item)
        {
            // Request the lock, and block until it is obtained.
            Monitor.Enter(groupedIns);
            try
            {
                groupedIns.Remove(item);
            }
            finally
            {
                // Ensure that the lock is released.
                Monitor.Exit(groupedIns);
            }
        }

        /// <summary>
        /// Get all elements safety
        /// </summary>
        public IEnumerable<GroupedInComing<T>> AllElementsSafe<T>(
            SortedSet<GroupedInComing<T>> groupedIns)
        {
            lock(groupedIns)
            {
                foreach (var elem in groupedIns)
                {
                    yield return elem;
                }
            }
        }

        #endregion


        #region Messages
        private SortedSet<GroupedInComing<Message>> _inComingMessages;

            /// <summary>
            /// Dictionary of updates and handling group 
            /// </summary>
            public SortedSet<GroupedInComing<Message>> InComingMessages
            {
                get
                {
                    if (_inComingMessages == null)
                        _inComingMessages = new SortedSet<GroupedInComing<Message>>(
                            CreateComparer<Message>());

                    return _inComingMessages;
                }
            }
            #endregion

        #region CallbackQuery
        private SortedSet<GroupedInComing<CallbackQuery>> _inComingCallbackQueries;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<CallbackQuery>> InComingCallbackQueries
        {
            get
            {
                if (_inComingCallbackQueries == null)
                    _inComingCallbackQueries = new SortedSet<GroupedInComing<CallbackQuery>>(
                        CreateComparer<CallbackQuery>());

                return _inComingCallbackQueries;
            }
        }
        #endregion

        #region InlineQuery
        private SortedSet<GroupedInComing<InlineQuery>> _inComingInlineQueries;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<InlineQuery>> InComingInlineQueries
        {
            get
            {
                if (_inComingInlineQueries == null)
                    _inComingInlineQueries = new SortedSet<GroupedInComing<InlineQuery>>(
                        CreateComparer<InlineQuery>());

                return _inComingInlineQueries;
            }
        }
        #endregion

        #region ChatMembers
        private SortedSet<GroupedInComing<ChatMemberUpdated>> _inComingChatMembers;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<ChatMemberUpdated>> InComingChatMembers
        {
            get
            {
                if (_inComingChatMembers == null)
                    _inComingChatMembers = new SortedSet<GroupedInComing<ChatMemberUpdated>>(
                        CreateComparer<ChatMemberUpdated>());

                return _inComingChatMembers;
            }
        }
        #endregion

        #region ChosenInlineResult

        private SortedSet<GroupedInComing<ChosenInlineResult>> _inComingInlineResultChosen;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<ChosenInlineResult>> InComingInlineResultChosen
        {
            get
            {
                if (_inComingInlineResultChosen == null)
                    _inComingInlineResultChosen = new SortedSet<GroupedInComing<ChosenInlineResult>>(
                        CreateComparer<ChosenInlineResult>());

                return _inComingInlineResultChosen;
            }
        }

        #endregion

        #region Poll

        private SortedSet<GroupedInComing<Poll>> _inComingPoll;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<Poll>> InComingPoll
        {
            get
            {
                if (_inComingPoll == null)
                    _inComingPoll = new SortedSet<GroupedInComing<Poll>>(
                        CreateComparer<Poll>());

                return _inComingPoll;
            }
        }
        #endregion

        #region PollAnswer

        private SortedSet<GroupedInComing<PollAnswer>> _inComingPollAnswer;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<PollAnswer>> InComingPollAnswer
        {
            get
            {
                if (_inComingPollAnswer == null)
                    _inComingPollAnswer = new SortedSet<GroupedInComing<PollAnswer>>(
                        CreateComparer<PollAnswer>());

                return _inComingPollAnswer;
            }
        }
        #endregion

        #region ShippingQuery

        private SortedSet<GroupedInComing<ShippingQuery>> _inComingShippingQuery;

        /// <summary>
        /// Dictionary of updates and handling group 
        /// </summary>
        public SortedSet<GroupedInComing<ShippingQuery>> InComingShippingQuery
        {
            get
            {
                if (_inComingShippingQuery == null)
                    _inComingShippingQuery = new SortedSet<GroupedInComing<ShippingQuery>>(
                        CreateComparer<ShippingQuery>());

                return _inComingShippingQuery;
            }
        }
        #endregion

        #region PreCheckoutQuery

        private SortedSet<GroupedInComing<PreCheckoutQuery>> _inComingPreCheckoutQuery;

        /// <summary>
        /// Dictionary of updates and handling group
        /// </summary>
        public SortedSet<GroupedInComing<PreCheckoutQuery>> InComingPreCheckoutQuery
        {
            get
            {
                if (_inComingPreCheckoutQuery == null)
                    _inComingPreCheckoutQuery = new SortedSet<GroupedInComing<PreCheckoutQuery>>(
                        CreateComparer<PreCheckoutQuery>());

                return _inComingPreCheckoutQuery;
            }
        }

        #endregion
    }
}
