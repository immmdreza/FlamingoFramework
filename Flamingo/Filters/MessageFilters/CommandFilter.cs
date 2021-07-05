using Flamingo.Condiments;
using Flamingo.Filters.Enums;
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
            ICondiment<Message> incoming,
            char prefix = '/',
            ArgumentsMode argumentsMode = ArgumentsMode.Idc,
            params string[] commands)
        {
            if (string.IsNullOrEmpty(incoming.StringQuery)) return false;

            var botUsername = "@" + incoming.Flamingo.BotInfo.Username.ToLower();

            var command = incoming.QueryArgs[0].ToLower().Replace(botUsername, "");

            if (argumentsMode == ArgumentsMode.Require &&
                incoming.QueryArgs.Length < 2) return false;

            if (argumentsMode == ArgumentsMode.NoArgs &&
                incoming.QueryArgs.Length > 1) return false;

            return commands.Any(x => prefix + x == command);
        }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed. default prefix '/' will be applied!</param>
        public CommandFilter(params string[] commands)
            : base(x=>
            {
                return _CommandFilter(x, '/', ArgumentsMode.Idc, commands);
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
                return _CommandFilter(x, prefix, ArgumentsMode.Idc, commands);
            })
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="commands">Command that are allowed</param>
        /// <param name="argumentsMode">If command should carry arguments</param>
        public CommandFilter(
            char prefix,
            ArgumentsMode argumentsMode,
            params string[] commands)
            : base(x =>
            {
                return _CommandFilter(x, prefix, argumentsMode, commands);
            })
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed</param>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="argumentsMode">If command should carry arguments</param>
        public CommandFilter(
            string commands,
            char prefix = '/',
            ArgumentsMode argumentsMode = ArgumentsMode.Idc)
            : base(x =>
            {
                return _CommandFilter(x, prefix, argumentsMode, commands);
            })
        { }
    }
}
