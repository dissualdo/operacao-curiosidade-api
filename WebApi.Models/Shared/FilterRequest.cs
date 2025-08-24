namespace WebApi.Models.Shared
{
    /// <summary>
    /// Represents a request for filtering data with pagination.
    /// </summary>
    public class FilterRequest
    {
        /// <summary>
        /// The number of items to be retrieved per page.
        /// Defaults to 10.
        /// </summary>
        public int ItensQuantity { get; set; } = 10;

        /// <summary>
        /// The current page number for pagination.
        /// Defaults to 1.
        /// </summary>
        public int CurrentPage { get; set; } = 1;
    }
}
