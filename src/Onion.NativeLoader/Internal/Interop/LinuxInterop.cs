using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal.Interop
{
    internal static class LinuxInterop
    {
        [DllImport("libdl.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libdl.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlerror();

        [DllImport("libdl.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        
        [DllImport("libdl.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int dlclose(IntPtr handle);
    }
}
