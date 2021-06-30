using Flamingo.Condiments;
using System.Linq;
using Telegram.Bot.Types;

namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Filters messages with specified command
    /// </summary>
    public class CommandFilter : MessageFilter
    {
        private static bool _CommandFilter(
            ICondiment<Message> incoming, char prefix = '/', params string[] commands)
        {
            if (string.IsNullOrEmpty(incoming.StringQuery)) return false;

            var botUsername = "@" + incoming.Flamingo.BotInfo.Username.ToLower();

            var command = incoming.QueryArgs.ElementAt(0).ToLower().Replace(botUsername, "");

            return commands.Any(x => prefix + x == command);
        }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed. default prefix '/' will be applied!</param>
        public CommandFilter(params string[] commands) 
            : base(x=>
            {
                return _CommandFilter(x, '/', commands);
            })
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="commands">Command that are allowed</param>
        public CommandFilter(char prefix, params string[] commands)
            : base(x =>
            {
                return _CommandFilter(x, prefix, commands);
            })
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed</param>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        public CommandFilter(string commands, char prefix = '/')
            : base(x =>
            {
                return _CommandFilter(x, prefix, commands);
            })
        { }
    }
}
