using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// An abstract <c>IModuleMap</c> implementation.
    /// </summary>
    public class ModuleMap : DataMap<Type, IModule>, IModuleMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleMap"/> class.
        /// </summary>
        public ModuleMap()
            : base()
        {
        }

        #region Public Functions

        /// <summary>
        /// Registers the module.
        /// </summary>
        /// <param name="module">The module.</param>
        public void RegisterModule(IModule module)
        {
            Register(module.GetType(), module);
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
            return Retrieve(moduleType);
        }

        /// <summary>
        /// Removes the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public void RemoveModule(Type moduleType)
        {
            Remove(moduleType);
        }

        #endregion Public Functions
    }
}