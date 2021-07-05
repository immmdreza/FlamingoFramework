using Flamingo.Fishes;
using Flamingo.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Flamingo.Condiments.Extensions
{
    /// <summary>
    /// Extensions to work with arguments
    /// </summary>
    public static class ArgumansExtensions
    {
        /// <summary>
        /// Get an string arguments if exists
        /// </summary>
        public static string GetStrArg<T>(this ICondiment<T> condiment,
            int index, bool toEnd = false, char joiner = ' ')
        {
            return condiment.QueryArgs.GetStrArg(index, toEnd, joiner);
        }

        /// <summary>
        /// Get an string arguments if exists
        /// </summary>
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

        /// <summary>
        /// Tries to get an string arguments if exists
        /// </summary>
        public static bool TryGetStrArg<T>(this ICondiment<T> condiment,
            int index, out string arg, bool toEnd = false, char joiner = ' ')
        {
            return condiment.QueryArgs.TryGetStrArg(index, out arg, toEnd, joiner);
        }

        /// <summary>
        /// Tries to get an string arguments if exists
        /// </summary>
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

        /// <summary>
        /// Tries to get an string arguments and convert it to Given Type if exists
        /// </summary>
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

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">Type to convert string to it</typeparam>
        /// <param name="cdmt">InComing condiment</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1>(
            this ICondiment<T> cdmt,
            out T1 arg1,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!cdmt.TryGetStrArg(startIndex + 0, out arg1, toEnd))
            {
                arg1 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">Type to convert string to it</typeparam>
        /// <param name="awaitableResult">InComing await-able result</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1>(
            this AwaitableResult<T> awaitableResult,
            out T1 arg1,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 0, out arg1, toEnd))
            {
                arg1 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T1">Type to convert string to it</typeparam>
        /// <param name="text">Input text</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="splitter">Split string using this char</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
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

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <param name="cdmt">InComing condiment</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2>(
            this ICondiment<T> cdmt,
            out T1 arg1,
            out T2 arg2,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 1, out arg2, toEnd))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <param name="awaitableResult">InComing await-able result</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2>(
            this AwaitableResult<T> awaitableResult,
            out T1 arg1,
            out T2 arg2,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 1, out arg2, toEnd))
            {
                arg1 = default;
                arg2 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="text">Input text</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of arguments</param>
        /// <param name="splitter">Split string using this char</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
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

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <param name="cdmt">InComing condiment</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2, T3>(
            this ICondiment<T> cdmt,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 2, out arg3, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <param name="awaitableResult">InComing await-able result</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2, T3>(
            this AwaitableResult<T> awaitableResult,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 2, out arg3, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="text">Input text</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="startIndex">starting index of arguments</param>
        /// <param name="splitter">Split string using this char</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
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

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <typeparam name="T4">Forth Type to convert string to it</typeparam>
        /// <param name="cdmt">InComing condiment</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="arg4">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2, T3, T4>(
            this ICondiment<T> cdmt,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            out T4 arg4,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 2, out arg3))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!cdmt.TryGetStrArg(startIndex + 3, out arg4, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T">Type of incoming message</typeparam>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <typeparam name="T4">Forth Type to convert string to it</typeparam>
        /// <param name="awaitableResult">InComing await-able result</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="arg4">out put converted argument</param>
        /// <param name="startIndex">starting index of argument</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
        public static bool GetRequireArgs<T, T1, T2, T3, T4>(
            this AwaitableResult<T> awaitableResult,
            out T1 arg1,
            out T2 arg2,
            out T3 arg3,
            out T4 arg4,
            int startIndex = 0,
            bool toEnd = false)
        {
            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 0, out arg1))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 1, out arg2))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 2, out arg3))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            if (!awaitableResult.Cdmt.TryGetStrArg(startIndex + 3, out arg4, toEnd))
            {
                arg1 = default;
                arg2 = default;
                arg3 = default;
                arg4 = default;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to get and convert an string argument
        /// </summary>
        /// <typeparam name="T1">First Type to convert string to it</typeparam>
        /// <typeparam name="T2">Second Type to convert string to it</typeparam>
        /// <typeparam name="T3">Third Type to convert string to it</typeparam>
        /// <typeparam name="T4">Forth Type to convert string to it</typeparam>
        /// <param name="text">Input text</param>
        /// <param name="arg2">out put converted argument</param>
        /// <param name="arg3">out put converted argument</param>
        /// <param name="arg1">out put converted argument</param>
        /// <param name="arg4">out put converted argument</param>
        /// <param name="startIndex">starting index of arguments</param>
        /// <param name="splitter">Split string using this char</param>
        /// <param name="toEnd">Join arguments to end of string</param>
        /// <returns>True if get an convert was successful</returns>
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
