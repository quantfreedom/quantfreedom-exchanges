namespace Models.Mufex;

public class MufexOrderStatusType
{

    private MufexOrderStatusType(string value)
    {
        Value = value;
    }

    public static MufexOrderStatusType Created
    {
        get => new("Created");
    }
    public static MufexOrderStatusType New
    {
        get => new("New");
    }
    public static MufexOrderStatusType Rejected
    {
        get => new("Rejected");
    }
    public static MufexOrderStatusType PartiallyFilled
    {
        get => new("PartiallyFilled");
    }
    public static MufexOrderStatusType Filled
    {
        get => new("Filled");
    }
    public static MufexOrderStatusType PendingCancel
    {
        get => new("PendingCancel");
    }
    public static MufexOrderStatusType Cancelled
    {
        get => new("Cancelled");
    }
    public static MufexOrderStatusType Untriggered
    {
        get => new("Untriggered");
    }
    public static MufexOrderStatusType Deactivated
    {
        get => new("Deactivated");
    }
    public static MufexOrderStatusType Triggered
    {
        get => new("Triggered");
    }
    public static MufexOrderStatusType Active
    {
        get => new("Active");
    }
    public string Value
    {
        get; private set;
    }
    public static implicit operator string(MufexOrderStatusType enm) => enm.Value;
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