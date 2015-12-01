using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IContext</c> implementation.
    /// </summary>
    public class Context : Module, IContext, IModuleMap
    {
        /// <summary>
        /// The module map.
        /// </summary>
        protected IModuleMap mModuleMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
            : base()
        {
            mModuleMap = new ModuleMap();
        }

        #region Public Functions

        #region IModuleMap Implementations

        /// <summary>
        /// Registers the module.
        /// </summary>
        /// <param name="module">The module.</param>
        public void RegisterModule(IModule module)
        {
            mModuleMap.RegisterModule(module);
        }

        /// <summary>
        /// Retrieves the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns>
        /// The module object.
        /// </returns>
        public IModule RetrieveModule(Type moduleType)
        {
            return mModuleMap.RetrieveModule(moduleType);
        }

        /// <summary>
        /// Removes the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public void RemoveModule(Type moduleType)
        {
            mModuleMap.RemoveModule(moduleType);
        }

        #endregion IModuleMap Implementations

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public override void RegisterModel(IModel model)
        {
            base.RegisterModel(model);

            model.ContextEventDispatcher = this;
        }

        #endregion IModelMap Implementations

        #endregion Public Functions
    }
}