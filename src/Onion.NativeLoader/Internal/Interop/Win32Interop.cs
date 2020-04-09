using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace Onion.NativeLoader.Internal.Interop
{
    internal class Win32Interop
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        [SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "<Pending>")]
        public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string path);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        [SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "<Pending>")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
}
