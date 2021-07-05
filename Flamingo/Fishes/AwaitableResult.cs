﻿using Flamingo.Condiments;

namespace Flamingo.Fishes
{
    /// <summary>
    /// The result of an await-able incoming
    /// </summary>
    public sealed class AwaitableResult<T>
    {
        /// <summary>
        /// The result of an await-able incoming
        /// </summary>
        public AwaitableResult(AwaitableStatus status, ICondiment<T> cdmt)
        {
            Status = status;
            Cdmt = cdmt;
        }

        /// <summary>
        /// Text respond fetched from <see cref="ICondiment{T}.StringQuery"/>
        /// </summary>
        public string TextRespond => Cdmt.StringQuery;

        /// <summary>
        /// Indicates that incoming update received successfully 
        /// </summary>
        public bool Succeeded => Status == AwaitableStatus.Succeeded;

        /// <summary>
        /// Indicates that waiting for incoming timed out!
        /// </summary>
        public bool TimedOut => Status == AwaitableStatus.TimedOut;

        /// <summary>
        /// The status of current Await-able incoming
        /// </summary>
        public AwaitableStatus Status { get; }

        /// <summary>
        /// This is a <see cref="ICondiment{T}"/> that built based on received update
        /// </summary>
        /// <remarks>
        /// This is null if the <see cref="Status"/> is not <see cref="AwaitableStatus.Succeeded"/>
        /// </remarks>
        public ICondiment<T> Cdmt { get; }
    }
}
