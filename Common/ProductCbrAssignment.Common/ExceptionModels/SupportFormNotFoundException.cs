namespace ProductCbrAssignment.Common.ExceptionModels
{
    public sealed class SupportFormNotFoundException : NotFoundException
    {
        public SupportFormNotFoundException(string supportFormId)
          : base($"The support form with the identifier {supportFormId} was not found.")
        {
        }
    }
}
