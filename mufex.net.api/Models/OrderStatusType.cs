namespace mufex.net.api.Models;

public class OrderStatusType
{

    private OrderStatusType(string value)
    {
        Value = value;
    }

    public static OrderStatusType Created
    {
        get => new("Created");
    }
    public static OrderStatusType New
    {
        get => new("New");
    }
    public static OrderStatusType Rejected
    {
        get => new("Rejected");
    }
    public static OrderStatusType PartiallyFilled
    {
        get => new("PartiallyFilled");
    }
    public static OrderStatusType Filled
    {
        get => new("Filled");
    }
    public static OrderStatusType PendingCancel
    {
        get => new("PendingCancel");
    }
    public static OrderStatusType Cancelled
    {
        get => new("Cancelled");
    }
    public static OrderStatusType Untriggered
    {
        get => new("Untriggered");
    }
    public static OrderStatusType Deactivated
    {
        get => new("Deactivated");
    }
    public static OrderStatusType Triggered
    {
        get => new("Triggered");
    }
    public static OrderStatusType Active
    {
        get => new("Active");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(OrderStatusType enm) => enm.Value;
    public override string ToString() => Value.ToString();
}

/*
Created - order has been accepted by the system but not yet put through the matching engine
New - order has been placed successfully
Rejected
PartiallyFilled
Filled
PendingCancel - matching engine has received the cancelation request but it may not be canceled successfully
Cancelled
For Conditional Orders Only:

Untriggered - order yet to be triggered
Deactivated - order has been canceled by the user before being triggered
Triggered - order has been triggered by last traded price
Active - order has been triggered and the new active order has been successfully placed. Is the final state of a successful conditional order
*/