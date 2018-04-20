using System;
using System.Runtime.InteropServices;


namespace UiTesting.Source
{
    [Obsolete("Native methods should be avoided at all times")]
    internal static class NativeMethods
    {
        [Obsolete]
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadImage(IntPtr instance, string filename, uint type, int width, int height, uint load);

        [Obsolete]
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestoryCursor(IntPtr cursor);

        [Obsolete]
        internal static IntPtr LoadCursor(string filename)
        {
            return LoadImage(IntPtr.Zero, filename, 2, 0, 0, 0x0010);
        }

        [Obsolete]
        [DllImport("User32.dll")]
        internal static extern short GetKeyState(int key);
    }
}
