using Flamingo.Condiments;
using System.Linq;

namespace Flamingo.Filters.SharedFilters
{
    public class FromChatsFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter incoming updates based on chat id
        /// </summary>
        /// <param name="ids">Allowed userids</param>
        public FromChatsFilter(params long[] ids)
            : base(x =>
            {
                var chat = x.Chat;

                if (chat == null) return false;

                return ids.Any(x => x == chat.Id);
            })
        { }

        /// <summary>
        /// Filter incoming updates based on chat username ( if exists )
        /// </summary>
        /// <param name="usernames">Allowed usernames</param>
        public FromChatsFilter(params string[] usernames)
            : base(x =>
            {
                var chat = x.Chat;

                if (chat == null) return false;

                if (string.IsNullOrEmpty(chat.Username)) return false;

                return usernames
                    .Any(x => x.Replace("@", "").ToLower() == chat.Username.ToLower());
            })
        { }
    }
}
