using System;
using System.Net.WebSockets;
using System.Text;
using Exchanges.Exceptions;
using Exchnages.Utils;
using Models.Bybit;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;

public class ExchangesWebSocket : IDisposable
{
    protected readonly Logger _logger;
    private readonly IExchangesWebSocketHandler Handler;
    private readonly List<Func<string, Task>> OnMessageReceivedFunctions;
    private readonly List<CancellationTokenRegistration> OnMessageReceivedCancellationTokenRegistrations;
    private CancellationTokenSource? loopCancellationTokenSource;
    private readonly int PingInterval;
    private readonly int ReceiveBufferSize;

    public ExchangesWebSocket(
        Logger logger,
        IExchangesWebSocketHandler handler,
        int pingInterval = 20,
        int receiveBufferSize = 262_144
    )
    {
        this._logger = logger;
        this.Handler = handler;
        this.Handler._logger = logger;
        this.ReceiveBufferSize = receiveBufferSize;
        this.PingInterval = pingInterval;
        this.OnMessageReceivedFunctions = new List<Func<string, Task>>();
        this.OnMessageReceivedCancellationTokenRegistrations = new List<CancellationTokenRegistration>();
    }
    public async Task ConnectAsync(string[] args, CancellationToken cancellationToken)
    {
        if (this.Handler.State != WebSocketState.Open)
        {
            this.loopCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await Handler.ConnectAsync(new Uri(this.Handler.WsUrl), cancellationToken);

            _ = Task.Run(() => Ping(this.loopCancellationTokenSource.Token), cancellationToken);

            await SendSubscription(args);
            await Task.Factory.StartNew(() => this.ReceiveLoop(this.loopCancellationTokenSource.Token, this.ReceiveBufferSize),
                this.loopCancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }
    }
    public void OnMessageReceived(Func<string, Task> onMessageReceived, CancellationToken cancellationToken)
    {
        this.OnMessageReceivedFunctions.Add(onMessageReceived);

        if (cancellationToken != CancellationToken.None)
        {
            var reg = cancellationToken.Register(() =>
                this.OnMessageReceivedFunctions.Remove(onMessageReceived));

            this.OnMessageReceivedCancellationTokenRegistrations.Add(reg);
        }
    }
    private async Task Ping(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(this.PingInterval), token);
            if (this.Handler.State == WebSocketState.Open)
            {
                await SendAsync("{\"op\":\"ping\"}", CancellationToken.None);
                await Console.Out.WriteLineAsync("ping sent");
            }
        }
    }
    public async Task SendAsync(string message, CancellationToken cancellationToken)
    {
        byte[] byteArray = Encoding.ASCII.GetBytes(message);

        await this.Handler.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, cancellationToken);
    }
    private async Task SendSubscription(string[] args)
    {
        ExchangeUtils.EnsureNoDuplicates(args);
        var subMessage = new
        {
            req_id = Guid.NewGuid().ToString(),
            op = "subscribe",
            args = args
        };
        _logger.Debug("going to serialize object");
        string subMessageJson = JsonConvert.SerializeObject(subMessage);
        _logger.Debug("serialized object");
        await Console.Out.WriteLineAsync($"send subscription {subMessageJson}");
        await SendAsync(subMessageJson, CancellationToken.None);
    }
    private async Task ReceiveLoop(CancellationToken cancellationToken, int receiveBufferSize = 8192)
    {
        WebSocketReceiveResult receiveResult;
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var buffer = new ArraySegment<byte>(new byte[receiveBufferSize]);
                receiveResult = await this.Handler.ReceiveAsync(buffer, cancellationToken);
                var resized = new byte[buffer.Count];
                Array.Copy(buffer.Array, buffer.Offset, resized, 0, buffer.Count);
                
                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                string content = Encoding.UTF8.GetString(buffer.ToArray(), buffer.Offset, buffer.Count).Replace("\0", string.Empty);
                this.OnMessageReceivedFunctions.ForEach(omrf => omrf(content));
            }
        }
        catch (TaskCanceledException)
        {
            await this.DisconnectAsync(CancellationToken.None);
        }
    }
    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        this.loopCancellationTokenSource?.Cancel();

        if (this.Handler.State == WebSocketState.Open)
        {
            await this.Handler.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, cancellationToken);
            await this.Handler.CloseAsync(WebSocketCloseStatus.NormalClosure, cancellationToken);
        }
    }
    public void Dispose()
    {
        this.DisconnectAsync(CancellationToken.None).Wait();

        this.Handler.Dispose();

        this.OnMessageReceivedCancellationTokenRegistrations.ForEach(ct => ct.Dispose());

        if (loopCancellationTokenSource != null)
            this.loopCancellationTokenSource.Dispose();
    }

    public List<ExchangeWsTradeDataParsed> TradeStreamParser(string receivedMessage)
    {
        var tradeDataList = this.Handler.GetTradeList(receivedMessage);
        var parsedData = WsTradeParseModel(tradeDataList);
        return parsedData;
    }
    private List<ExchangeWsTradeDataParsed> WsTradeParseModel(List<ExchangeWsTradeData> tradeDataList)
    {
        var parsedData = tradeDataList
            .GroupBy(t => new { Price = t.price, Time = t.timestamp, Side = t.side })
            .Select(t =>
            {
                var assetQty = t.Sum(x => x.assetQty);
                var price = t.Key.Price;
                var timestamp = t.Key.Time;
                var side = t.Key.Side == "Buy" ? "Buyer" : "Seller";

                return new ExchangeWsTradeDataParsed
                {
                    Timestamp = timestamp,
                    AssetQty = assetQty,
                    Price = price,
                    Side = side,
                    UsdtQty = assetQty * price
                };
            })
            .ToList();
        return parsedData;
    }


}
