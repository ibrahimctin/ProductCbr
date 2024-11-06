namespace ProductCbrAssignment.Common.ExceptionModels
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string productId) : base($"The product with the identifier {productId} was not found.")
        {
        }

    }
}