using cRGB.Modules.Common.Base;

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
