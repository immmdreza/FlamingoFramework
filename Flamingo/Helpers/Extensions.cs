using Flamingo.Helpers.Types.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Helpers
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert <see cref="FlamingoChatType"/> to <see cref="ChatType"/> 
        /// </summary>
        /// <param name="flamingoChatType">Flamingo chat type</param>
        /// <returns></returns>
        public static ChatType ToChatType(
            this FlamingoChatType flamingoChatType)
        {
            return flamingoChatType switch
            {
                FlamingoChatType.Private => ChatType.Private,
                FlamingoChatType.Sender => ChatType.Sender,
                FlamingoChatType.Group => ChatType.Group,
                FlamingoChatType.SuperGroup => ChatType.Supergroup,
                FlamingoChatType.Channel => ChatType.Channel,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Convert <see cref="ChatType"/> to <see cref="FlamingoChatType"/> 
        /// </summary>
        /// <param name="chatType">Chat type</param>
        /// <returns></returns>
        public static FlamingoChatType ToFlamingoChatType(
            this ChatType chatType)
        {
            return chatType switch
            {
                ChatType.Private => FlamingoChatType.Private,
                ChatType.Sender => FlamingoChatType.Sender,
                ChatType.Group => FlamingoChatType.Group,
                ChatType.Supergroup => FlamingoChatType.SuperGroup,
                ChatType.Channel => FlamingoChatType.Channel,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Convert <see cref="ChatMemberFlamingoStatus"/> to <see cref="ChatMemberStatus"/> 
        /// </summary>
        /// <param name="flamingoStatus">Flamingo chat member status</param>
        /// <returns></returns>
        public static ChatMemberStatus ToChatMemberStatus(
            this ChatMemberFlamingoStatus flamingoStatus)
        {
            return flamingoStatus switch
            {
                ChatMemberFlamingoStatus.Admin => ChatMemberStatus.Administrator,
                ChatMemberFlamingoStatus.Kicked => ChatMemberStatus.Kicked,
                ChatMemberFlamingoStatus.Member => ChatMemberStatus.Member,
                ChatMemberFlamingoStatus.Restricted => ChatMemberStatus.Restricted,
                ChatMemberFlamingoStatus.Creator => ChatMemberStatus.Creator,
                ChatMemberFlamingoStatus.Left => ChatMemberStatus.Left,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Convert <see cref="ChatMemberStatus"/> to <see cref="ChatMemberFlamingoStatus"/> 
        /// </summary>
        /// <param name="status">Normal Chat member status</param>
        /// <returns></returns>
        public static ChatMemberFlamingoStatus ToChatMemberFlamingoStatus(
            this ChatMemberStatus status)
        {
            return status switch
            {
                ChatMemberStatus.Administrator => ChatMemberFlamingoStatus.Admin,
                ChatMemberStatus.Kicked => ChatMemberFlamingoStatus.Kicked,
                ChatMemberStatus.Member => ChatMemberFlamingoStatus.Member,
                ChatMemberStatus.Restricted => ChatMemberFlamingoStatus.Restricted,
                ChatMemberStatus.Creator => ChatMemberFlamingoStatus.Creator,
                ChatMemberStatus.Left => ChatMemberFlamingoStatus.Left,
                _ => throw new NotImplementedException(),
            };
        }

        private static List<T> Wapper<T>(Action<List<T>> func, List<T> list)
        {
            func(list);
            return list;
        }

        /// <summary>
        /// Use this method to build a list dynamically
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="markup">List to build</param>
        /// <param name="func">Function of how to build the list</param>
        /// <returns></returns>
        public static List<T> Build<T>(
            this List<T> markup,
            Action<List<T>> func)
                => Wapper(func, markup);

        /// <summary>
        /// Tries to convert a string to a given type
        /// </summary>
        /// <typeparam name="T">Should convert to this</typeparam>
        /// <param name="input">Input string to convert</param>
        /// <param name="output">Output result</param>
        /// <returns></returns>
        public static bool TryConvert<T>(this string input, out T output)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    output = (T)converter.ConvertFromString(input);
                    return true;
                }

                output = default;
                return false;
            }
            catch (ArgumentException)
            {
                output = default;
                return false;
            }
            catch (NotSupportedException)
            {
                output = default;
                return false;
            }
        }

        /// <summary>
        /// Tries to convert a string to a given type
        /// </summary>
        /// <param name="type">Should convert to this</param>
        /// <param name="input">Input string to convert</param>
        /// <param name="output">Output result</param>
        /// <returns></returns>
        public static bool TryConvert(this string input, Type type, out object output)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(type);
                if (converter != null)
                {
                    output = converter.ConvertFromString(input);
                    return true;
                }

                output = default;
                return false;
            }
            catch(ArgumentException)
            {
                output = default;
                return false;
            }
            catch (NotSupportedException)
            {
                output = default;
                return false;
            }
        }
    }
}
