using cRGB.WPF.ViewModels;
using cRGB.WPF.ViewModels.Event;

namespace cRGB.WPF.Messages
{
    public class DialogSelectedMessage
    {
        public IViewModelBase SelectedViewModel { get; set; }

        public DialogSelectedMessage(IViewModelBase viewModel) => SelectedViewModel = viewModel;

        public DialogSelectedMessage()
        {

        }
    }
}
