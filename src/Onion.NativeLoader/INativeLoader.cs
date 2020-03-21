using System;

namespace Onion.NativeLoader
{
    /// <summary>
    /// Native library loader.
    /// </summary>
    public interface INativeLoader : IDisposable
    {
        /// <summary>
        /// Load a native function to delegate.
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="functionName">Indicates the name of the load function.</param>
        /// <returns></returns>
        TDelegate LoadFunction<TDelegate>(string functionName) where TDelegate : Delegate;
    }
}
