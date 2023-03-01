namespace Services.Exceptions
{
    public class ExistenceException : Exception
    {
        public ExistenceException(string? message) : base(message)
        {
        }
    }
}
