namespace Services.Exceptions
{
    public class NoPassData : Exception
    {
        public NoPassData(string? message) : base(message)
        {
        }
    }
}