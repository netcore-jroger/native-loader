using System;
using System.Runtime.InteropServices;

namespace Onion.NativeLoader.Internal
{
    internal static class OSPlatformHelper
    {
        public static bool IsLinux { get; }

        public static bool IsWindows { get; }

        public static bool IsMacOSX { get; }
        
        public static bool IsNetCore { get; }
        
        public static bool IsMono { get; }
        
        public static bool Is64Bit { get; }
        
        static OSPlatformHelper()
        {
            IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            IsMacOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            IsNetCore = RuntimeInformation.FrameworkDescription.StartsWith(".NET Core", StringComparison.OrdinalIgnoreCase);
            IsMono = Type.GetType("Mono.Runtime") != null;
            Is64Bit = IntPtr.Size == 8;
        }
    }
}
