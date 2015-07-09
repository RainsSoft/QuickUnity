using QuickUnity.Tasks;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Example.Task
{
    /// <summary>
    /// Example for showing how to use Task and TaskManager.
    /// </summary>
    public class TaskExample : MonoBehaviour
    {
        /// <summary>
        /// Does something.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator DoSomething()
        {
            while (true)
            {
                Debug.Log("I am doing things.");
                yield return null;
            }
        }

        /// <summary>
        /// Stops the task.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="task">The task.</param>
        /// <returns>IEnumerator.</returns>
        private IEnumerator StopTask(float delay, ITask task)
        {
            yield return new WaitForSeconds(delay);
            task.Pause();
        }

        /// <summary>
        /// Start.
        /// </summary>
        private void Start()
        {
            ITask taskA = new QuickUnity.Tasks.Task(DoSomething());
            TaskManager.instance.AddTask("taskA", taskA);
            taskA.Start();

            ITask taskB = new QuickUnity.Tasks.Task(StopTask(2.0f, taskA));
            TaskManager.instance.AddTask("taskB", taskB);
            taskB.Start();
        }
    }
}