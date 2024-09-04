using System;

namespace mufex.net.api.Utils;

public class ExchangeConstants
{
    public List<object> EXCHANGE_TIMEFRAMES = new List<object> { "1m", "5m", "15m", "30m", "1h", "2h", "4h", "6h", "12h", "d", "w" };
    public List<int> EXCHANGE_TIMEFRAMES_IN_MINUTES = new List<int> { 1, 5, 15, 30, 60, 120, 240, 360, 720, 1440, 10080 };

}
