using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flamingo.InComingProcesses
{
    /// <summary>
    /// Incoming update details 
    /// </summary>
    class InComingDetails
    {
        /// <summary>
        /// Incoming update details 
        /// </summary>
        public InComingDetails(
            Func<Task> processTask,
            DateTime sentTime,
            long userId,
            long chatId)
        {
            ProcessTask = processTask;
            SentTime = sentTime;
            UserId = userId;
            ChatId = chatId;
        }

        /// <summary>
        /// The task where update processes in
        /// </summary>
        public Func<Task> ProcessTask { get; }

        /// <summary>
        /// When update arrives
        /// </summary>
        public DateTime SentTime { get; }

        /// <summary>
        /// Sender id of update
        /// </summary>
        public long UserId { get; }

        /// <summary>
        /// Chat which update sent in
        /// </summary>
        public long ChatId { get; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if(obj is InComingDetails details)
            {
                return details.ChatId == ChatId && details.UserId == UserId;
            }

            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
