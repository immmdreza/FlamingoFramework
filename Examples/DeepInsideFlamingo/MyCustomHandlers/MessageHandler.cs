using DeepInsideFlamingo.MyCustomCondiments;
using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Flamingo.Fishes.InComingFishes;
using Telegram.Bot.Types;

namespace DeepInsideFlamingo.MyCustomHandlers
{
    public class MessageHandler : InComingMessage
    {
        public MessageHandler(
            IFilter<ICondiment<Message>> filter = null,
            IFilterAsync<ICondiment<Message>> filterAsync = null) : base(filter, filterAsync)
        { }

        public MyMessageCondiment MyCdmt
        {
            get
            {
                if(Cdmt is MyMessageCondiment myMessageCondiment)
                {
                    return myMessageCondiment;
                }

                return null;
            }
        }
    }
}
