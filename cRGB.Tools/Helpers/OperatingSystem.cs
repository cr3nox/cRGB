#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Runtime.InteropServices;

namespace cRGB.Tools.Helpers
{
    public static class OS
    {
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}