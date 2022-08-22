using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Api.Extensions;

public static class WebSocketExtensions {
    public static async Task<(WebSocketReceiveResult ReceiveResult, T? Message)> ReceiveAsync<T>(this WebSocket webSocket) {
        
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        var body = Encoding.ASCII.GetString(buffer);

        body = body.Replace("\0", "");

        T? data = default(T);

        if (body.Trim().Length > 0) {
            data = JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        return (receiveResult, data);
    }

    public static async Task SendAsync(this WebSocket webSocket, object? data) {
        await webSocket.SendAsync(
            JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }),
            WebSocketMessageType.Text, true,
            CancellationToken.None);
    }
}