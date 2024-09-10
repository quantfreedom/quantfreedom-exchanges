namespace Models.Bybit
{
    public struct BybitSide
    {
        private BybitSide(string value)
        {
            Value = value;
        }

        public static BybitSide Buy
        {
            get => new("Buy");
        }
        public static BybitSide Sell
        {
            get => new("Sell");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(BybitSide enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
