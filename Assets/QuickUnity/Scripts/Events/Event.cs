namespace QuickUnity.Events
{
    /// <summary>
    /// Class Event.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The type of event.
        /// </summary>
        protected string mType;

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string eventType
        {
            get { return mType; }
        }

        /// <summary>
        /// The target object of event.
        /// </summary>
        protected object mTarget;

        /// <summary>
        /// Gets the target object of event.
        /// </summary>
        /// <value>The target of event.</value>
        public object target
        {
            get { return mTarget; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="type">The type of event. </param>
        /// <param name="target">The target object of event. </param>
        public Event(string type, object target = null)
        {
            mType = type;
            mTarget = target;
        }
    }
}