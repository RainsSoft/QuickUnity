namespace QuickUnity.FSM
{
    /// <summary>
    /// A base <c>IFSMStateAction</c> implementation.
    /// </summary>
    public abstract class FSMStateAction : IFSMStateAction
    {
        /// <summary>
        /// Whether this <see cref="IFSMStateAction"/> is enabled.
        /// </summary>
        protected bool mEnabled = true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IFSMStateAction" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool enabled
        {
            get { return mEnabled; }
            set { mEnabled = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMStateAction"/> class.
        /// </summary>
        public FSMStateAction()
        {
        }

        #region API

        /// <summary>
        /// Called when [enter].
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// Called when [fixed update].
        /// </summary>
        public virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        /// Called when [update].
        /// </summary>
        public virtual void OnUpdate()
        {
        }

        /// <summary>
        /// Called when [late update].
        /// </summary>
        public virtual void OnLateUpdate()
        {
        }

        /// <summary>
        /// Called when [exit].
        /// </summary>
        public virtual void OnExit()
        {
        }

        #endregion API
    }
}