namespace QuickUnity.Commands
{
    /// <summary>
    /// The interface definition for the ResponderCommand component.
    /// </summary>
    public interface IResponderCommand : ICommand
    {
        /// <summary>
        /// This method is called by a service when the return value has been received.
        /// </summary>
        /// <param name="data">The data.</param>
        void Result(object data);

        /// <summary>
        /// This method is called by a service when an error has been received.
        /// </summary>
        /// <param name="info">The information.</param>
        void Fault(object info);
    }
}