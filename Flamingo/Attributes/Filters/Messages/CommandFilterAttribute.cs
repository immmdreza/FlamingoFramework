using Flamingo.Filters.Enums;
using Flamingo.Filters.MessageFilters;

namespace Flamingo.Attributes.Filters.Messages
{
    /// <summary>
    /// Filters messages with specified command
    /// </summary>
    public class CommandFilterAttribute : MessageFiltersAttribute
    {
        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed. default prefix '/' will be applied!</param>
        public CommandFilterAttribute(params string[] commands) 
            : base(new CommandFilter(commands))
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="commands">Command that are allowed</param>
        public CommandFilterAttribute(char prefix, params string[] commands)
            : base(new CommandFilter(prefix, commands))
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="commands">Command that are allowed</param>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="argumentsMode">If command should carry arguments</param>
        public CommandFilterAttribute(
            string commands,
            ArgumentsMode argumentsMode = ArgumentsMode.Idc,
            char prefix = '/')
            : base(new CommandFilter(commands, prefix, argumentsMode))
        { }

        /// <summary>
        /// Filters messages with specified command
        /// </summary>
        /// <param name="prefix">Prefix of command. default to '/'</param>
        /// <param name="commands">Command that are allowed</param>
        /// <param name="argumentsMode">If command should carry arguments</param>
        public CommandFilterAttribute(
            char prefix,
            ArgumentsMode argumentsMode,
            params string[] commands)
            : base(new CommandFilter(prefix, argumentsMode, commands))
        { }
    }
}
