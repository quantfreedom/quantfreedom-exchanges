using System.Net.WebSockets;
using Exchanges.Constants;
using Models.Bybit;

namespace Exchanges.Websockets;


public class BybitWebSocketHandler : IExchangesWebSocketHandler
{
    private readonly ClientWebSocket webSocket;

    public string WsUrl { get; set; }

    public WebSocketState State
    {
        get
        {
            return this.webSocket.State;
        }
    }

    public BybitWebSocketHandler()
    {
        this.webSocket = new ClientWebSocket();
        this.WsUrl = BybitConstants.WS_LINEAR_MAINNET;
    }

    public async Task CloseAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null)
    {
        await this.webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
    }

    public async Task CloseOutputAsync(WebSocketCloseStatus closeStatus, CancellationToken cancellationToken, string? statusDescription = null)
    {
        await this.webSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
    }

    public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        await this.webSocket.ConnectAsync(uri, cancellationToken);
    }

    public void Dispose() => this.webSocket.Dispose();

    public async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
    {
        var receiveResult = await this.webSocket.ReceiveAsync(buffer, cancellationToken);

        return receiveResult;
    }

    public async Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
    {
        await this.webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
    }
}
