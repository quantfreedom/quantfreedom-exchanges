using Models.Exchange;
using System;
using System.Net.WebSockets;
using Serilog.Core;

namespace Exchanges.Websockets;

public interface IExchangesWebSocketHandler:IDisposable
{
    public string WsUrl
    {
        get; set;
    }
    WebSocketState State
    {
        get;
    }

    public Logger _logger
    {
        get; set;
    }

    Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

    Task CloseOutputAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null);

    Task CloseAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null);

    Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

    Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);
    List<ExchangeWsTradeData> GetTradeList(string receivedMessage);
}
