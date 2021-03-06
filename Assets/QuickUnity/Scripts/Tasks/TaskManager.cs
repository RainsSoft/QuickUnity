﻿using QuickUnity.Patterns;
using System.Collections.Generic;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// Manage all task objects.
    /// </summary>
    public class TaskManager : SingletonMonoBehaviour<TaskManager>
    {
        /// <summary>
        /// The task dictionary.
        /// </summary>
        private Dictionary<string, ITask> mTasks;

        /// <summary>
        /// Awake this script.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // Initialize task list.
            mTasks = new Dictionary<string, ITask>();
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove all tasks.
            RemoveAllTasks();
            mTasks = null;
        }

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="task">The task object.</param>
        public void AddTask(string taskName, ITask task)
        {
            if (string.IsNullOrEmpty(taskName) || task == null)
                return;

            mTasks.Add(taskName, task);
            task.AddEventListener(TaskEvent.TASK_STOP, OnTaskStop);
            StartCoroutine(task.RoutineWrapper());
        }

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>ITask.</returns>
        public ITask GetTask(string taskName)
        {
            if (!string.IsNullOrEmpty(taskName))
            {
                if (mTasks.ContainsKey(taskName))
                    return mTasks[taskName];
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>System.String.</returns>
        public string GetTaskName(ITask task)
        {
            if (task != null)
            {
                foreach (KeyValuePair<string, ITask> kvp in mTasks)
                {
                    if (kvp.Value == task)
                        return kvp.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        public void RemoveTask(string taskName)
        {
            if (!string.IsNullOrEmpty(taskName))
            {
                ITask task = GetTask(taskName);

                if (task != null)
                {
                    task.RemoveEventListenersByTarget(this);
                    StopCoroutine(task.RoutineWrapper());
                    mTasks.Remove(taskName);
                }
            }
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void RemoveTask(ITask task)
        {
            string taskName = GetTaskName(task);

            if (!string.IsNullOrEmpty(taskName))
                RemoveTask(taskName);
        }

        /// <summary>
        /// Removes all tasks.
        /// </summary>
        public void RemoveAllTasks()
        {
            foreach (KeyValuePair<string, ITask> kvp in mTasks)
            {
                ITask task = kvp.Value;

                if (task != null)
                    task.RemoveEventListenersByTarget(this);
            }

            StopAllCoroutines();
            mTasks.Clear();
        }

        /// <summary>
        /// Called when [task stop].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnTaskStop(QuickUnity.Events.Event evt)
        {
            TaskEvent taskEvent = (TaskEvent)evt;
            ITask task = taskEvent.task;

            if (taskEvent.task != null)
            {
                StopCoroutine(taskEvent.task.RoutineWrapper());

                // Remove the task automatically.
                if (task.removedWhenStop)
                    RemoveTask(task);
            }
        }
    }
}