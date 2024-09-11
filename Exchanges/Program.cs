using Exchanges.Websockets;
using Newtonsoft.Json;
using Serilog;
using Exchanges.Utils;

using var log = new LoggerConfiguration()
    .Enrich.WithCaller()
    .MinimumLevel.Debug()
    .WriteTo.File($"../../../logs/logs-{DateTime.Now:HH-mm-ss}-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} - {Level:u} - {Caller} {Message} {NewLine}{Exception}")
    .CreateLogger();

//var symbol = "BTCUSDT";
//var coin = "USDT";

//var services = new ServiceCollection();
//services.AddTransient<IApiService, ApiService>();
//var servicesProvider = services.BuildServiceProvider();
//var apiService = servicesProvider.GetService<IApiService>();

// var mufexExchange = new Mufex(
//         apiService: apiService,
//         apiKey: MufexKeys.Mainnet_TestAccount_ApiKey,
//         secretKey: MufexKeys.Mainnet_TestAccount_Secret,
//         useTestnet: false
//         );
// var result = await mufexExchange.GetAccountBalance(coin);

//var bybitExchange = new Bybit(
//       apiService: apiService,
//       apiKey: BybitKeys.Testnet_Neo_ApiKey,
//       secretKey: BybitKeys.Testnet_Neo_SecretKey,
//       useTestnet: true
//       );

var binanceWebSocket = new BinanceWebSocket(logger: log);
binanceWebSocket.OnMessageReceived(
    (data) =>
    {
        if (data.Contains("success") || data.Contains("pong") || data.Contains("null"))
        {
            log.Debug("Success");
            Console.WriteLine("Success");
            return Task.CompletedTask;
        }
        var parsedData = binanceWebSocket.TradeStreamParser(data);
        foreach (var item in parsedData)
        {
            log.Debug($"Trade Data looping through parsedData UsdtQty= {item.UsdtQty}");
            if (item.UsdtQty > 50_000)
            {
                try
                {
                    var serializedTrade = JsonConvert.SerializeObject(item, Formatting.Indented);
                    log.Information("Trade Data: \n" + serializedTrade + "\n");
                    Console.WriteLine("Trade Data: \n" + serializedTrade + "\n");
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Error serializing trade data");
                    Console.WriteLine($"{ex} Error serializing trade data");
                }

            }
        }
        return Task.CompletedTask;
    }, CancellationToken.None);
await binanceWebSocket.ConnectAsync(CancellationToken.None);

// var bybitWebSocket = new BybitWebSocket(logger: log);
// bybitWebSocket.OnMessageReceived(
//     (data) =>
//     {
//         if (data.Contains("success"))
//         {
//             Console.WriteLine("Success");
//             return Task.CompletedTask;
//         }
//         var parsedData = bybitWebSocket.TradeStreamParser(data);
//         foreach (var item in parsedData)
//         {
//             if (item.UsdtQty > 100_000)
//             {
//                 var serializedTrade = JsonConvert.SerializeObject(item, Formatting.Indented);
//                 Console.WriteLine("Trade Data: \n" + serializedTrade + "\n");
//             }
//         }
//         return Task.CompletedTask;
//     }, CancellationToken.None);
// await bybitWebSocket.ConnectAsync(CancellationToken.None);
Console.WriteLine("Press 'q' to quit the application.");
while (true) { }