namespace Models.Mufex
{
    public struct MufexTimeInForce
    {
        private MufexTimeInForce(string value)
        {
            Value = value;
        }

        public static MufexTimeInForce GoodTillCancel
        {
            get => new("GoodTillCancel");
        }
        public static MufexTimeInForce FillOrKill
        {
            get => new("FillOrKill");
        }
        public static MufexTimeInForce ImmediateOrCancel
        {
            get => new("ImmediateOrCancel");
        }
        public static MufexTimeInForce PostOnly
        {
            get => new("PostOnly");
        }
        public string Value
        {
            get; private set;
        }
        public static implicit operator string(MufexTimeInForce enm) => enm.Value;
        public override string ToString() => Value.ToString();
    }
}
