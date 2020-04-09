using System;
using System.Runtime.InteropServices;
using Onion.NativeLoader.Internal.Interop;

namespace Onion.NativeLoader.Internal
{
    internal static class UnmanagedLibraryHelper
    {
        private const int RTLD_LAZY = 1;
        private const int RTLD_GLOBAL = 8;
        
        public static HandleRef LoadLibrary(object wrapper, string filePath)
        {
            if (OSPlatformHelper.IsWindows)
            {
                return LoadWindowsLibrarySymbol(wrapper, filePath);
            }

            if (OSPlatformHelper.IsLinux && OSPlatformHelper.IsMono)
            {
                return LoadPosixLibrarySymbol(wrapper, MonoInterop.dlopen, MonoInterop.dlerror, filePath);
            }
            
            if (OSPlatformHelper.IsLinux && OSPlatformHelper.IsNetCore)
            {
                return LoadPosixLibrarySymbol(wrapper, CoreCLRInterop.dlopen, CoreCLRInterop.dlerror, filePath);
            }

            if (OSPlatformHelper.IsMacOSX)
            {
                return LoadPosixLibrarySymbol(wrapper, MacOSXInterop.dlopen, MacOSXInterop.dlerror, filePath);
            }
            
            throw new InvalidOperationException("Unsupported OS platform.");
        }

        private static HandleRef LoadWindowsLibrarySymbol(object wrapper, string filePath)
        {
            var handle = Win32Interop.LoadLibrary(filePath);

            if (handle == IntPtr.Zero)
            {
                throw new ArgumentException($"failed to load {filePath}");
            }

            return new HandleRef(wrapper, handle);
        }
        
        private static HandleRef LoadPosixLibrarySymbol(object wrapper, Func<string, int, IntPtr> dlopenFunc, Func<IntPtr> dlerrorFunc, string filePath)
        {
            var handle = dlopenFunc(filePath, RTLD_GLOBAL + RTLD_LAZY);
            
            if (handle == IntPtr.Zero)
            {
                var errorMessage = Marshal.PtrToStringAnsi(dlerrorFunc());
                throw new Exception($"failed to load {filePath}; {errorMessage}");
            }

            return new HandleRef(wrapper, handle);
        }
    }
}
