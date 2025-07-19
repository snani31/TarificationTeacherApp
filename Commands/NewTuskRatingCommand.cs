using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp.Commands
{
    public class NewTuskRatingCommand : CommandBase
    {
        private readonly SelectedUnratedTuskDataViewModel _viewModelMember;
        private readonly Action _makeRating;
        public NewTuskRatingCommand(SelectedUnratedTuskDataViewModel viewModelMember, Action makeRating)
        {
            _viewModelMember = viewModelMember;
            _viewModelMember.PropertyChanged += OnViewModelPropertyChange;
            _makeRating = makeRating;
        }

        public override void Execute(object parameter)
        {
            _makeRating?.Invoke();
        }

        public override bool CanExecute(object parameter)
        {
            return (!string.IsNullOrEmpty(_viewModelMember.GradeFootnote) && (_viewModelMember.SelectedGrade != null)) && base.CanExecute(parameter);
        }
        private void OnViewModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedUnratedTuskDataViewModel.GradeFootnote) || e.PropertyName == nameof(SelectedUnratedTuskDataViewModel.SelectedGrade))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
