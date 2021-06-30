using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flamingo.Fishes.Awaitables
{
    /// <summary>
    /// Create an await-able incoming handler which will wait for a specific update to come!
    /// </summary>
    /// <remarks>
    /// DO NOT use this manually! use <see cref="FlamingoCore.WaitForInComing{T}(IFisherAwaits{T})"/>
    /// to await incoming!
    /// </remarks>
    public class SimpleAwaitableInComing<T> : IFish<T>, IFisherAwaits<T>
    {
        private CancellationTokenSource _timerCancell;

        /// <inheritdoc/>
        public async Task<AwaitableResult<T>> Wait(Dictionary<IFish<T>, int> mamager)
        {
            for (int i = 0; i < TimeOut * 2; i++)
            {
                if (CancellationToken.IsCancellationRequested)
                {
                    _status = AwaitableStatus.Cancelled;
                    mamager.Remove(this);
                    return null;
                }

                if (_timerCancell.IsCancellationRequested)
                {
                    mamager.Remove(this);
                    if (_status == AwaitableStatus.Succeeded) 
                        return new AwaitableResult<T>(Status, _cdmt);
                }

                await Task.Delay(500);
            }

            mamager.Remove(this);
            _status = AwaitableStatus.TimedOut;
            return null;
        }

        private void Triggered()
        {
            _status = AwaitableStatus.Succeeded;

            if (!_timerCancell.IsCancellationRequested)
                _timerCancell.Cancel();
        }

        /// <summary>
        /// Create an await-able incoming handler which will wait for a specific update to come!
        /// </summary>
        /// <remarks>
        /// DO NOT use this manually! use <see cref="FlamingoCore.WaitForInComing{T}(IFisherAwaits{T})"/>
        /// to await incoming!
        /// </remarks>
        /// <param name="filter">Sync filter like always</param>
        /// <param name="filterAsync">Async filter like always</param>
        /// <param name="timeOut">Time out in seconds</param>
        /// <param name="cancellationToken">A custom token to cancel the await!</param>
        public SimpleAwaitableInComing(
            IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            Filter = filter;
            FilterAsync = filterAsync;
            TimeOut = timeOut;
            CancellationToken = cancellationToken;
            AwaitFor = Triggered;

            if (CancellationToken == default)
                CancellationToken = new CancellationTokenSource();

            _status = AwaitableStatus.Pending;
            _timerCancell = new CancellationTokenSource();
        }

        /// <summary>
        /// Cancel awaiting incoming
        /// </summary>
        public void Cancell()
        {
            CancellationToken.Cancel();
        }

        /// <inheritdoc/>
        public void SetCdmt(ICondiment<T> condiment)
        {
            _cdmt = condiment;
        }

        /// <inheritdoc/>
        public IFilter<ICondiment<T>> Filter { get; }

        /// <inheritdoc/>
        public IFilterAsync<ICondiment<T>> FilterAsync { get; }

        /// <inheritdoc/>
        public Func<ICondiment<T>, Task<bool>> GetEaten => null;

        /// <inheritdoc/>
        public Action AwaitFor { get; }

        /// <inheritdoc/>
        public int TimeOut { get; }

        /// <inheritdoc/>
        public CancellationTokenSource CancellationToken { get; }

        private ICondiment<T> _cdmt;

        private AwaitableStatus _status;

        /// <inheritdoc/>
        public AwaitableStatus Status => _status;

        /// <inheritdoc/>
        public Timer Timer { get; }
    }
}
