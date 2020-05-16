using cRGB.WPF.ViewModels.Navigation;

namespace cRGB.WPF.Messages
{
    public class TreeViewSelectionChangedMessage : IMessage
    {
        public IMenuItemViewModel SelectedItem { get; set; }

        public TreeViewSelectionChangedMessage(IMenuItemViewModel selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
