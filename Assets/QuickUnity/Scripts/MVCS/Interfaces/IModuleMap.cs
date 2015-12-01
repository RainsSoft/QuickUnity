using System;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// The MVCS ModuleMap contract.
    /// </summary>
    public interface IModuleMap
    {
        /// <summary>
        /// Registers the module.
        /// </summary>
        /// <param name="module">The module.</param>
        void RegisterModule(IModule module);

        /// <summary>
        /// Retrieves the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns>The module object.</returns>
        IModule RetrieveModule(Type moduleType);

        /// <summary>
        /// Removes the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        void RemoveModule(Type moduleType);
    }
}