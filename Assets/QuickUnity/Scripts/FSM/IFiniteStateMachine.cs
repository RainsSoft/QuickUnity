using QuickUnity.Events;

namespace QuickUnity.FSM
{
    /// <summary>
    /// The interface definition for the FiniteStateMachine component.
    /// </summary>
    public interface IFiniteStateMachine : IEventDispatcher
    {
        /// <summary>
        /// Gets the name of finite state machine.
        /// </summary>
        /// <value>
        /// The name of finite state machine.
        /// </value>
        string name
        {
            get;
        }

        /// <summary>
        /// Enters the state.
        /// </summary>
        /// <param name="state">The state object.</param>
        void EnterState(IFSMState state);

        /// <summary>
        /// This function is called every fixed framerate frame.
        /// </summary>
        void FixedUpdate();

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        void Update();

        /// <summary>
        /// LateUpdate is called every frame.
        /// </summary>
        void LateUpdate();
    }
}