namespace mufex.net.api.Models
{
    public struct TimeInForce
    {
        private TimeInForce(string value)
        {
            Value = value;
        }

        public static TimeInForce GoodTillCancel
        {
            get => new("GoodTillCancel");
        }
        public static TimeInForce FillOrKill
        {
            get => new("FillOrKill");
        }
        public static TimeInForce ImmediateOrCancel
        {
            get => new("ImmediateOrCancel");
        }
        public static TimeInForce PostOnly
        {
            get => new("PostOnly");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(TimeInForce enm) => enm.Value;
        public override string ToString() => Value.ToString();
    }
}
