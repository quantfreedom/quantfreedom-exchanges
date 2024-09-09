// using System;

// namespace mufex.net.api.Services;

// public class MufexWsService
// {

//     var uri = new Uri("wss://ws.mufex.finance/realtime_public");
// using (var client = new ClientWebSocket())
// {
//     try
//     {
//         // Connect to the WebSocket server
//         await client.ConnectAsync(uri, CancellationToken.None);
// Console.WriteLine("Connected!");

//         // Send a message
//         var message = new Dictionary<string, object>
//             {
//                 { "op", "subscribe" },
//                 { "req_id", "10110001" },
//                 { "args", new string[] { "trades-100.BTCUSDT" } }
//             };

// var jsonMessage = JsonConvert.SerializeObject(message);
// var buffer = Encoding.UTF8.GetBytes(jsonMessage);
// var segment = new ArraySegment<byte>(buffer);
// await client.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
// Console.WriteLine("Message sent!");

//         while (true)
//         {
//             // Optionally, receive a response
//             var receiveBuffer = new byte[100000];
// var response = await client.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
// var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, response.Count);
// Console.WriteLine("Received: " + receivedMessage);
//             if (receivedMessage.Contains("success"))
//             {
//                 continue;
//             }
//             var jsonResult = JsonConvert.DeserializeObject<WsTradeData>(receivedMessage);
// var parsedData = MufexUtils.WsTradeParseModel(jsonResult);
// var testing = 1;
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine("An error occurred: " + ex.Message);
//     }
//     finally
//     {
//     // Close the connection
//     if (client.State == WebSocketState.Open)
//     {
//         await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
//         Console.WriteLine("Connection closed.");
//     }
// }
