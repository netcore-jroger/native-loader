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
        private static readonly IDictionary<string, Delegate> _delegateCache = new ConcurrentDictionary<string, Delegate>();
        private readonly string _filePath;
        private bool disposed = false;
        private HandleRef _moduleHandle;

        public NativeLoader(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException($"{nameof(filePath)} can not empty string.");
            if (!File.Exists(filePath)) throw new FileNotFoundException($"Native library not found in path: {filePath}.");

            this._filePath = filePath;
            this._moduleHandle = this.Initial();
        }

        public virtual TDelegate LoadFunction<TDelegate>(string functionName) where TDelegate : Delegate
        {
            if (_delegateCache.TryGetValue(functionName, out var @delegate))
            {
                return @delegate as TDelegate;
            }

            var functionAddress = Win32Interop.GetProcAddress(this._moduleHandle.Handle, functionName);
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
            if (this.disposed) return;

            if (disposing)
            {
                this.Free();
            }

            this.disposed = true;
        }

        private HandleRef Initial()
        {
            var handle = Win32Interop.LoadLibrary(this._filePath);

            if (handle == IntPtr.Zero)
            {
                throw new ArgumentException($"failed to load {this._filePath}");
            }

            return new HandleRef(this, handle);
        }

        private void Free()
        {
            if (this._moduleHandle.Handle == IntPtr.Zero) return;

            Win32Interop.FreeLibrary(this._moduleHandle.Handle);

            this._moduleHandle = new HandleRef(null, IntPtr.Zero);
        }
    }
}
