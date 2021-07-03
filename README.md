# Flamingo 🦩
Flamingo is a framework to build Telegram bots using .NET as easy as possible!

## To what purpose?
As you can see, Flamingo uses [Telegram.Bot](https://github.com/TelegramBots/Telegram.Bot) and [Telegram.Bot.Extensions.Polling](https://github.com/TelegramBots/Telegram.Bot.Extensions.Polling) (Just in case) as dependencies. so by installing this package you have pure packages for telegram bots in .NET. 

But this one serves as a Top layer created over **TelegramBot** library to help you setup your bot in most easiest and also pro way! The very first and important purpose of Flamingo is simplicity, save time and cleaner code when writing telegram bots with .NET

**Remember that flamingo is just getting started**. it can grows with your support and help (help and support to an almost a large community)

In my turn, i tried to put all my experience here but there are a lot to go. lock into examples and you can examine the advantages of Flamingo ( at least in basic ) 

## Install
**Flamingo** is available in [Nuget](https://www.nuget.org/packages/Flamingo)

_⚠ Please consider this as a beta version yet!_

## How to use
Below there are some sources you can use

**Please read [WIKI](https://github.com/immmdreza/FlamingoFramework/wiki)**

<details>
  <summary> <b>Get Started</b> </summary>

_After installing Flamingo through Nuget package manager, you can use it like example below:_

#### 1- First of all Create an instance of `FlamingoCore` class into your program main method

```cs
static async Task Main()
{
    var flamingo = await new FlamingoCore()
}
```

#### 2- Now is the time to initialize your bot. in order to do that you need a bot token
(You can make a new bot and get api token for it from [@BotFather](https://t.me/BotFather))

- bot token is something like: `123443536:Akhkhpoue_DLkhejbdkeaiDHJKFkjbjs_D`
- Use method InitBot to initialize the bot like below:
```cs
// Pass your bot token to the method
await flamingo.InitBot("123443536:Akhkhpoue_DLkhejbdkeaiDHJKFkjbjs_D")
```

_Remember, most of this library methods are `async`. meaning that you should `await` them and use them in an async function ( like `Main()` here )_


#### 3- Let's make our first handler. handlers are what you handle and process inComing updates the way you like.
- To quickly build a handler we Use `SimpleInComing<T>`. where T is update type like messages, callback queries, inline queries and etc.
- To make handler for incoming messages, The `T` is `Message` ( like `SimpleInComing<Message>` )

#### 4- Every incoming handler has 2 important parts: **Filter** and **Callback function**
- Filters job is obvious: filter incoming updates to receive exact update you want
- Callback function: this function is where you decide how to process incoming update that passed filters. 

### Filters
Here we use two filters: 
- ChatTypeFilter: to make sure the update is coming from private chat
- CommandFilter: to handle specified bot command like `/start` 
- To Combine filters we bitwise operator `&` Meaning `AND`!

Your filters should be like below:
```cs
var chatFilter = new ChatTypeFilter<Message>(FlamingoChatType.Private);

var commandFilter new CommandFilter("start");
```
Later we'll combine them!

### Callback function
Callback function should take an `ICondiment<Message>` which contain everything you need to handle your updates, and should return a boolean as Task ( awaitable method )

Method structure should be like this:
```cs
private static async Task<bool> CallbackFunc(ICondiment<Message> cdmt)
{
    await cdmt.ReplyText("Just started!");
    return true;
}
```
We decided to reply to use command with a text message: `"Just started!"`. 

`cdmt.ReplyText()` dose that for us. it's an extension method of `ICondiment<Message>`


#### 5- Now that our filters and callback function is ready, we should pass them to handler instance (3)
```cs
var startHandler = new SimpleInComing<Message>(CallbackFunc,
    chatFilter & commandFilter);
``` 
As you see we combined two filters with &, meaning both of them should pass!

#### 6- Now add your handler to the Flamingo
```cs
flamingo.AddInComing(startHandler);
```

#### 7- Start listening to updates
```cs
await flamingo.Fly();
```

Your `Program.cs` file should be like below:
```cs
using Flamingo;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SimpleFlamingo
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = new FlamingoCore();

            await flamingo.InitBot("1820608649:AAF_rimZO_y_RlYnTX2WnifXldL1GiIcxt4");

            var chatFilter = new ChatTypeFilter<Message>(FlamingoChatType.Private);

            var commandFilter = new CommandFilter("start");

            var startHandler = new SimpleInComing<Message>(CallbackFunc,
                chatFilter & commandFilter);

            flamingo.AddInComing(startHandler);

            await flamingo.Fly();
        }

        private static async Task<bool> CallbackFunc(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText("Just started!");
            return true;
        }
    }
}
```

#### 8- Send command `/start` to your bot and see what happens.

#### You can write all of these using Attributes even more quicker
- [See SimpleFlamingo](https://github.com/immmdreza/FlamingoFramework/blob/master/Examples/SimpleFlamingo/Program.cs)

</details>

## Await-able InComing Handlers
Wait for user respond!
[Read Wiki](https://github.com/immmdreza/FlamingoFramework/wiki/Await-able-InComing-handlers)


## More to go
There are some example projects that may help you for now.

Full explanation example with a lot of comments: 
[FlamingoProduction](https://github.com/immmdreza/FlamingoFramework/tree/master/FlamingoProduction)

## Examples:

### Simple Flamingo
An example of how to create a simple Flamingo app + simple Attribute handlers usage:
- [SimpleFlamingo](https://github.com/immmdreza/FlamingoFramework/tree/master/Examples/SimpleFlamingo)
 
### FillForm Flamingo
See how to use await-able incoming handlers to wait for user answers and fill a sign up form
- [FillFormFlamingo](https://github.com/immmdreza/FlamingoFramework/tree/master/Examples/FillFormFlamingo)

### DeepInside Flamingo
In this example we show you how to go deep inside flamingo and create your own handlers and condiments. so you can use any custom properties in your handlers and even control lifecycle of db objects and etc.
- [DeepInsideFlamingo](https://github.com/immmdreza/FlamingoFramework/tree/master/Examples/DeepInsideFlamingo)
