using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Helpers
{
    class ReplyButton
    {
        private List<List<string>> _keyboard;

        public ReplyButton(params string[][] rows)
        {
            _keyboard = new List<List<string>>();

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    _keyboard.Add(new List<string>());
                    foreach (var btn in row)
                    {
                        _keyboard[^1].Add(btn);
                    }
                }
            }
        }

        public List<List<KeyboardButton>> Use()
        {
            var result = new List<List<KeyboardButton>>();

            foreach (var row in _keyboard)
            {
                result.Add(new List<KeyboardButton>());
                foreach (var btn in row)
                {
                    result[^1].Add(new KeyboardButton(btn));
                }
            }

            return result;
        }

        public ReplyButton Build(Action<List<List<string>>> action)
        {
            _keyboard.Build(action);
            return this;
        }
    }
}
