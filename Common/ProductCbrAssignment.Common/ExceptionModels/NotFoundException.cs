namespace ProductCbrAssignment.Common.ExceptionModels
{
    public class NotFoundException : ApplicationLayerException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}