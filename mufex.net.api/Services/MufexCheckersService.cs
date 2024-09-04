using mufex.net.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace mufex.net.api.Services;

public class MufexCheckersService
{
    private MufexGettersService getters;
    public async Task<bool?> CheckOrderFilled(
        string symbol,
        string orderId
    )
    {
        var result = await getters.GetFilledOrders(
            symbol: symbol,
            orderId: orderId
        );
        var receivedOrderId = result[0].orderId;
        var isFilled = receivedOrderId == orderId;
        return isFilled;
    }
    public async Task<bool?> CheckOrderCanceled(
        string symbol,
        string orderId
    )
    {
        var query = new Dictionary<string, object>{
            {"symbol", symbol},
            {"orderId", orderId}
        };

        var jsonResult = await getters.GetOrderHistory(
            symbol: symbol,
            orderId: orderId
        );

        var jsonResultList = jsonResult.data.list;
        var receivedOrderId = jsonResultList[0].orderId;
        var receivedOrderStatus = jsonResultList[0].orderStatus;

        var isOrderCanceled = receivedOrderStatus == OrderStatusType.Cancelled;
        var isOrderDeactivated = receivedOrderStatus == OrderStatusType.Deactivated;
        var isOrderSameId = receivedOrderId == orderId;
        
        var isCanceled = (isOrderCanceled || isOrderDeactivated) && isOrderSameId;
        return isCanceled;
    }
    public async Task<bool?> CheckOrderActive(
        string symbol,
        string orderId
    )
    {
        var query = new Dictionary<string, object>{
            {"symbol", symbol},
            {"orderId", orderId}
        };

        var jsonResult = await getters.GetOrderHistory(
            symbol: symbol,
            orderId: orderId
        );

        var jsonResultList = jsonResult.data.list;
        var receivedOrderId = jsonResultList[0].orderId;
        var receivedOrderStatus = jsonResultList[0].orderStatus;

        var isOrderNew = receivedOrderStatus == OrderStatusType.New;
        var isOrderUntriggered = receivedOrderStatus == OrderStatusType.Untriggered;
        var isOrderSameId = receivedOrderId == orderId;
        
        var isCanceled = (isOrderNew || isOrderUntriggered) && isOrderSameId;
        return isCanceled;
    }
}
