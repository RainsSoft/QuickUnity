using QuickUnity.Events;

namespace QuickUnity.FSM
{
    /// <summary>
    /// A FiniteStateMachine event.
    /// </summary>
    public class FSMEvent : Event
    {
        /// <summary>
        /// When the finite state machine make a state transition, it will dispatch this event.
        /// </summary>
        public const string STATE_TRANSITION = "stateTransition";

        /// <summary>
        /// The previous state.
        /// </summary>
        private IFSMState mPreviousState;

        /// <summary>
        /// Gets or sets the state of the previous.
        /// </summary>
        /// <value>
        /// The state of the previous.
        /// </value>
        public IFSMState previousState
        {
            get { return mPreviousState; }
            set { mPreviousState = value; }
        }

        /// <summary>
        /// The current state.
        /// </summary>
        private IFSMState mCurrentState;

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public IFSMState currentState
        {
            get { return mCurrentState; }
            set { mCurrentState = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        public FSMEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}