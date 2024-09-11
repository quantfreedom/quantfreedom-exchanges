using Models.Binance;
using Models.Exchange;
using Newtonsoft.Json;
using Serilog.Core;

namespace Exchanges.Websockets;


public class BinanceWebSocket : ExchangesWebSocket
{
    public BinanceWebSocket(Logger logger) : base(logger, BinanceConstants.WS_USDM_FUTURES_MAINNET) { }


    public override Dictionary<string, object> CreateSubscriptionMessage()
    {
        var subMessage = new Dictionary<string, object>
        {
            {"method", "SUBSCRIBE"},
            {"params", new string[] { "btcusdt@aggTrade" }},
            {"id", 1}
        };
        return subMessage;
    }
    public List<ExchangeWsTradeDataParsed> TradeStreamParser(string receivedMessage)
    {
        try
        {
            _logger.Debug("Going to deserialize Binance trade data");
            var BinanceTradeData = JsonConvert.DeserializeObject<BinanceWsTradeData>(receivedMessage);
            _logger.Debug("Creating ExchangeWsTradeDataParsed list");
            var exchangeTradeDataList = new List<ExchangeWsTradeDataParsed>
            {
                new ExchangeWsTradeDataParsed
                {
                    Exchange = "Binance",
                    Datetime = DateTimeOffset.FromUnixTimeMilliseconds(BinanceTradeData.Timestamp).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                    Timestamp = BinanceTradeData.Timestamp,
                    AssetQty = BinanceTradeData.AssetQty,
                    Price = BinanceTradeData.Price,
                    Side = BinanceTradeData.Side ? "Sell" : "Buy",
                    UsdtQty = Math.Round(BinanceTradeData.AssetQty * BinanceTradeData.Price,2)
                }
            };
            _logger.Debug("Returning ExchangeWsTradeDataParsed list");
            return exchangeTradeDataList;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, receivedMessage);
            return new List<ExchangeWsTradeDataParsed>();
        }
    }


}
