namespace ProductCbrAssignment.Common.ExceptionModels
{
    public class HandledException:Exception
    {
        public HandledException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
