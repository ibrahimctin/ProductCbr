namespace ProductCbrAssignment.Common.ExceptionModels
{
    public abstract class ApplicationLayerException : Exception
    {
        protected ApplicationLayerException(string title, string message)
            : base(message) =>
            Title = title;

        public string Title { get; }
    }
}
