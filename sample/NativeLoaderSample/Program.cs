using System;
using System.Runtime.InteropServices;
using Onion.NativeLoader;

namespace NativeLoaderSample
{
    public class Program
    {
        public delegate int MessageBoxW(int hWnd, [MarshalAs(UnmanagedType.LPWStr)]string text, [MarshalAs(UnmanagedType.LPWStr)]string caption, uint type);

        static void Main(string[] args)
        {
            using (var nativeLoader = new NativeLoader(@"C:\Windows\System32\user32.dll"))
            {
                MessageBoxW messageBox = nativeLoader.LoadFunction<MessageBoxW>(nameof(MessageBoxW));
                messageBox(0, "Hello !", "Caption", 0);
            }

            Console.ReadKey(true);
        }
    }
}
