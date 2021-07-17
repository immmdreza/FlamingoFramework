using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.Advanced.CarrierFishes
{
    public sealed class MessageCarrierFish<T> 
        : BaseCarrierFish<Message, T> where T: IAdvFish<Message>
    {
        public MessageCarrierFish(
            IFilter<ICondiment<Message>> filter = null,
            IFilterAsync<ICondiment<Message>> filterAsync = null) 
            : base(filter, filterAsync)
        { }
    }
}
