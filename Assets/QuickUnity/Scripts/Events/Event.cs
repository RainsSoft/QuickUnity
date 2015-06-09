/// <summary>
/// The Events namespace.
/// </summary>
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
        /// The data of event.
        /// </summary>
        protected object mData;

        /// <summary>
        /// Gets the data of event.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        public Event(string type, object data = null)
        {
            mType = type;
            mData = data;
        }
    }
}