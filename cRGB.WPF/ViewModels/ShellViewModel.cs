using System;
using System.Collections.Generic;
using System.Text;

namespace cRGB.WPF.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello => !string.IsNullOrWhiteSpace(Name);

        public void SayHello()
        {

        }
    }
}
