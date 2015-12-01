namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS Mediator contract.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// The mediator name.
        /// </summary>
        string mediatorName
        {
            get;
        }

        /// <summary>
        /// Gets the view component.
        /// </summary>
        /// <value>
        /// The view component.
        /// </value>
        object viewComponent
        {
            get;
        }

        /// <summary>
        /// Called when [register].
        /// </summary>
        void OnRegister();

        /// <summary>
        /// Called when [remove].
        /// </summary>
        void OnRemove();
    }
}