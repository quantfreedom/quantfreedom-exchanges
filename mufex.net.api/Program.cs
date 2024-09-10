using System.Security.Cryptography.X509Certificates;
using Exchanges.Mufex;
using Exchanges.Services;
using Exchanges.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mufex.net.api.Keys;

var symbol = "BTCUSDT";
var coin = "USDT";

var services = new ServiceCollection();
services.AddTransient<IApiService, ApiService>();
var servicesProvider = services.BuildServiceProvider();
var apiService = servicesProvider.GetService<IApiService>();

var mufexExchange = new Mufex(
        apiService: apiService,
        apiKey: MufexMainTestAccount.ApiKey,
        secretKey: MufexMainTestAccount.Secret
        );

var response = await mufexExchange.GetAccountBalance(coin);
var a = response;
//MufexService MufexMainnet = new(
//    apiKey: MufexConstants.API_KEY,
//    apiSecret: MufexConstants.API_SECRET,
//    baseUrl: MufexConstants.HTTP_MAINNET_URL
//    );

// var orderId = await MufexMainnet.placers.PlaceOrder(
//     symbol: symbol,
//     side: Side.Buy,
//     orderType: OrderType.Market,
//     qty: 0.001,
//     timeInForce: TimeInForce.GoodTillCancel,
//     positionIdx: PositionIndex.HedgeModeBuySide,
//     stopLoss: 55400
//     );

// var tpOrderId = await MufexMainnet.placers.PlaceOrder(
//     symbol: symbol,
//     side: Side.Sell,
//     orderType: OrderType.Limit,
//     qty: 0.001,
//     price: 60000,
//     timeInForce: TimeInForce.PostOnly,
//     positionIdx: PositionIndex.HedgeModeBuySide,
//     reduceOnly: true
//     );

// var positionInfo = await MufexMainnet.getters.GetPositionInfo(symbol: symbol);
// var filledOrderInfo = await MufexMainnet.getters.GetFilledOrders(
//     symbol: symbol,
//     orderId: orderId
//     );
// var openOrders = await MufexMainnet.getters.GetOpenOrders(symbol: symbol);
// Console.ReadLine();

//await MufexMainnet.getters.GetCandles();

