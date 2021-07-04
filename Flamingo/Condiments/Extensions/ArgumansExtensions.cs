using Flamingo.Helpers;
using System.Collections.Generic;
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

        public static string GetStrArg(this IEnumerable<string> args,
            int index, bool toEnd = false, char joiner = ' ')
        {
            if (args != null)
            {
                if (index <= args.Count() - 1)
                {
                    return toEnd ?
                        string.Join(joiner, args.ToArray()[index..]) :
                        args.ElementAt(index);
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

        public static bool TryGetStrArg(this IEnumerable<string> args,
            int index, out string arg, bool toEnd = false, char joiner = ' ')
        {
            var givenArg = args.GetStrArg(index, toEnd, joiner);
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

        public static bool TryGetStrArg<Type>(this IEnumerable<string> args,
            int index, out Type outArg, bool toEnd = false, char joiner = ' ')
        {
            var arg = args.GetStrArg(index, toEnd, joiner);
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

        public static bool GetRequireArgs<T1>(
            this string text,
            out T1 arg1,
            int startIndex = 0,
            char splitter = ' ',
            bool toEnd = false)
        {
            var args = text.Split(splitter);

            if (!args.TryGetStrArg(startIndex + 0, out arg1, toEnd, splitter))
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

        public static bool GetRequireArgs<T1, T2>(
            this string text,
            out T1 arg1,
            out T2 arg2,
            int startIndex = 0,
            char splitter = ' ',
            bool toEnd = false)
        {
            var args = text.Split(splitter);

            if (!args.TryGetStrArg(startIndex + 0, out arg1, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 1, out arg2, toEnd, splitter))
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

        public static bool GetRequireArgs<T1, T2, T3>(
            this string text,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            int startIndex = 0,
            char splitter = ' ',
            bool toEnd = false)
        {
            var args = text.Split(splitter);

            if (!args.TryGetStrArg(startIndex + 0, out arg1, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 1, out arg2, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 2, out arg3, toEnd, splitter))
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

        public static bool GetRequireArgs<T1, T2, T3, T4>(
            this string text,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            out T4 arg4,
            char splitter = ' ',
            int startIndex = 0,
            bool toEnd = false)
        {
            var args = text.Split(splitter);

            if (!args.TryGetStrArg(startIndex + 0, out arg1, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 1, out arg2, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 2, out arg3, joiner: splitter))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!args.TryGetStrArg(startIndex + 3, out arg4, toEnd, splitter))
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
