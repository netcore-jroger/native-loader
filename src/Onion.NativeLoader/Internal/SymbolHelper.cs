using System;
using System.Runtime.InteropServices;
using Onion.NativeLoader.Internal.Interop;

namespace Onion.NativeLoader.Internal
{
    internal static class SymbolHelper
    {
        public static IntPtr LoadSymbol(HandleRef handleRef, string symbolName)
        {
            if (OSPlatformHelper.IsWindows)
            {
                return Win32Interop.GetProcAddress(handleRef.Handle, symbolName);
            }

            if (OSPlatformHelper.IsLinux && OSPlatformHelper.IsMono)
            {
                return MonoInterop.dlsym(handleRef.Handle, symbolName);
            }

            if (OSPlatformHelper.IsLinux && OSPlatformHelper.IsNetCore)
            {
                return CoreCLRInterop.dlsym(handleRef.Handle, symbolName);
            }

            if (OSPlatformHelper.IsMacOSX)
            {
                return MacOSXInterop.dlsym(handleRef.Handle, symbolName);
            }
            
            throw new InvalidOperationException("Unsupported OS platform.");
        }
    }
}
