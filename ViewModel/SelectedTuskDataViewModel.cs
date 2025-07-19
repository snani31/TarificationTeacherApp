using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TerificationTeacherApp.Commands;
using TerificationTeacherApp.Stores;

namespace TerificationTeacherApp.ViewModel
{
    public class SelectedTuskDataViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigateStore;
        private readonly SessionDataStore _sessionDataStore;
        private readonly Tusk _selectedTusk;
        public string TuskTytle { get => _selectedTusk.tytle; }
        public string TuskStatus { get => _selectedTusk.status; }
        public string StydentFIO { get => $"{_selectedTusk.student.surName} {_selectedTusk.student.name} {_selectedTusk.student.middleName}" ; }
        public string TuskEssence { get => _selectedTusk.essence; }
        public string GradeFootnote { get => _selectedTusk.grade.footnote; }
        public string DisciplineTytle { get => _selectedTusk.discipline.tytle; }
        public string AssignmentDate { get => _selectedTusk.assignmentDate.ToString(); }
        public ICommand BackTusksForm { get; }

        private readonly Dictionary<int, string> _cardColorsBackground = new Dictionary<int, string>()
        {
             { 2, "#c72e32"},
             { 3, "#f28a03"},
             { 4, "#02fffe"},
             { 5, "#14f905"}
        };
        private readonly Dictionary<int, string> _cardColorsBrush = new Dictionary<int, string>()
        {
             { 2, "#590027"},
             { 3, "#704206"},
             { 4, "#092e92"},
             { 5, "#1a2c2d"}
        };
        private readonly Dictionary<int, string> _cardPictures = new Dictionary<int, string>()
        {
             { 2, "/Styles/Icons/Grades/Bad.png"},
             { 3, "/Styles/Icons/Grades/Satisfactorily.png"},
             { 4, "/Styles/Icons/Grades/Good.png"},
             { 5, "/Styles/Icons/Grades/Great.png"}
        };
        private readonly Dictionary<int, string> _cardGradessStr = new Dictionary<int, string>()
        {
             { 2, "Неудовлетворительно"},
             { 3, "Удовлетворительно"},
             { 4, "Хорошо"},
             { 5, "Отлично"}
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionDataStore"></param>
        /// <param name="navigateStore"></param>
        /// <param name="selectedTuskPrimaryKey">первичный ключ выбранного задания для выгрузки данных</param>
        /// <param name="pastForm">модель для обратной навигации</param>
        public SelectedTuskDataViewModel(SessionDataStore sessionDataStore, NavigationStore navigateStore,int selectedTuskPrimaryKey, ViewModelBase pastForm)
        {
            _sessionDataStore = sessionDataStore;
            _navigateStore = navigateStore;
            _selectedTusk = _sessionDataStore.SessionAuthorisedUser.GetTusksData(selectedTuskPrimaryKey);
            BackTusksForm = new NavigationCommand(_navigateStore,() => pastForm);
        }

        public string TuskCardPicture
        {
            get
            {
                return _selectedTusk.grade == null ? "/Styles/Icons/Grades/NonGraded.png" : _cardPictures[_selectedTusk.grade.grade];
            }
        }
        public string TuskCardColorBackground
        {
            get
            {
                return _selectedTusk.grade == null ? "#c24b98" : _cardColorsBackground[_selectedTusk.grade.grade];
            }
        }
        public string TuskCardColorBrush
        {
            get
            {
                return _selectedTusk.grade == null ? "#301746" : _cardColorsBrush[_selectedTusk.grade.grade];
            }
        }

        public string TuskGradeStr
        {
            get
            {
                return _cardGradessStr[_selectedTusk.grade.grade];
            }
        }
    }
}
