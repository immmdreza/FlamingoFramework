using Flamingo.Condiments.Extensions;
using Flamingo.Helpers;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Condiments
{
    /// <summary>
    /// Interface to create condiments
    /// </summary>
    /// <typeparam name="T">InComing Update Type</typeparam>
    public interface ICondiment<T>
    {
        /// <summary>
        /// InComing Update!
        /// </summary>
        public T InComing { get; }

        /// <summary>
        /// The engaged instance of FlamingoCore
        /// </summary>
        public FlamingoCore Flamingo { get; }

        /// <summary>
        /// The main string query that update may carry
        /// </summary>
        /// <example>message.Text or message.Caption for Message</example>
        /// <example>callback.Data for CallbackQuery</example>
        public string StringQuery { get; }

        /// <summary>
        /// Query argumans fetched by splitting string query ( mostly with ' ' )
        /// </summary>
        /// <remarks>Consider using <c>GetRequireArgs</c> to check args safe and quickly</remarks>
        public IEnumerable<string> QueryArgs { get; }


        #region Extra attr

        /// <summary>
        /// Userid for sender of update
        /// </summary>
        public long SenderId { get; }

        /// <summary>
        /// The sender of update ( mostly <c>update.From</c> )
        /// </summary>
        public User Sender { get; }

        /// <summary>
        /// The container chat of update ( mostly <c>update.Chat</c> )
        /// </summary>
        public Chat Chat { get; }

        /// <summary>
        /// You have this if you have a RegexFilter and atleast a matched result
        /// </summary>
        public MatchCollection MatchCollection { get; set; }

        #endregion


        #region Extensions

        /// <summary>
        /// Tries to get a arguman
        /// </summary>
        /// <typeparam name="Type">Type of arg</typeparam>
        /// <param name="index">It's index</param>
        /// <param name="outArg">Fetched arg</param>
        /// <param name="toEnd">Get to the end of line</param>
        /// <param name="joiner">join them using which char?</param>
        /// <returns></returns>
        public bool TryGetStrArg<Type>(
            int index, out Type outArg, bool toEnd = false, char joiner = ' ')
        {
            var arg = this.GetStrArg(index, toEnd, joiner);
            if (arg != null)
            {
                try
                {
                    if (arg.TryConvert(out Type output))
                    {
                        outArg = output;
                        return true;
                    }

                    outArg = default;
                    return true;
                }
                catch
                {
                    outArg = default;
                    return false;
                }
            }
            else
            {
                outArg = default;
                return false;
            }
        }

        #endregion
    }
}
