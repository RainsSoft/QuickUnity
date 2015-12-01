using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// An abstract <c>IModelMap</c> implementation.
    /// </summary>
    public class ModelMap : DataMap<Type, IModel>, IModelMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelMap"/> class.
        /// </summary>
        public ModelMap()
            : base()
        {
        }

        #region Public Functions

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RegisterModel(IModel model)
        {
            Register(model.GetType(), model);
        }

        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The model object.</returns>
        public IModel RetrieveModel(Type modelType)
        {
            return Retrieve(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        public void RemoveModel(Type modelType)
        {
            Remove(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RemoveModel(IModel model)
        {
            Remove(model);
        }

        #endregion IModelMap Implementations

        #endregion Public Functions
    }
}