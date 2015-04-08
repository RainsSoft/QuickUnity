using System.Collections.Generic;

/// <summary>
/// The FSM namespace.
/// </summary>
namespace QuickUnity.FSM
{
    /// <summary>
    /// Class FiniteStateMachine.
    /// </summary>
    public class FiniteStateMachine
    {
        /// <summary>
        /// Delegate EnterState
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public delegate void EnterState(string stateName);

        /// <summary>
        /// Delegate PushState
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="lastStateName">Last name of the state.</param>
        public delegate void PushState(string stateName, string lastStateName);

        /// <summary>
        /// Delegate PopState
        /// </summary>
        public delegate void PopState();

        /// <summary>
        /// The states dictionary.
        /// </summary>
        protected Dictionary<string, State> mStates;

        /// <summary>
        /// The state stack.
        /// </summary>
        protected Stack<State> mStateStack;

        /// <summary>
        /// The entry point.
        /// </summary>
        protected string mEntryPoint;

        /// <summary>
        /// Gets the current state.
        /// </summary>
        /// <value>The state of the current.</value>
        public State CurrentState
        {
            get
            {
                if (mStateStack.Count == 0)
                    return null;

                return mStateStack.Peek();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiniteStateMachine"/> class.
        /// </summary>
        public FiniteStateMachine()
        {
            mStates = new Dictionary<string, State>();
            mStateStack = new Stack<State>();
            mEntryPoint = null;
        }

        /// <summary>
        /// State machine updates.
        /// </summary>
        public void Update()
        {
        }

        /// <summary>
        /// Registers the specified state with state name.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="state">The state.</param>
        public void Register(string stateName, IState stateObj)
        {
            //mStates.Add(stateName, new State(stateName, stateObj, this, Enter, Push, Pop));
        }

        /// <summary>
        /// Enters the specified state name.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public void Enter(string stateName)
        {
        }

        /// <summary>
        /// Pushes the specified new state.
        /// </summary>
        /// <param name="newState">The new state.</param>
        public void Push(string newState)
        {
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        public void Pop()
        {
        }
    }
}