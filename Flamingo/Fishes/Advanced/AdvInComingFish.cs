using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Flamingo.Fishes.Advanced
{
    /// <summary>
    /// Base class to create advanced incoming handlers
    /// </summary>
    /// <typeparam name="T">InComing update type</typeparam>
    public abstract class AdvInComingFish<T> : IAdvFish<T>
    {
        /// <inheritdoc/>
        public Func<ICondiment<T>, Task<bool>> GetEaten { get; }

        /// <summary>
        /// Base abstract class of InComings
        /// </summary>
        public AdvInComingFish()
        {
            GetEaten = _GetEaten;
        }

        /// <summary>
        /// It's the ICondiment of incoming update
        /// </summary>
        protected ICondiment<T> Cdmt { get; set; }

        /// <summary>
        /// Instance of FlamingoCore that takes care of update
        /// </summary>
        protected IFlamingoCore Flamingo => Cdmt.Flamingo;

        /// <summary>
        /// Engaged bot.
        /// </summary>
        protected TelegramBotClient BotClient => Cdmt.Flamingo.BotClient;

        /// <inheritdoc/>
        public IFilter<ICondiment<T>> Filter => null;

        /// <inheritdoc/>
        public IFilterAsync<ICondiment<T>> FilterAsync => null;


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
