using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public event Action CurrentViewModelChanged;

        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel= value;
                OnCurrentViewModelChanged();
            }
        }
        /// <summary>
        /// Возникает, чтобы уведомить модель о том, что представление данных изменилось 
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
