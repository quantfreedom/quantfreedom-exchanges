namespace mufex.net.api;

public static class MufexConstants
{

    public const string API_KEY = "DUJrbVOWleEa6V0gxM";
    public const string API_SECRET = "CZoRij0aGI0T5jhsFr5mugDT1yFmIx1lHRfY";

    public const string HTTP_MAINNET_URL = "https://api.mufex.finance";
    public const string HTTP_TESTNET_URL = "https://api.testnet.mufex.finance";
    public const string DEFAULT_REC_WINDOW = "5000";
    public const string DEFAULT_SIGN_TYPE = "2";

    // WebSocket channels
    public const string WEBSOCKET_PUBLIC_MAINNET = "wss://ws.mufex.finance/realtime_public";
    public const string WEBSOCKET_PRIVATE_MAINNET = "wss://ws.mufex.finance/contract/private";

    public const string WEBSOCKET_PUBLIC_TESTNET = "wss://ws.mufex.finance/realtime_public";
    public const string WEBSOCKET_PRIVATE_TESTNET = "wss://ws.mufex.finance/contract/private";
    // public static List<object> MufexConstants = new List<object> { 1, 5, 15, 30, 60, 120, 240, 360, 720, "D", "W" };
    // public static readonly MufexConstants MufexConstants => new List<object> { 1, 5, 15, 30, 60, 120, 240, 360, 720, "D", "W" };
}
