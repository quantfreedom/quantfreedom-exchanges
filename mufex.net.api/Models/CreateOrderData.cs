using Newtonsoft.Json;

namespace mufex.net.api.Models;

public class CreateOrderData
{
    /// <summary>
    /// https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_placeorder
    /// </summary>
    public string? orderId { get; set; }
    public string? orderLinkId { get; set; }
    
}

