namespace Services.Exceptions
{
    public class Under18 : Exception
    {
        public Under18(string? message) : base(message)
        {
        }
    }
}
