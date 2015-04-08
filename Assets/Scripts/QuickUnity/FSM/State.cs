/// <summary>
/// The FSM namespace.
/// </summary>
namespace QuickUnity.FSM
{
    /// <summary>
    /// Class State.
    /// </summary>
    public class State
    {
        /// <summary>
        /// The delegate of entering state.
        /// </summary>
        protected FiniteStateMachine.EnterState mEnterDelegate;

        /// <summary>
        /// The delegate of pushing state.
        /// </summary>
        protected FiniteStateMachine.PushState mPushDelegate;

        /// <summary>
        /// The delegate of poping state.
        /// </summary>
        protected FiniteStateMachine.PopState mPopDelegate;

        /// <summary>
        /// The state name.
        /// </summary>
        protected string mStateName;

        /// <summary>
        /// The state object.
        /// </summary>
        protected IState mStateObj;

        /// <summary>
        /// The owner of state.
        /// </summary>
        protected FiniteStateMachine mOwner;

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <value>The name of the state.</value>
        public string StateName
        {
            get { return mStateName; }
        }

        /// <summary>
        /// Gets the state object.
        /// </summary>
        /// <value>The state object.</value>
        public IState StateObject
        {
            get { return mStateObj; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="stateObj">The state object.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="enterDelegate">The enter delegate.</param>
        /// <param name="pushDelegate">The push delegate.</param>
        /// <param name="popDelegate">The pop delegate.</param>
        public State(string stateName, IState stateObj, FiniteStateMachine owner,
            FiniteStateMachine.EnterState enterDelegate,
            FiniteStateMachine.PushState pushDelegate,
            FiniteStateMachine.PopState popDelegate)
        {
            mStateName = stateName;
            mStateObj = stateObj;
            mEnterDelegate = enterDelegate;
            mPushDelegate = pushDelegate;
            mPopDelegate = popDelegate;
        }
    }
}