using System.Net.WebSockets;
using System.Text;
using Exchnages.Utils;
using Models.Bybit;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;

public abstract class ExchangesWebSocket
{
    public readonly Logger _logger;
    private readonly string WsUrl;
    private readonly List<Func<string, Task>> OnMessageReceivedFunctions = new();
    private readonly List<CancellationTokenRegistration> OnMessageReceivedCancellationTokenRegistrations = new();
    private CancellationTokenSource? loopCancellationTokenSource;
    private readonly int PingInterval = 20;
    private readonly int ReceiveBufferSize = 262_144;
    private readonly ClientWebSocket WebSocket = new();
    public abstract Dictionary<string, object> CreateSubscriptionMessage();


    public ExchangesWebSocket(Logger logger, string wsUrl) { this._logger = logger; this.WsUrl = wsUrl; }
    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        if (this.WebSocket.State != WebSocketState.Open)
        {
            this.loopCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await this.WebSocket.ConnectAsync(new Uri(this.WsUrl), cancellationToken);

            _ = Task.Run(() => Ping(this.loopCancellationTokenSource.Token), cancellationToken);

            await SendSubscription();
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
            _logger.Debug("Ping loop");
            await Task.Delay(TimeSpan.FromSeconds(this.PingInterval), token);
            if (this.WebSocket.State == WebSocketState.Open)
            {
                _logger.Debug("Sending ping");
                await SendAsync("{\"op\":\"ping\"}", CancellationToken.None);
                await Console.Out.WriteLineAsync("ping sent");
            }
        }
    }
    public async Task SendAsync(string message, CancellationToken cancellationToken)
    {
        byte[] byteArray = Encoding.ASCII.GetBytes(message);

        await this.WebSocket.SendAsync(new ArraySegment<byte>(byteArray), WebSocketMessageType.Text, true, cancellationToken);
    }
    private async Task SendSubscription()
    {
        var subMessage = this.CreateSubscriptionMessage();
        string subMessageJson = JsonConvert.SerializeObject(subMessage);

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
                receiveResult = await this.WebSocket.ReceiveAsync(buffer, cancellationToken);
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
    public async Task DisconnectAsync(
        CancellationToken cancellationToken,
        string? statusDescription = null)
    {
        this.loopCancellationTokenSource?.Cancel();

        if (this.WebSocket.State == WebSocketState.Open)
        {
            await this.WebSocket.CloseOutputAsync(
                WebSocketCloseStatus.NormalClosure,
                statusDescription,
                cancellationToken);
            await this.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, statusDescription, cancellationToken);
        }
    }
    public void Dispose()
    {
        this.DisconnectAsync(CancellationToken.None).Wait();

        this.WebSocket.Dispose();

        this.OnMessageReceivedCancellationTokenRegistrations.ForEach(ct => ct.Dispose());

        if (loopCancellationTokenSource != null)
            this.loopCancellationTokenSource.Dispose();
    }



}
