namespace Models.Bybit;

public class BybitOrderStatusType
{

    private BybitOrderStatusType(string value)
    {
        Value = value;
    }

    public static BybitOrderStatusType Created
    {
        get => new("Created");
    }
    public static BybitOrderStatusType New
    {
        get => new("New");
    }
    public static BybitOrderStatusType Rejected
    {
        get => new("Rejected");
    }
    public static BybitOrderStatusType PartiallyFilled
    {
        get => new("PartiallyFilled");
    }
    public static BybitOrderStatusType Filled
    {
        get => new("Filled");
    }
    public static BybitOrderStatusType PendingCancel
    {
        get => new("PendingCancel");
    }
    public static BybitOrderStatusType Cancelled
    {
        get => new("Cancelled");
    }
    public static BybitOrderStatusType Untriggered
    {
        get => new("Untriggered");
    }
    public static BybitOrderStatusType Deactivated
    {
        get => new("Deactivated");
    }
    public static BybitOrderStatusType Triggered
    {
        get => new("Triggered");
    }
    public static BybitOrderStatusType Active
    {
        get => new("Active");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(BybitOrderStatusType enm) => enm.Value;
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