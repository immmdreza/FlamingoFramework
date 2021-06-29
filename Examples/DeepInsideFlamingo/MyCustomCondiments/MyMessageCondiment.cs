using Flamingo;
using Flamingo.Condiments.HotCondiments;
using Telegram.Bot.Types;

namespace DeepInsideFlamingo.MyCustomCondiments
{
    public class MyMessageCondiment : MessageCondiment
    {
        public MyMessageCondiment(
            Message inComing,
            FlamingoCore flamingo,
            DatabaseManager databaseManager) 
            : base(inComing, flamingo, false, false)
        {
            Db = databaseManager;
        }

        public DatabaseManager Db { get; }
    }
}
