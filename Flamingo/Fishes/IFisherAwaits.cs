using Flamingo.Condiments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flamingo.Fishes
{
    /// <summary>
    /// Interface to build await-able incoming handler
    /// </summary>
    /// <typeparam name="T">InComing update type</typeparam>
    public interface IFisherAwaits<T>
    {
        /// <summary>
        /// Set ICondiment that arrives
        /// </summary>
        public void SetCdmt(ICondiment<T> condiment);

        /// <summary>
        /// The status of current Await-able incoming
        /// </summary>
        public AwaitableStatus Status { get; }

        /// <summary>
        /// A token to cancel wait operation
        /// </summary>
        public CancellationTokenSource CancellationToken { get; }

        /// <summary>
        /// The time in second we should wait
        /// </summary>
        public int TimeOut { get; }

        /// <summary>
        /// We are waiting for this!
        /// </summary>
        public Action AwaitFor { get; }

        /// <summary>
        /// Wait for the target fish to get in trap!
        /// </summary>
        /// <returns></returns>
        public Task<AwaitableResult<T>> Wait(Dictionary<IFish<T>, int> pairs);
    }
}
