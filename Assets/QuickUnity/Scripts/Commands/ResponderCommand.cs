namespace QuickUnity.Commands
{
    /// <summary>
    /// A base <c>IResponderCommand</c> implementation.
    /// </summary>
    public abstract class ResponderCommand : Command, IResponderCommand
    {
        #region API

        /// <summary>
        /// This method is called by a service when the return value has been received.
        /// </summary>
        /// <param name="data">The data.</param>
        public abstract void Result(object data);

        /// <summary>
        /// This method is called by a service when an error has been received.
        /// </summary>
        /// <param name="info">The information.</param>
        public virtual void Fault(object info)
        {
            DispatchEvent(new CommandEvent(CommandEvent.ERROR, this));
        }

        #endregion API
    }
}