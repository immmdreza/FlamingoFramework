using System.Collections.Concurrent;

namespace Flamingo.InComingProcesses
{
    class InComingProcessorsManager
    {
        private readonly ConcurrentBag<InComingDetails> _inComings;

        public InComingProcessorsManager()
        {
            _inComings = new ConcurrentBag<InComingDetails>();
        }

        public void Add(InComingDetails details)
        {
            _inComings.Add(details);
        }
    }
}
