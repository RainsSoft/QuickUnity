namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS MediatorMap contract.
    /// </summary>
    public interface IMediatorMap
    {
        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        /// <param name="mediator">The mediator.</param>
        void RegisterMediator(IMediator mediator);

        /// <summary>
        /// Retrieves the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <returns>The mediator object.</returns>
        IMediator RetrieveMediator(string mediatorName);

        /// <summary>
        /// Removes the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        void RemoveMediator(string mediatorName);
    }
}