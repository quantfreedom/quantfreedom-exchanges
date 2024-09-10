using System;
using System.Net.WebSockets;

namespace Exchanges.Websockets;

public interface IExchangesWebSocketHandler : IDisposable
{
    public string WsUrl { get; set; }
    WebSocketState State { get; }

    Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

    Task CloseOutputAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null);

    Task CloseAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null);

    Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

    Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);
}
