namespace Models.Bybit
{
    public struct BybitPositionIndex
    {
        private BybitPositionIndex(int value)
        {
            Value = value;
        }

        public static BybitPositionIndex OneWayMode => new(0);
        public static BybitPositionIndex HedgeModeBuySide => new(1);
        public static BybitPositionIndex HedgeModeSellSide => new(2);
        public int Value
        {
            get; private set;
        }
        public override readonly string ToString() => Value.ToString();
        public static implicit operator int(BybitPositionIndex positionIdx) => positionIdx.Value;
    }
}
