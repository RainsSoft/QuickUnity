using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS ModelMap contract.
    /// </summary>
    public interface IModelMap
    {
        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        void RegisterModel(IModel model);

        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The model object.</returns>
        IModel RetrieveModel(Type modelType);

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        void RemoveModel(Type modelType);

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        void RemoveModel(IModel model);
    }
}