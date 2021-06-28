using System.Linq;

namespace Flamingo.Condiments.Extensions
{
    public static class ArgumansExtensions
    {
        public static string GetStrArg<T>(this ICondiment<T> condiment,
            int index, bool toEnd = false, char joiner = ' ')
        {
            if (condiment.QueryArgs != null)
            {
                if (index <= condiment.QueryArgs.Count() - 1)
                {
                    return toEnd ?
                        string.Join(joiner, condiment.QueryArgs.ToArray()[index..]) :
                        condiment.QueryArgs.ElementAt(index);
                }
            }

            return null;
        }

        public static bool TryGetStrArg<T>(this ICondiment<T> condiment,
            int index, out string arg, bool toEnd = false, char joiner = ' ')
        {
            var givenArg = condiment.GetStrArg(index, toEnd, joiner);
            if (givenArg != null)
            {
                arg = givenArg;
                return true;
            }
            else
            {
                arg = null;
                return false;
            }
        }

        public static bool GetRequireArgs<T, T1>(
            this ICondiment<T> context,
            out T1 arg1,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!context.TryGetStrArg(startIndex + 0, out arg1, toEnd))
            {
                arg1 = default;
                return false;
            }

            return true;
        }

        public static bool GetRequireArgs<T, T1, T2>(
            this ICondiment<T> context,
            out T1 arg1,
            out T2 arg2,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!context.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 1, out arg2, toEnd))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            return true;
        }

        public static bool GetRequireArgs<T, T1, T2, T3>(
            this ICondiment<T> context,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!context.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 2, out arg3, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            return true;
        }

        public static bool GetRequireArgs<T, T1, T2, T3, T4>(
            this ICondiment<T> context,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            out T4 arg4,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!context.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 2, out arg3))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!context.TryGetStrArg(startIndex + 3, out arg4, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            return true;
        }
    }
}
