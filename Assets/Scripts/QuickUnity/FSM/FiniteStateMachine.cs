using System.Collections.Generic;

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
        protected Dictionary<string, State> states;
    }
}