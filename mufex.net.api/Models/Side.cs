namespace mufex.net.api.Models
{
    public struct Side
    {
        private Side(string value)
        {
            Value = value;
        }

        public static Side Buy
        {
            get => new("Buy");
        }
        public static Side Sell
        {
            get => new("Sell");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(Side enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
