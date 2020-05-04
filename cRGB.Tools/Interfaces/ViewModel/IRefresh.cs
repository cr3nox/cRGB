namespace cRGB.Tools.Interfaces.ViewModel
{
    /// <summary>
    /// Used for performance intensive Properties to bypass OnPropertyChanged.
    /// </summary>
    public interface IRefresh
    {
        public void RefreshProperties();
    }
}
