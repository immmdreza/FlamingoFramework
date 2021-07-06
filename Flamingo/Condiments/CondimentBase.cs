using System;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using static Flamingo.Extensions;

namespace Flamingo.Condiments
{
    /// <summary>
    /// Base class to make condiments for different updates
    /// </summary>
    /// <typeparam name="T">Update Type</typeparam>
    public abstract class CondimentBase<T> : ICondiment<T>
    {
        /// <summary>
        /// Base class to make condiments for different updates
        /// </summary>
        /// <param name="inComing">InComing Update</param>
        /// <param name="flamingo">The engaged instance of FlamingoCore</param>
        public CondimentBase(T inComing, IFlamingoCore flamingo)
        {
            UpdateType = AsUpdateType<T>();

            InComing = inComing;
            Flamingo = flamingo;
        }

        /// <inheritdoc/>
        public UpdateType UpdateType { get; }

        /// <inheritdoc/>
        public T InComing { get; }

        /// <inheritdoc/>
        public IFlamingoCore Flamingo { get; }

        /// <inheritdoc/>
        public virtual string[] QueryArgs
        {
            get
            {
                if (string.IsNullOrEmpty(StringQuery))
                    return Array.Empty<string>();

                return StringQuery.Split(' ');
            }
        }

        /// <inheritdoc/>
        public virtual User Sender => throw new NotImplementedException();

        /// <inheritdoc/>
        public virtual Chat Chat => throw new NotImplementedException();

        /// <inheritdoc/>
        public virtual string StringQuery => throw new NotImplementedException();

        /// <inheritdoc/>
        public virtual long SenderId => Sender?.Id ?? 0;

        /// <inheritdoc/>
        public MatchCollection MatchCollection { get; set; }

        /// <inheritdoc/>
        public string CommandQuery { get; set; }
    }
}
