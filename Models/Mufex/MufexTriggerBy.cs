namespace Models.Mufex
{
    public struct MufexTriggerBy
    {
        private MufexTriggerBy(string value)
        {
            Value = value;
        }
        public static MufexTriggerBy LastPrice => new MufexTriggerBy("LastPrice");
        public static MufexTriggerBy IndexPrice => new MufexTriggerBy("IndexPrice");
        public static MufexTriggerBy MarkPrice => new MufexTriggerBy("MarkPrice");
        public string Value
        {
            get; private set;
        }
        public override string ToString() => Value;
        public static implicit operator string(MufexTriggerBy MufexTriggerBy) => MufexTriggerBy.Value;
    }
}
