namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The interface of TypeParser.
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        object Parse(string value);
    }
}