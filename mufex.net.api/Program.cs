using System.Net.WebSockets;
using System.Text;
using mufex.net.api;
using mufex.net.api.Models;
using mufex.net.api.Services;
using Newtonsoft.Json;

var symbol = "BTCUSDT";

// MufexService MufexMainnet = new(
//     apiKey: MufexConstants.API_KEY,
//     apiSecret: MufexConstants.API_SECRET,
//     baseUrl: MufexConstants.HTTP_MAINNET_URL
//     );

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

var uri = new Uri("wss://ws.mufex.finance/realtime_public");
using (var client = new ClientWebSocket())
{
    try
    {
        // Connect to the WebSocket server
        await client.ConnectAsync(uri, CancellationToken.None);
        Console.WriteLine("Connected!");

        // Send a message
        var message = new Dictionary<string, object>
            {
                { "op", "subscribe" },
                { "req_id", "10110001" },
                { "args", new string[] { "trades-100.BTCUSDT" } }
            };

        var jsonMessage = JsonConvert.SerializeObject(message);
        var buffer = Encoding.UTF8.GetBytes(jsonMessage);
        var segment = new ArraySegment<byte>(buffer);
        await client.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        Console.WriteLine("Message sent!");

        while (true)
        {
            // Optionally, receive a response
            var receiveBuffer = new byte[100000];
            var response = await client.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, response.Count);
            Console.WriteLine("Received: " + receivedMessage);
            if (receivedMessage.Contains("success"))
            {
                continue;
            }
            var jsonResult = JsonConvert.DeserializeObject<WsTradeData>(receivedMessage);
            var parsedData = MufexWsService.ParseModel(jsonResult);
            var testing = 1;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex.Message);
    }
    finally
    {
        // Close the connection
        if (client.State == WebSocketState.Open)
        {
            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            Console.WriteLine("Connection closed.");
        }
    }
}
