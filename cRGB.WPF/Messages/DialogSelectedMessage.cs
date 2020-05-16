using cRGB.Modules.Common.Base;
using cRGB.WPF.ViewModels;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.Messages
{
    public class DialogSelectedMessage : IMessage
    {
        public object Tag { get; set; }

        public IViewModelBase SelectedViewModel { get; set; }

        public DialogSelectedMessage(IViewModelBase viewModel) => SelectedViewModel = viewModel;

        public DialogSelectedMessage()
        {

        }
    }
}
