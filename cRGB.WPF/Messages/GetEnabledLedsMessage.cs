using cRGB.WPF.ViewModels.Navigation;

namespace cRGB.WPF.Messages
{
    public class GetEnabledLedsMessage : IMessage
    {
        public int EnabledLedsCount { get; set; }
        public int EnabledLedsHighestIndex { get; set; }
    }
}
