using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Condiments.Extensions
{
    /// <summary>
    /// A set of regular extensions
    /// <summary>
    public static class RegularExtensions
    {
        public static string Mention(
            this User user,
            ParseMode parseMode,
            bool fullname = false,
            string customText = null)
        {
            throw new NotImplementedException();
        }
    }
}
