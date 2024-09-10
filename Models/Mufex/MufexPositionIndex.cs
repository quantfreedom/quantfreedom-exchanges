namespace Models.Mufex
{
    public struct MufexPositionIndex
    {
        private MufexPositionIndex(int value)
        {
            Value = value;
        }

        public static MufexPositionIndex OneWayMode => new(0);
        public static MufexPositionIndex HedgeModeBuySide => new(1);
        public static MufexPositionIndex HedgeModeSellSide => new(2);
        public int Value
        {
            get; private set;
        }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator int(MufexPositionIndex positionIdx) => positionIdx.Value;
    }
}
