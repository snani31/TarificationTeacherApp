using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp.Commands
{
    public class UploadNewTuskCommand : CommandBase
    {
        private readonly AddNewTuskViewModel _viewModelMember;
        private readonly Action _makeTuskUpload;
        public UploadNewTuskCommand(AddNewTuskViewModel viewModelMember, Action makeTuskUpload)
        {
            _viewModelMember = viewModelMember;
            _viewModelMember.PropertyChanged += OnViewModelPropertyChange;
            _makeTuskUpload = makeTuskUpload;
        }

        public override void Execute(object parameter)
        {
            _makeTuskUpload?.Invoke();
        }

        public override bool CanExecute(object parameter)
        {
            return (!string.IsNullOrEmpty(_viewModelMember.TuskEssence))
                && (!string.IsNullOrEmpty(_viewModelMember.TuskSelectedType))
                && (!string.IsNullOrEmpty(_viewModelMember.TuskTytle))
                && (!string.IsNullOrEmpty(_viewModelMember.TuskSelectedTime)
                && (_viewModelMember.SelectedDiscipline != null)
                && (_viewModelMember.SelectedDate) != default)
                && base.CanExecute(parameter);
        }
        private void OnViewModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddNewTuskViewModel.SelectedDiscipline) 
                || e.PropertyName == nameof(AddNewTuskViewModel.TuskEssence)
                || e.PropertyName == nameof(AddNewTuskViewModel.TuskSelectedType)
                || e.PropertyName == nameof(AddNewTuskViewModel.TuskTytle)
                || e.PropertyName == nameof(AddNewTuskViewModel.TuskSelectedTime)
                || e.PropertyName == nameof(AddNewTuskViewModel.SelectedDate))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
