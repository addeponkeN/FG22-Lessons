namespace Util
{
    //  manually looping indexer
    public struct IndexLooper
    {
        public int Value { get; set; }
        public int Length { get; set; }

        public IndexLooper(int length)
        {
            Length = length;
            Value = 0;
        }

        public int Next()
        {
            Value++;
            return Value %= Length;
        }
    
        public int Previous()
        {
            Value--;
            if (Value < 0)
                Value = Length - 1;
            return Value;
        }

        public static implicit operator int(IndexLooper ind) => ind.Value;
        public static implicit operator IndexLooper(int length) => new IndexLooper(length);

    }
}