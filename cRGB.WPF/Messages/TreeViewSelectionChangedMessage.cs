using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using cRGB.WPF.ViewModels.Menu;

namespace cRGB.WPF.Messages
{
    public class TreeViewSelectionChangedMessage
    {
        public MenuItemViewModel SelectedItem { get; set; }

        public TreeViewSelectionChangedMessage(MenuItemViewModel selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
