using BotInterface.Forms;
using TelegramBotBase.Builder;
using TelegramBotBase.Commands;
using Microsoft.Extensions.DependencyInjection;
using BotInterface;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var serviceCollection = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<ApiClient>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var bot = BotBaseBuilder
    .Create()
    .WithAPIKey(configuration["BotToken"]
        ?? throw new InvalidOperationException("BotToken is not configured."))
    .DefaultMessageLoop()
    .WithServiceProvider<MainMenuForm>(serviceProvider)
    .NoProxy()
    .CustomCommands(a =>
    {
        a.Start("Start the bot");
    })
    .NoSerialization()
    .UseEnglish()
    .UseSingleThread()
    .Build();

await bot.UploadBotCommands();
await bot.Start();

Console.WriteLine("Bot started");
Console.ReadLine();

await bot.Stop();