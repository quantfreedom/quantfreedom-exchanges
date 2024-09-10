namespace Models.Mufex
{
    public struct MufexTriggerDirection
    {
        public int Value
        {
            get; private set;
        }

        public static readonly MufexTriggerDirection TriggerOnRise = new(1);
        public static readonly MufexTriggerDirection TriggerOnFall = new(2);

        private MufexTriggerDirection(int value)
        {
            Value = value;
        }
    }
}
