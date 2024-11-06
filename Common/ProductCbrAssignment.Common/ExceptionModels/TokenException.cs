namespace ProductCbrAssignment.Common.ExceptionModels
{
    public class TokenException : Exception
    {
        public TokenException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
