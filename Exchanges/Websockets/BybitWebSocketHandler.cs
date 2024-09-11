using System.Net.WebSockets;
using Exchanges.Constants;
using Models.Bybit;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;


public class BybitWebSocketHandler : IExchangesWebSocketHandler
{
    private readonly ClientWebSocket webSocket;

    public string WsUrl
    {
        get; set;
    }

    public WebSocketState State
    {
        get
        {
            return this.webSocket.State;
        }
    }
    public Logger _logger
    {
        get; set;
    }

    public BybitWebSocketHandler()
    {
        this.webSocket = new ClientWebSocket();
        this.WsUrl = BybitConstants.WS_LINEAR_MAINNET;
    }

    public async Task CloseAsync(
        WebSocketCloseStatus closeStatus,
        CancellationToken cancellationToken,
        string? statusDescription = null)
    {
        await this.webSocket.CloseAsync(
            closeStatus,
            statusDescription,
            cancellationToken);
    }

    public async Task CloseOutputAsync(
        WebSocketCloseStatus closeStatus,
        CancellationToken cancellationToken,
        string? statusDescription = null)
    {
        await this.webSocket.CloseOutputAsync(
            closeStatus,
            statusDescription,
            cancellationToken);
    }

    public async Task ConnectAsync(
        Uri uri,
        CancellationToken cancellationToken)
    {
        await this.webSocket.ConnectAsync(
            uri,
            cancellationToken);
    }


    public async Task<WebSocketReceiveResult> ReceiveAsync(
        ArraySegment<byte> buffer,
        CancellationToken cancellationToken)
    {
        var receiveResult = await this.webSocket.ReceiveAsync(
            buffer,
            cancellationToken);

        return receiveResult;
    }

    public async Task SendAsync(
        ArraySegment<byte> buffer,
        WebSocketMessageType messageType,
        bool endOfMessage,
        CancellationToken cancellationToken)
    {
        await this.webSocket.SendAsync(
            buffer,
            messageType,
            endOfMessage,
            cancellationToken);
    }
    public void Dispose() => this.webSocket.Dispose();
    public (List<ExchangeWsTradeData>, string) GetTradeList(string receivedMessage)
    {
        try
        {
            BybitWsTradeDataList bybitTradeData = JsonConvert.DeserializeObject<BybitWsTradeDataList>(receivedMessage);
            var bybitSerializedTradeDataList = JsonConvert.SerializeObject(bybitTradeData.tradeList);
            var exchangeTradeDataList = JsonConvert.DeserializeObject<List<ExchangeWsTradeData>>(bybitSerializedTradeDataList);
            return (exchangeTradeDataList, "Bybit");
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, receivedMessage);
            return (new List<ExchangeWsTradeData>(), "Bybit");
        }
    }

    public Dictionary<string, object> CreateSubscriptionMessage(string[] args)
    {

        var subMessage = new Dictionary<string, object>
        {
            {"req_id", Guid.NewGuid().ToString()},
            {"op", "subscribe"},
            {"args", args},
        };

        return subMessage;
    }
}
