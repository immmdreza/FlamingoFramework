using System;
using System.Collections.Generic;
using System.Text;

namespace Flamingo.Helpers
{
    /// <summary>
    /// Interface to create a grid by lists
    /// </summary>
    /// <typeparam name="T">Input data</typeparam>
    /// <typeparam name="K">Output data</typeparam>
    public interface IGridBuilder<T>
    {
        /// <summary>
        /// Created structure of grid
        /// </summary>
        public List<List<T>> Struncture { get; }

        /// <summary>
        /// Build structure
        /// </summary>
        public List<List<T>> innerBuilder(params T[][] rows)
        {
            var _keyboard = new List<List<T>>();

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    _keyboard.Add(new List<T>());
                    foreach (var btn in row)
                    {
                        _keyboard[^1].Add(btn);
                    }
                }
            }

            return _keyboard;
        }

        /// <summary>
        /// Build structure
        /// </summary>
        public List<List<T>> innerColumnBuilder(int columnCount = 2, params T[] rows)
        {
            int added = 0;
            var _keyboard = new List<List<T>>();

            foreach (var row in rows)
            {
                if (added % columnCount == 0)
                {
                    _keyboard.Add(new List<T>());
                }

                _keyboard[^1].Add(row);
                added++;
            }

            return _keyboard;
        }

        /// <summary>
        /// Dynamically build your set
        /// </summary>
        public List<List<T>> Build(List<List<T>> _keyboard, Action<List<List<T>>> action)
        {
            return _keyboard.Build(action);
        }
    }
}
