namespace Models.Mufex
{
    public struct MufexSide
    {
        private MufexSide(string value)
        {
            Value = value;
        }

        public static MufexSide Buy
        {
            get => new("Buy");
        }
        public static MufexSide Sell
        {
            get => new("Sell");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(MufexSide enm) => enm.Value;
        public override readonly string ToString() => Value.ToString();
    }
}
