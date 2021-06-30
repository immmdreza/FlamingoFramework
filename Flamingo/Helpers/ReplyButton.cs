using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Helpers
{
    /// <summary>
    /// Create a set of reply keyboards as quick as possible
    /// </summary>
    public class ReplyButton: IGridBuilder<string>
    {
        /// <inheritdoc/>
        public List<List<string>> Struncture => _struncture;

        private List<List<string>> _struncture;

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        public ReplyButton(params string[][] rows)
        {
             _struncture = (this as IGridBuilder<string>).innerBuilder(rows);
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard texts for a set of keyboards</param>
        /// <param name="columnCount">Column count to divide keyboards to</param>
        public ReplyButton(int columnCount = 2, params string[] rows)
        {
            _struncture = (this as IGridBuilder<string>).innerColumnBuilder(columnCount, rows);
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard texts for a set of keyboards (divided to 2 columns)</param>
        public ReplyButton(params string[] rows)
        {
            _struncture = (this as IGridBuilder<string>).innerColumnBuilder(2, rows);
        }

        /// <summary>
        /// Create a set of reply keyboards as quick as possible
        /// </summary>
        /// <param name="rows">Keyboard text for a single button</param>
        public ReplyButton(string rows)
        {
            _struncture = (this as IGridBuilder<string>).innerColumnBuilder(1, rows);
        }

        /// <summary>
        /// Convert your buttons to <see cref="ReplyKeyboardMarkup"/>
        /// </summary>
        public ReplyKeyboardMarkup Markup(bool resize = true, bool oneTime = true)
        {
            var result = new List<List<KeyboardButton>>();

            foreach (var row in Struncture)
            {
                result.Add(new List<KeyboardButton>());
                foreach (var btn in row)
                {
                    result[^1].Add(new KeyboardButton(btn));
                }
            }

            return new ReplyKeyboardMarkup(result, resize, oneTime);
        }

        /// <summary>
        /// Build list dynamically
        /// </summary>
        /// <param name="action"></param>
        public void Build(Action<List<List<string>>> action)
        {
            _struncture = _struncture.Build(action);
        }
    }
}
