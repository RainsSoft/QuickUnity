using QuickUnity.Events;
using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// Finite State Machine hold all states, and control them.
    /// </summary>
    public class FiniteStateMachine : EventDispatcher, IFiniteStateMachine
    {
        /// <summary>
        /// The state dictionary.
        /// </summary>
        private Dictionary<string, IFSMState> mStateDic;

        /// <summary>
        /// The name of finite state machine.
        /// </summary>
        private string mName;

        /// <summary>
        /// The current state.
        /// </summary>
        private IFSMState mCurrentState;

        /// <summary>
        /// Gets the name of finite state machine.
        /// </summary>
        /// <value>
        /// The name of finite state machine.
        /// </value>
        public string name
        {
            get { return mName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiniteStateMachine" /> class.
        /// </summary>
        /// <param name="name">The name of finite state machine.</param>
        public FiniteStateMachine(string name = null)
            : base()
        {
            mName = name;
            mStateDic = new Dictionary<string, IFSMState>();

            if (FSMManager.instance != null)
                FSMManager.instance.AddFSM(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FiniteStateMachine"/> class.
        /// </summary>
        ~FiniteStateMachine()
        {
            if (mStateDic != null)
            {
                mStateDic.Clear();
                mStateDic = null;
            }
        }

        #region API

        /// <summary>
        /// Enters the state.
        /// </summary>
        /// <param name="state">The state object.</param>
        public void EnterState(IFSMState state)
        {
            if (state == null)
                return;

            IFSMState previousState = mCurrentState;
            mCurrentState = state;

            if (previousState != null)
                previousState.OnExit();

            if (mCurrentState != null)
                mCurrentState.OnEnter();

            FSMEvent fsmEvent = new FSMEvent(FSMEvent.STATE_TRANSITION);
            fsmEvent.previousState = previousState;
            fsmEvent.currentState = mCurrentState;
            DispatchEvent(fsmEvent);
        }

        /// <summary>
        /// This function is called every fixed framerate frame.
        /// </summary>
        public void FixedUpdate()
        {
            if (mCurrentState != null)
                mCurrentState.OnFixedUpdate();
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        public void Update()
        {
            if (mCurrentState != null)
                mCurrentState.OnUpdate();
        }

        /// <summary>
        /// LateUpdate is called every frame.
        /// </summary>
        public void LateUpdate()
        {
            if (mCurrentState != null)
                mCurrentState.OnLateUpdate();
        }

        /// <summary>
        /// Adds the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void AddState(IFSMState state)
        {
            if (state == null)
                return;

            if (!mStateDic.ContainsKey(state.stateName))
                mStateDic.Add(state.stateName, state);
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public void RemoveState(string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
                return;

            if (mStateDic.ContainsKey(stateName))
                mStateDic.Remove(stateName);
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void RemoveState(IFSMState state)
        {
            if (state == null)
                return;

            string stateName = state.stateName;
            RemoveState(stateName);
        }

        #endregion API
    }
}