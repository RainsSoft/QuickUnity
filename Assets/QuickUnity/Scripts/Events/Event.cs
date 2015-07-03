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
        /// Initializes a new sInstance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Event(string type)
        {
            mType = type;
        }
    }
}