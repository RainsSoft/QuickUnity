using QuickUnity.Patterns;
using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// The FSMManager hold all finite state machines.
    /// </summary>
    public class FSMManager : SingletonMonoBehaviour<FSMManager>
    {
        /// <summary>
        /// The dictionary of finite state machine.
        /// </summary>
        private Dictionary<string, IFiniteStateMachine> mFiniteStateMachineDic;

        #region Messages

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (mFiniteStateMachineDic == null)
                mFiniteStateMachineDic = new Dictionary<string, IFiniteStateMachine>();
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            foreach (IFiniteStateMachine fsm in mFiniteStateMachineDic.Values)
            {
                fsm.FixedUpdate();
            }
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            foreach (IFiniteStateMachine fsm in mFiniteStateMachineDic.Values)
            {
                fsm.Update();
            }
        }

        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// </summary>
        private void LateUpdate()
        {
            foreach (IFiniteStateMachine fsm in mFiniteStateMachineDic.Values)
            {
                fsm.LateUpdate();
            }
        }

        #endregion Messages

        #region API

        /// <summary>
        /// Adds the FSM.
        /// </summary>
        /// <param name="finiteStateMachine">The finite state machine.</param>
        public void AddFSM(IFiniteStateMachine finiteStateMachine)
        {
            if (finiteStateMachine == null)
                return;

            string fsmName = finiteStateMachine.name;

            if (!mFiniteStateMachineDic.ContainsKey(fsmName))
                mFiniteStateMachineDic.Add(fsmName, finiteStateMachine);
        }

        /// <summary>
        /// Removes the FSM.
        /// </summary>
        /// <param name="name">The name of finite state machine.</param>
        public void RemoveFSM(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            if (mFiniteStateMachineDic.ContainsKey(name))
                mFiniteStateMachineDic.Remove(name);
        }

        /// <summary>
        /// Removes the FSM.
        /// </summary>
        /// <param name="fsm">The finite state machine.</param>
        public void RemoveFSM(IFiniteStateMachine fsm)
        {
            if (fsm == null)
                return;

            string fsmName = fsm.name;
            RemoveFSM(fsmName);
        }

        #endregion API
    }
}