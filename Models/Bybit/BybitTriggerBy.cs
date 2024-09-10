namespace Models.Bybit
{
    public struct BybitTriggerBy
    {
        private BybitTriggerBy(string value)
        {
            Value = value;
        }
        public static BybitTriggerBy LastPrice => new BybitTriggerBy("LastPrice");
        public static BybitTriggerBy IndexPrice => new BybitTriggerBy("IndexPrice");
        public static BybitTriggerBy MarkPrice => new BybitTriggerBy("MarkPrice");
        public string Value
        {
            get; private set;
        }
        public override string ToString() => Value;
        public static implicit operator string(BybitTriggerBy BybitTriggerBy) => BybitTriggerBy.Value;
    }
}
