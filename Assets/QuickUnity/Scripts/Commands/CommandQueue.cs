using QuickUnity.Events;
using System.Collections.Generic;

namespace QuickUnity.Commands
{
    /// <summary>
    /// A command queue implement.
    /// </summary>
    public class CommandQueue : EventDispatcher
    {
        /// <summary>
        /// The queue object.
        /// </summary>
        protected Queue<ICommand> mQueue;

        /// <summary>
        /// The total commands count.
        /// </summary>
        protected int mCommandsCount = 0;

        /// <summary>
        /// The executed commands count.
        /// </summary>
        protected int mExecutedCommandsCount = 0;

        /// <summary>
        /// The progress of commands executing.
        /// </summary>
        protected float mProgress;

        /// <summary>
        /// Gets the progress of commands executing.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public float progress
        {
            get { return mProgress; }
        }

        /// <summary>
        /// if set to <c>true</c> [stop when error].
        /// </summary>
        protected bool mStopWhenError = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueue" /> class.
        /// </summary>
        /// <param name="stopWhenError">if set to <c>true</c> [stop when error].</param>
        public CommandQueue(bool stopWhenError = false)
        {
            mQueue = new Queue<ICommand>();
            mStopWhenError = stopWhenError;
        }

        #region API

        /// <summary>
        /// Start to executes the commands.
        /// </summary>
        public void Start()
        {
            DispatchEvent(new CommandQueueEvent(CommandQueueEvent.START));
            ExecuteCommand();
        }

        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void AddCommand(ICommand command)
        {
            if (command != null && mQueue != null)
            {
                mQueue.Enqueue(command);
                mCommandsCount++;
            }
        }

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Executes the command that is dequeued from queue.
        /// </summary>
        protected void ExecuteCommand()
        {
            if (mQueue == null)
                return;

            if (mQueue.Count > 0)
            {
                ICommand command = mQueue.Dequeue();

                if (command != null)
                {
                    command.AddEventListener(CommandEvent.ERROR, OnCommandError);
                    command.AddEventListener(CommandEvent.EXECUTED, OnCommandExecuted);
                    command.Execute();
                }
            }
            else
            {
                DispatchEvent(new CommandQueueEvent(CommandQueueEvent.COMPLETE));
                Reset();
            }
        }

        /// <summary>
        /// Execute next command.
        /// </summary>
        protected void Next()
        {
            mExecutedCommandsCount++;

            // Dispatch progress event.
            CommandQueueEvent evt = new CommandQueueEvent(CommandQueueEvent.PROGRESS);
            evt.progress = (float)mExecutedCommandsCount / mCommandsCount;
            DispatchEvent(evt);

            // Execute the next command.
            ExecuteCommand();
        }

        /// <summary>
        /// Resets.
        /// </summary>
        protected void Reset()
        {
            mCommandsCount = 0;
            mExecutedCommandsCount = 0;
        }

        #endregion Protected Functions

        #region Private Functions

        /// <summary>
        /// Called when [command got error].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnCommandError(QuickUnity.Events.Event evt)
        {
            RemoveCommandEventListeners(evt);

            if (!mStopWhenError)
                Next();
            else
                DispatchEvent(new CommandQueueEvent(CommandQueueEvent.INTERRUPT));
        }

        /// <summary>
        /// Called when [command has executed].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnCommandExecuted(QuickUnity.Events.Event evt)
        {
            RemoveCommandEventListeners(evt);

            Next();
        }

        /// <summary>
        /// Removes the command event listeners.
        /// </summary>
        /// <param name="evt">The event object.</param>
        private void RemoveCommandEventListeners(QuickUnity.Events.Event evt)
        {
            CommandEvent commandEvent = (CommandEvent)evt;
            ICommand command = (ICommand)commandEvent.target;

            if (command != null)
                command.RemoveEventListenersByTarget(this);
        }

        #endregion Private Functions
    }
}