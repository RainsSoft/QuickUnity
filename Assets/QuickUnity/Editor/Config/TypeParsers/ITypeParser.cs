namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The interface of TypeParser.
    /// </summary>
    public interface ITypeParser
    {
        /// <summary>
        /// Parses the type string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The parsed type string.</returns>
        string ParseType(string source);

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        object ParseValue(string value);
    }
}