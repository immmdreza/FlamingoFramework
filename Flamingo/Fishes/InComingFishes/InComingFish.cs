using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// Base abstract class of InComings
    /// </summary>
    /// <typeparam name="T">InComing update type</typeparam>
    public abstract class InComingFish<T> : IFish<T>
    {
        /// <summary>
        /// Base abstract class of InComings
        /// </summary>
        /// <param name="filter">
        /// Sync filter of this incoming handler
        /// </param>
        /// <param name="filterAsync">Async filter of this incoming handler</param>
        /// <param name="getEatenCallback">A callback function whitch processes incoming update.</param>
        public InComingFish(IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null,
            Func<ICondiment<T>, Task<bool>> getEatenCallback = null)
        {
            Filter = filter;
            FilterAsync = filterAsync;
            if (getEatenCallback == null)
                this.GetEaten = _GetEaten;
            else
                GetEaten = getEatenCallback;
        }

        /// <inheritdoc/>
        public IFilter<ICondiment<T>> Filter { get; }

        /// <inheritdoc/>
        public IFilterAsync<ICondiment<T>> FilterAsync { get; }

        /// <summary>
        /// It's the ICondiment of incoming update
        /// </summary>
        protected ICondiment<T> Cdmt { get; set; }

        /// <summary>
        /// Instance of FlamingoCore that takes care of update
        /// </summary>
        protected FlamingoCore Flamingo => Cdmt.Flamingo;

        /// <summary>
        /// Engaged bot.
        /// </summary>
        protected TelegramBotClient BotClient => Cdmt.Flamingo.BotClient;

        /// <inheritdoc/>
        public Func<ICondiment<T>, Task<bool>> GetEaten { get; }

        private bool _result = true;

        /// <inheritdoc/>
        protected void StopPropagation()
        {
            _result = false;
        }

        /// <summary>
        /// Override this function to process incoming update.
        /// if you are using this, left <see cref="GetEaten"/> null.
        /// </summary>
        /// <param name="inComing"></param>
        /// <returns></returns>
        protected virtual Task GetEatenWrapper(T inComing)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> _GetEaten(ICondiment<T> condiment)
        {
            Cdmt = condiment;

            await GetEatenWrapper(condiment.InComing);

            return _result;
        }
    }
}
