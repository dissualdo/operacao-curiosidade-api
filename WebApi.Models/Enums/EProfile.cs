namespace WebApi.Models.Enums
{
    /// <summary>
    /// Enumeration that defines the available user profiles in the system.
    /// </summary>
    public enum EProfile
    {
        /// <summary>
        /// Standard user profile with limited permissions.
        /// </summary>
        User = 1,

        /// <summary>
        /// Administrator profile with advanced permissions.
        /// </summary>
        Admin = 2,
         
        /// <summary>
        /// System profile with advanced permissions.
        /// </summary>
        System = 3 
         
    }
}
