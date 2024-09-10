using System.Runtime.CompilerServices;
using Exchanges.Bybit;
using Exchanges.Mufex;
using Exchanges.Services;
using Exchanges.Websockets;
using Exchanges.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Bybit;
using Models.Mufex;
using mufex.net.api.Keys;

var symbol = "BTCUSDT";
var coin = "USDT";

var services = new ServiceCollection();
services.AddTransient<IApiService, ApiService>();
var servicesProvider = services.BuildServiceProvider();
var apiService = servicesProvider.GetService<IApiService>();

// var mufexExchange = new Mufex(
//         apiService: apiService,
//         apiKey: MufexKeys.Mainnet_TestAccount_ApiKey,
//         secretKey: MufexKeys.Mainnet_TestAccount_Secret,
//         useTestnet: false
//         );
// var result = await mufexExchange.GetAccountBalance(coin);

var bybitExchange = new Bybit(
       apiService: apiService,
       apiKey: BybitKeys.Testnet_Neo_ApiKey,
       secretKey: BybitKeys.Testnet_Neo_SecretKey,
       useTestnet: true
       );

var bybitWebSocket = new ExchangesWebSocket(handler: new BybitWebSocketHandler());
bybitWebSocket.OnMessageReceived(
    (data) =>
    {
        Console.WriteLine(data);

        return Task.CompletedTask;
    }, CancellationToken.None);
await bybitWebSocket.ConnectAsync(new string[] { "publicTrade.BTCUSDT" }, CancellationToken.None);
var a = 0;