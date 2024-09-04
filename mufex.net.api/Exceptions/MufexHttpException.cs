namespace mufex.net.api
{

    public class MufexHttpException : Exception
    {
        public MufexHttpException()
        {
        }

        public MufexHttpException(string? message) : base(message)
        {
        }

        public MufexHttpException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public int StatusCode { get; set; }

        public Dictionary<string, IEnumerable<string>>? Headers { get; set; }

    }
}