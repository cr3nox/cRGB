//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using cRGB.WPF.Annotations;

//namespace cRGB.WPF.Helpers
//{
//    public class BindableDataSource : INotifyPropertyChanged
//    {
//        readonly Task _task;

//        public BindableDataSource(Task task)
//        {
//            _task = task;
            
//        }

//        public Exception Exception => _task.Exception;

//        public object AsyncState => _task.AsyncState;

//        public TaskCreationOptions CreationOptions => _task.CreationOptions;

//        public int Id => _task.Id;

//        public bool IsCanceled => _task.IsCanceled;

//        public bool IsCompleted => _task.IsCompleted;

//        public bool IsCompletedSuccessfully => _task.IsCompletedSuccessfully;

//        public bool IsFaulted => _task.IsFaulted;

//        public TaskStatus Status => _task.Status;

//        public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
//        {
//            return _task.ConfigureAwait(continueOnCapturedContext);
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        [NotifyPropertyChangedInvocator]
//        // ReSharper disable once UnusedMember.Global
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
