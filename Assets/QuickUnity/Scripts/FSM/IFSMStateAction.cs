namespace QuickUnity.FSM
{
    /// <summary>
    /// The interface definition for the FSMStateAction component.
    /// </summary>
    public interface IFSMStateAction
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IFSMStateAction"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Called when [enter].
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Called when [fixed update].
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// Called when [update].
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// Called when [late update].
        /// </summary>
        void OnLateUpdate();

        /// <summary>
        /// Called when [exit].
        /// </summary>
        void OnExit();
    }
}