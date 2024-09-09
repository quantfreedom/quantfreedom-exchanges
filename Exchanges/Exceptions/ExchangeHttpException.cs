namespace Exchanges.Exceptions;

public class ExchangeHttpException : Exception
{
        public ExchangeHttpException()
        {
        }

        public ExchangeHttpException(string? message) : base(message)
        {
        }

        public ExchangeHttpException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public int StatusCode { get; set; }

        public Dictionary<string, IEnumerable<string>>? Headers { get; set; }
}
