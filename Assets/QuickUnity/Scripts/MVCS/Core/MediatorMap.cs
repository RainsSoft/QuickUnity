namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IMediatorMap</c> implementation.
    /// </summary>
    public class MediatorMap : DataMap<string, IMediator>, IMediatorMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorMap"/> class.
        /// </summary>
        public MediatorMap()
            : base()
        {
        }

        #region Public Functions

        #region IMediatorMap Implementations

        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        /// <param name="mediator">The mediator.</param>
        public void RegisterMediator(IMediator mediator)
        {
            if (mediator != null)
                Register(mediator.mediatorName, mediator);
        }

        /// <summary>
        /// Retrieves the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <returns>The mediator object.</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            if (!string.IsNullOrEmpty(mediatorName))
                return Retrieve(mediatorName);

            return null;
        }

        /// <summary>
        /// Removes the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        public void RemoveMediator(string mediatorName)
        {
            if (!string.IsNullOrEmpty(mediatorName))
                Remove(mediatorName);
        }

        #endregion IMediatorMap Implementations

        #endregion Public Functions
    }
}