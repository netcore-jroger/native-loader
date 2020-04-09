using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal.Interop
{
    internal static class LinuxInterop
    {
        [DllImport("libdl.so")]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libdl.so")]
        internal static extern IntPtr dlerror();

        [DllImport("libdl.so")]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
    }
}
