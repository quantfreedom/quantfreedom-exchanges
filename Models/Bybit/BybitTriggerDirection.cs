namespace Models.Bybit
{
    public struct BybitTriggerDirection
    {
        public int Value
        {
            get; private set;
        }

        public static readonly BybitTriggerDirection TriggerOnRise = new(1);
        public static readonly BybitTriggerDirection TriggerOnFall = new(2);

        private BybitTriggerDirection(int value)
        {
            Value = value;
        }
    }
}
