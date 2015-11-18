namespace QuickUnity.FSM
{
    /// <summary>
    /// The interface definition for the FSMState component.
    /// </summary>
    public interface IFSMState
    {
        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        string stateName
        {
            get;
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