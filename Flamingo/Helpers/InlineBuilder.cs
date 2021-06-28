using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Helpers
{
    public class InlineBuilder
    {
        private List<List<(string, string)>> _inline;

        public InlineBuilder(params (string, string)[][] rows)
        {
            _inline = new List<List<(string, string)>>();

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    _inline.Add(new List<(string, string)>());
                    foreach (var btn in row)
                    {
                        _inline[^1].Add(btn);
                    }
                }
            }
        }

        public List<List<InlineKeyboardButton>> Use()
        {
            var result = new List<List<InlineKeyboardButton>>();

            foreach (var row in _inline)
            {
                result.Add(new List<InlineKeyboardButton>());
                foreach (var btn in row)
                {
                    result[^1].Add(
                        InlineKeyboardButton.WithCallbackData(btn.Item1, btn.Item2));
                }
            }

            return result;
        }

        public InlineBuilder Build(Action<List<List<(string, string)>>> action)
        {
            _inline.Build(action);
            return this;
        }
    }
}
