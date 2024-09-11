using System.Net.WebSockets;
using Exchanges.Constants;
using Models.Bybit;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;


public class BybitWebSocketHandler:IExchangesWebSocketHandler
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
    public List<ExchangeWsTradeData> GetTradeList(string receivedMessage)
    {
        try
        {
            var tradeDataList = JsonConvert.DeserializeObject<BybitWsTradeDataList>(receivedMessage);
            return tradeDataList.tradeList;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, receivedMessage);
            return new List<ExchangeWsTradeData>();
        }
    }
}
