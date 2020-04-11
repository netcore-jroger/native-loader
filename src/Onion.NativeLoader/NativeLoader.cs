using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Onion.NativeLoader.Internal;

namespace Onion.NativeLoader
{
    public class NativeLoader : INativeLoader
    {
        private const string _methodNameSuffix = "_delegate";
        private static readonly IDictionary<string, Delegate> _delegateCache = new ConcurrentDictionary<string, Delegate>();
        private bool _disposed = false;
        private HandleRef _moduleHandle;

        public NativeLoader(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException($"{nameof(filePath)} can not empty string.");
            if (!File.Exists(filePath)) throw new FileNotFoundException($"Native library not found in path: {filePath}.");

            this._moduleHandle = UnmanagedLibraryHelper.LoadLibrary(this, filePath);
        }

        public virtual TDelegate LoadFunction<TDelegate>(string functionName = "") where TDelegate : Delegate
        {
            if (string.IsNullOrWhiteSpace(functionName))
            {
                functionName = typeof(TDelegate).Name;
            }

            functionName = RemoveFunctionNameSuffix(functionName);
            
            if (_delegateCache.TryGetValue(functionName, out var @delegate))
            {
                return @delegate as TDelegate;
            }

            var functionAddress = SymbolHelper.LoadSymbol(this._moduleHandle, functionName);
            if (functionAddress == IntPtr.Zero)
            {
                throw new MissingMethodException($"Native library method name '{functionName}' not found.");
            }

            @delegate = Marshal.GetDelegateForFunctionPointer(functionAddress, typeof(TDelegate));

            _delegateCache.Add(functionName, @delegate);

            return @delegate as TDelegate;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;

            if (disposing)
            {
                UnmanagedLibraryHelper.Free(this._moduleHandle);
                this._moduleHandle = new HandleRef(null, IntPtr.Zero);
            }

            this._disposed = true;
        }

        private static string RemoveFunctionNameSuffix(string functionName)
        {
            if (functionName.EndsWith(_methodNameSuffix, StringComparison.OrdinalIgnoreCase))
            {
                functionName = functionName.Substring(0, functionName.Length - _methodNameSuffix.Length);
            }

            return functionName;
        }
    }
}
