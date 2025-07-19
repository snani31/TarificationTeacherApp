using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerificationTeacherApp.Stores;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _createViewModel;

        public NavigationCommand(NavigationStore navigationStore,Func<ViewModelBase> CreateViewModel)
        {
            _navigationStore = navigationStore;
            this._createViewModel = CreateViewModel;
        }

        public override void Execute(object parameter)
        {
            ViewModelBase localViewModel = _createViewModel();
            if (_navigationStore.CurrentViewModel.GetType() != localViewModel.GetType())
            {
                _navigationStore.CurrentViewModel = localViewModel;
            }
        }
    }
}
