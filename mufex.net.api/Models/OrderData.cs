namespace mufex.net.api.Models;

public class OrderData
{
    public string? orderId { get; set; }
    public string? orderLinkId { get; set; }

}

/*
https://www.mufex.finance/apidocs/derivatives/contract/index.html#t-dv_placeorder
{
    "code":0,
        "message":"OK",
        "data": {
        "orderId":"a09a43f1-7a65-4255-8758-034103447a4e",
        "orderLinkId":""
    },
    "ext_info":null,
        "time":1658850321861
}
*/
