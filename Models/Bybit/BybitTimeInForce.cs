namespace Models.Bybit
{
    public struct BybitTimeInForce
    {
        private BybitTimeInForce(string value)
        {
            Value = value;
        }

        public static BybitTimeInForce GoodTillCancel
        {
            get => new("GTC");
        }
        public static BybitTimeInForce FillOrKill
        {
            get => new("FOK");
        }
        public static BybitTimeInForce ImmediateOrCancel
        {
            get => new("IOC");
        }
        public static BybitTimeInForce PostOnly
        {
            get => new("PostOnly");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(BybitTimeInForce enm) => enm.Value;
        public override string ToString() => Value.ToString();
    }
}
