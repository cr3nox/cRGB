using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Caliburn.Micro;

namespace cRGB.WPF.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region Properties


        #endregion Properties

        int count = 1;

        public void OpenTab()
        {
            ActivateItemAsync(new BlinkStickViewModel
            {
                DisplayName = "Tab " + count++
            }, new CancellationToken());
        }
    }
}
