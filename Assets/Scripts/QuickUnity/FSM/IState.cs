/// <summary>
/// The FSM namespace.
/// </summary>
namespace QuickUnity.FSM
{
    /// <summary>
    /// Interface IState
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when enter this state.
        /// </summary>
        /// <param name="prevState">State of the previous.</param>
        void OnEnter(string prevState);

        /// <summary>
        /// Called when exit this state.
        /// </summary>
        /// <param name="nextState">State of the next.</param>
        void OnExit(string nextState);

        /// <summary>
        /// Called when update this state.
        /// </summary>
        void OnUpdate();
    }
}