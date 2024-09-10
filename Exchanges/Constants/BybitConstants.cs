using System;

namespace Exchanges.Constants;

public class BybitConstants
{
    public const string HTTP_MAINNET_URL = "https://api.bybit.com";
    public const string HTTP_MAINNET_BACKUP_URL = "https://api.bytick.com";
    public const string HTTP_TESTNET_URL = "https://api-testnet.bybit.com";
    // WebSocket public channel - Mainnet
    public const string WS_SPOT_MAINNET = "wss://stream.bybit.com/v5/public/spot";
    public const string WS_LINEAR_MAINNET = "wss://stream.bybit.com/v5/public/linear";
    public const string WS_INVERSE_MAINNET = "wss://stream.bybit.com/v5/public/inverse";
    public const string WS_OPTION_MAINNET = "wss://stream.bybit.com/v5/public/option";

    // WebSocket public channel - Testnet
    public const string WS_SPOT_TESTNET = "wss://stream-testnet.bybit.com/v5/public/spot";
    public const string WS_LINEAR_TESTNET = "wss://stream-testnet.bybit.com/v5/public/linear";
    public const string WS_INVERSE_TESTNET = "wss://stream-testnet.bybit.com/v5/public/inverse";
    public const string WS_OPTION_TESTNET = "wss://stream-testnet.bybit.com/v5/public/option";

    // WebSocket private channel
    public const string WS_PRIVATE_MAINNET = "wss://stream.bybit.com/v5/private";
    public const string WS_PRIVATE_TESTNET = "wss://stream-testnet.bybit.com/v5/private";
    public const string WS_V3_CONTRACT_PRIVATE = "wss://stream.bybit.com/contract/private/v3";
    public const string WS_V3_UNIFIED_PRIVATE = "wss://stream.bybit.com/unified/private/v3";
    public const string WS_V3_SPOT_PRIVATE = "wss://stream.bybit.com/spot/private/v3";
}
