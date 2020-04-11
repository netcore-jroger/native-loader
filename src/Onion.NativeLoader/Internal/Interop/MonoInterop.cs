using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal.Interop
{
    internal static class MonoInterop
    {
        [DllImport("__Internal")]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("__Internal")]
        internal static extern IntPtr dlerror();

        [DllImport("__Internal")]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);
        
        [DllImport("__Internal")]
        internal static extern int dlclose(IntPtr handle);
    }
}
