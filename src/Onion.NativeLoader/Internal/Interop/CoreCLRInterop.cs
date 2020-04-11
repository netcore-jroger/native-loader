using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal.Interop
{
    internal static class CoreCLRInterop
    {
        [DllImport("libcoreclr.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libcoreclr.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlerror();

        [DllImport("libcoreclr.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        
        [DllImport("libcoreclr.so", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int dlclose(IntPtr handle);
    }
}
