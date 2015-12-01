using QuickUnity.Events;
using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS Module contract.
    /// </summary>
    public interface IModule : IEventDispatcher
    {
        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        /// <value>
        /// The event dispatcher.
        /// </value>
        IEventDispatcher eventDispatcher
        {
            get;
        }
    }
}