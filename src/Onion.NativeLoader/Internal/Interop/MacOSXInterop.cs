using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal.Interop
{
    internal static class MacOSXInterop
    {
        [DllImport("libSystem.dylib", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libSystem.dylib", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlerror();

        [DllImport("libSystem.dylib", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        
        [DllImport("libSystem.dylib", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int dlclose(IntPtr handle);
    }
}
