#region File Info
// Author: Andreas Hofmann, 05 2020
#endregion

using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using cRGB.WPF.Messages;
using cRGB.WPF.ViewModels.Controls;

namespace cRGB.WPF.ViewModels.Event
{
    public class EventListViewModel : ViewModelBase, IEventListViewModel, IHandle<DialogSelectedMessage>
    {
        #region Fields

        IEventAggregator _eventAggregator;


        #endregion

        #region Properties

        public BindableCollection<IEventViewModel> Events { get; set; }= new BindableCollection<IEventViewModel>();

        public IEventViewModel SelectedEvent { get; set; }

        public DialogComboBoxSelectionViewModel DialogComboBoxSelectionViewModel { get; }

        public bool IsEventSelectionOpen { get; set; }

        #endregion

        #region Constructor
        public EventListViewModel(IEventAggregator aggregator, DialogComboBoxSelectionViewModel eventDialogComboBoxSelectionViewModel)
        {
            _eventAggregator = aggregator;
            DialogComboBoxSelectionViewModel = eventDialogComboBoxSelectionViewModel;
        }

        #endregion

        #region Methods

        public void AddEvent()
        {
            IsEventSelectionOpen = true;
        }

        public Task HandleAsync(DialogSelectedMessage message, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion


    }
}