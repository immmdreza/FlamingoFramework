using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Flamingo.Fishes.Advanced
{
    public abstract class AdvInComingFish<T> : IAdvFish<T>
    {
        public Func<ICondiment<T>, Task<bool>> GetEaten { get; }

        /// <summary>
        /// Base abstract class of InComings
        /// </summary>
        /// <param name="getEatenCallback">A callback function which processes incoming update.</param>
        public AdvInComingFish(Func<ICondiment<T>, Task<bool>> getEatenCallback = null)
        {
            if (getEatenCallback == null)
                GetEaten = _GetEaten;
            else
                GetEaten = getEatenCallback;
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

        public IFilter<ICondiment<T>> Filter => null;

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
