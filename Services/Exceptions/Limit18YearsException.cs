namespace Services.Exceptions
{
    public class Limit18YearsException : Exception
    {
        public Limit18YearsException(string? message) : base(message)
        {
        }
    }
}
