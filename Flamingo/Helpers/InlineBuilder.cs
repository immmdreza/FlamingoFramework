using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Helpers
{
    public class InlineBuilder: IGridBuilder<(string, string)>
    {
        public List<List<(string, string)>> Struncture => _struncture;

        private List<List<(string, string)>> _struncture;

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        public InlineBuilder()
        {
            _struncture = new List<List<(string, string)>>();
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard (text, data)s for a set of keyboards</param>
        /// <param name="columnCount">Column count to divide keyboards to</param>
        public InlineBuilder(int columnCount = 2, params (string, string)[] rows)
        {
            _struncture = (this as IGridBuilder<(string, string)>).innerColumnBuilder(columnCount, rows);
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard (text, data)s for a set of keyboards (divided to 2 columns)</param>
        public InlineBuilder(params (string, string)[] rows)
        {
            _struncture = (this as IGridBuilder<(string, string)>).innerColumnBuilder(2, rows);
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard (text, data) for a single button</param>
        public InlineBuilder((string, string) rows)
        {
            _struncture = (this as IGridBuilder<(string, string)>).innerColumnBuilder(1, rows);
        }

        /// <summary>
        /// Give <see cref="InlineKeyboardMarkup"/> of your buttons.
        /// </summary>
        public InlineKeyboardMarkup Markup()
        {
            var result = new List<List<InlineKeyboardButton>>();

            foreach (var row in Struncture)
            {
                result.Add(new List<InlineKeyboardButton>());
                foreach (var btn in row)
                {
                    result[^1].Add(
                        InlineKeyboardButton.WithCallbackData(btn.Item1, btn.Item2));
                }
            }

            return new InlineKeyboardMarkup(result);
        }

        /// <summary>
        /// Build list dynamically
        /// </summary>
        /// <param name="action"></param>
        public void Build(Action<List<List<(string, string)>>> action)
        {
            _struncture = _struncture.Build(action);
        }
    }
}
