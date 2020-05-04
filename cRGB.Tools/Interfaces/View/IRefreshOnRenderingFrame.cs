using System;

namespace cRGB.Tools.Interfaces.View
{
    /// <summary>
    /// Used for performance intensive Properties to bypass OnPropertyChanged.
    /// </summary>
    public interface IRefreshOnRenderingFrame
    {
        void OnRendering(object sender, EventArgs e);
    }
}
