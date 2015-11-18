using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// A base <c>IFSMState</c> implementation.
    /// </summary>
    public abstract class FSMState : IFSMState
    {
        /// <summary>
        /// The name of state.
        /// </summary>
        protected string mStateName;

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string stateName
        {
            get { return mStateName; }
        }

        /// <summary>
        /// The action list of state.
        /// </summary>
        protected List<IFSMStateAction> mActions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMState"/> class.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public FSMState(string stateName)
        {
            mStateName = stateName;
            mActions = new List<IFSMStateAction>();
        }

        #region API

        /// <summary>
        /// Called when [enter].
        /// </summary>
        public void OnEnter()
        {
            foreach (IFSMStateAction action in mActions)
            {
                action.OnEnter();
            }
        }

        /// <summary>
        /// Called when [fixed update].
        /// </summary>
        public void OnFixedUpdate()
        {
            foreach (IFSMStateAction action in mActions)
            {
                action.OnFixedUpdate();
            }
        }

        /// <summary>
        /// Called when [update].
        /// </summary>
        public void OnUpdate()
        {
            foreach (IFSMStateAction action in mActions)
            {
                action.OnUpdate();
            }
        }

        /// <summary>
        /// Called when [late update].
        /// </summary>
        public void OnLateUpdate()
        {
            foreach (IFSMStateAction action in mActions)
            {
                action.OnLateUpdate();
            }
        }

        /// <summary>
        /// Called when [exit].
        /// </summary>
        public void OnExit()
        {
            foreach (IFSMStateAction action in mActions)
            {
                action.OnExit();
            }
        }

        #endregion API
    }
}