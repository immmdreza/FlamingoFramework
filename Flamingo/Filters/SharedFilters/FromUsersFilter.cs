using Flamingo.Condiments;
using System.Linq;

namespace Flamingo.Filters.SharedFilters
{
    public class FromUsersFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter incoming updates based on sender id
        /// </summary>
        /// <param name="ids">Allowed userids</param>
        public FromUsersFilter(params long[] ids)
            : base(x =>
            {
                var user = x.Sender;

                if (user == null) return false;

                return ids.Any(x => x == user.Id);
            })
        { }

        /// <summary>
        /// Filter incoming updates based on sender username ( if exists )
        /// </summary>
        /// <param name="usernames">Allowed usernames</param>
        public FromUsersFilter(params string[] usernames)
            : base(x =>
            {
                var user = x.Sender;

                if (user == null) return false;

                if (string.IsNullOrEmpty(user.Username)) return false;

                return usernames
                    .Any(x => x.Replace("@", "").ToLower() == user.Username.ToLower());
            })
        { }
    }
}
