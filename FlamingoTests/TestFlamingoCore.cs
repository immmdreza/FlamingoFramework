using System.Net.Http;
using Telegram.Bot.Types;

namespace FlamingoTests
{
    public class TestFlamingoCore : Flamingo.FlamingoCore
    {
        public TestFlamingoCore(
            User botInfo = null,
            HttpClient httpClient = null,
            string baseUrl = null) : base(httpClient, baseUrl)
        {
            if (botInfo != null)
                _botInfo = botInfo;
        }
    }
}
