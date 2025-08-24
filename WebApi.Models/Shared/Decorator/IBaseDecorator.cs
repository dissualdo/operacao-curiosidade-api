namespace WebApi.Models.Shared.Decorator
{
    /// <summary>
    /// Abstract base decorator class that defines a structure for processing operations.
    /// Implements the Decorator design pattern for extending functionality dynamically.
    /// </summary>
    /// <typeparam name="T">The type of object that will be processed by the decorator.</typeparam>
    public abstract class IBaseDecorator<T> where T : class
    {
        /// <summary>
        /// Abstract method that defines an operation to be performed on the given request.
        /// This method must be implemented by derived classes.
        /// </summary>
        /// <param name="request">The object on which the operation will be performed.</param>
        /// <returns>The processed object of type <typeparamref name="T"/>.</returns>
        public abstract T Operation(T request);
    }
}
