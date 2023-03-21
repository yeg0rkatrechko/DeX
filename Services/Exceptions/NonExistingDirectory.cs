namespace Services.Exceptions
{
    internal class NonExistingDirectory : Exception
    {
        public NonExistingDirectory(string? message) : base(message)
        { 
        }
    }
}
