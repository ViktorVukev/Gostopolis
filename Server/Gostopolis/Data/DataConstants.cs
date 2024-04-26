namespace Gostopolis.Data;

/// <summary>
/// Base constants used during some of the validations.
/// </summary>
public static class DataConstants
{
    /// <summary>
    /// Constants that are common for all of the bounded contexts.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Default name minimum length.
        /// </summary>
        public const int MinNameLength = 2;

        /// <summary>
        /// Default name maximum length.
        /// </summary>
        public const int MaxNameLength = 20;

        /// <summary>
        /// Instead of using hard-coded zero.
        /// </summary>
        public const int Zero = 0;
    }
}