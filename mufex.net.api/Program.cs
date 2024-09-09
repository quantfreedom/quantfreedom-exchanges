using Microsoft.Extensions.Configuration;
using mufex.net.api;
using mufex.net.api.Services;

var symbol = "BTCUSDT";

var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

var configuration = builder.Build();


MufexService MufexMainnet = new(
    apiKey: MufexConstants.API_KEY,
    apiSecret: MufexConstants.API_SECRET,
    baseUrl: MufexConstants.HTTP_MAINNET_URL
    );

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

await MufexMainnet.getters.GetCandles();

