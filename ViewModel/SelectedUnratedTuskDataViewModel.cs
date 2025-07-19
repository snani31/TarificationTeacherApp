using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TerificationTeacherApp.Commands;
using TerificationTeacherApp.Stores;

namespace TerificationTeacherApp.ViewModel
{
    public class SelectedUnratedTuskDataViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigateStore;
        private readonly Action UpdateTuskList;
        private readonly SessionDataStore _sessionDataStore;
        private readonly ViewModelBase _pastPage;
        private readonly Tusk _selectedTusk;
        private readonly int _selectedTuskPrimaryKey;
        public string TuskTytle { get => _selectedTusk.tytle; }
        public string TuskStatus { get => _selectedTusk.status; }
        public string StydentFIO { get => $"{_selectedTusk.student.surName} {_selectedTusk.student.name} {_selectedTusk.student.middleName}"; }
        public string TuskEssence { get => _selectedTusk.essence; }
        public string DisciplineTytle { get => _selectedTusk.discipline.tytle; }
        public string AssignmentDate { get => _selectedTusk.assignmentDate.ToString(); }
        public ICommand BackTusksForm { get; }
        public ICommand MakeNewRatingForTusk { get; }

        private string _gradeFootnote;
        public string GradeFootnote
        {
            get
            {
                return _gradeFootnote ?? string.Empty;
            }
            set
            {
                _gradeFootnote = value;
                OnPropertyChenged(nameof(GradeFootnote));
            }
        }
        private GradeRecordViewModel _selectedGrade;
        public GradeRecordViewModel SelectedGrade
        {
            get
            {
                return _selectedGrade;
            }
            set
            {
                _selectedGrade = value;
                OnPropertyChenged(nameof(SelectedGrade));
            }
        }
        public ObservableCollection<GradeRecordViewModel> GradeList { get; } = new ObservableCollection<GradeRecordViewModel>();
        public SelectedUnratedTuskDataViewModel(SessionDataStore sessionDataStore, NavigationStore navigateStore, int selectedTuskPrimaryKey, ViewModelBase pastPage, Action UpdateTuskList)
        {
            _sessionDataStore = sessionDataStore;
            _navigateStore = navigateStore;
            _selectedTuskPrimaryKey = selectedTuskPrimaryKey;
            _pastPage = pastPage;
            this.UpdateTuskList = UpdateTuskList;
            _selectedTusk = _sessionDataStore.SessionAuthorisedUser.GetUnratedTuskData(_selectedTuskPrimaryKey);
            BackTusksForm = new NavigationCommand(_navigateStore, () => _pastPage);
            MakeNewRatingForTusk = new NewTuskRatingCommand(this, MakeRating);
            GradeList.Add(new GradeRecordViewModel("Неудовлетворительно", "/Styles/Icons/Grades/Bad.png", "#c72e32", "#590027", 2,"Сделано в срок"));
            GradeList.Add(new GradeRecordViewModel("Не сделано в срок", "/Styles/Icons/Grades/TimeOutGrade-100.png", "#f60001", "#a90000", 2, "Не сделано в срок"));
            GradeList.Add(new GradeRecordViewModel("Удовлетворительно", "/Styles/Icons/Grades/Satisfactorily.png", "#f28a03", "#704206", 3, "Сделано в срок"));
            GradeList.Add(new GradeRecordViewModel("Хорошо", "/Styles/Icons/Grades/Good.png", "#02fffe", "#092e92", 4, "Сделано в срок"));
            GradeList.Add(new GradeRecordViewModel("Отлично", "/Styles/Icons/Grades/Great.png", "#14f905", "#1a2c2d", 5, "Сделано в срок"));
        }
        private void MakeRating()
        {
            if (_sessionDataStore.SessionAuthorisedUser.RateTusk(_selectedTuskPrimaryKey, new Grade(SelectedGrade.GradeNum, GradeFootnote),SelectedGrade.UpdatedTuskStatus) )
            {
                MessageBox.Show("Оценка успешно выставлена");
                _navigateStore.CurrentViewModel= _pastPage;
                UpdateTuskList();
            }
        }
    }
}
