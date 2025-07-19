using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Xml.Linq;
using TerificationTeacherApp.Stores;

namespace TerificationTeacherApp.ViewModel
{
    public class StudentsTusksDataViewModel : ViewModelBase
    {
        private readonly StudentsTuskRecordViewModel _addNewTuskCard = null;
        private readonly SessionDataStore _sessionDataStore = null;
        private readonly NavigationStore _navigationStore = null;
        private ComboboxItemViewModel _selectedGroupFilter = null;
        public ComboboxItemViewModel SelectedGroupFilter
        {
            get
            {
                return _selectedGroupFilter;
            }
            set
            {
                _selectedGroupFilter = value;
                LoadStudentList();
                OnPropertyChenged(nameof(SelectedGroupFilter));
            }
        }
        private StudentsTuskRecordViewModel _selectedTuskFilter = null;
        public StudentsTuskRecordViewModel SelectedTuskFilter
        {
            get
            {
                return _selectedTuskFilter;
            }
            set
            {
                _selectedTuskFilter = value;
                if (value == null) return;

                if (_selectedTuskFilter._isRated)
                {
                    _navigationStore.CurrentViewModel = CreateSelectedTuskDataPageViewModel();
                }
                else if (_selectedTuskFilter == _addNewTuskCard)
                {
                    _navigationStore.CurrentViewModel = CreateAddNewTuskDataPageViewModel();
                }
                else
                {
                    _navigationStore.CurrentViewModel = CreateSelectedUnratedTuskDataPageViewModel();
                }
                OnPropertyChenged(nameof(SelectedTuskFilter));
            }
        }
        private StudentRecordViewModel _selectedStudentFilter = null;
        public StudentRecordViewModel SelectedStudentFilter
        {
            get
            {
                return _selectedStudentFilter;
            }
            set
            {
                _selectedStudentFilter = value;
                LoadTuskList();
                OnPropertyChenged(nameof(SelectedStudentFilter));
            }
        }
        private ObservableCollection<StudentsTuskRecordViewModel> _tuskItems;
        public ObservableCollection<StudentsTuskRecordViewModel> TuskItems
        {
            get
            {
                return _tuskItems;
            }
            set
            {
                _tuskItems = value;
                OnPropertyChenged(nameof(TuskItems));
            }
        }
        private ObservableCollection<ComboboxItemViewModel> _groupItems;

        public ObservableCollection<ComboboxItemViewModel> GroupItems
        {
            get
            {
                return _groupItems;
            }
            set
            {
                _groupItems = value;
                OnPropertyChenged(nameof(GroupItems));
            }
        }
        private ObservableCollection<StudentRecordViewModel> _studentItems;

        public ObservableCollection<StudentRecordViewModel> StudentItems
        {
            get
            {
                return _studentItems;
            }
            set
            {
                _studentItems = value;
                OnPropertyChenged(nameof(StudentItems));
            }
        }
        public StudentsTusksDataViewModel(SessionDataStore sessionDataStore, NavigationStore navigationStore)
        {
            _sessionDataStore = sessionDataStore;
            TuskItems = new ObservableCollection<StudentsTuskRecordViewModel>();
            StudentItems = new ObservableCollection<StudentRecordViewModel>();
            GroupItems = new ObservableCollection<ComboboxItemViewModel>();
            _addNewTuskCard = new StudentsTuskRecordViewModel("Дать новое задание");
            LoadStudentGroupList();
            _navigationStore = navigationStore;
        }
        /// <summary>
        /// Метод производит загрузку данных коллекции групп студентов
        /// </summary>
        private void LoadStudentGroupList()
        {
            foreach (StudentGroup Record in _sessionDataStore.SessionAuthorisedUser.GetStudentGroupsList())
            {
                _groupItems.Add(new ComboboxItemViewModel(Record));
            }
        }
        /// <summary>
        /// Метод производит загрузку данных коллекции студентов выбранной группы
        /// </summary>
        private void LoadStudentList()
        {
            _studentItems.Clear();
            foreach (Student Record in _sessionDataStore.SessionAuthorisedUser.GetStudentsList(SelectedGroupFilter.GroupPrimaryKey))
            {
                _studentItems.Add(new StudentRecordViewModel(Record));
            }
        }
        /// <summary>
        /// Метод производит загрузку заданий выбранного студента
        /// </summary>
        private void LoadTuskList()
        {
            _tuskItems.Clear();
            if (SelectedStudentFilter == null) return;
            foreach (Tusk Record in _sessionDataStore.SessionAuthorisedUser.GetTusksList(SelectedStudentFilter.StudentPrymaryKey))
            {
                _tuskItems.Add(new StudentsTuskRecordViewModel(Record));
            }
            // В конце всегда будет добавлена карта, позволяющая назначить новое задание
            _tuskItems.Add(_addNewTuskCard);
        }
        /// <summary>
        /// Метод создания страницы с данными выбранного задания для перехода 
        /// </summary>
        /// <returns></returns>
        private SelectedTuskDataViewModel CreateSelectedTuskDataPageViewModel()
        {
            return new SelectedTuskDataViewModel(_sessionDataStore, _navigationStore, _selectedTuskFilter.TuskPrimaryKey, this);
        }

        /// <summary>
        /// Метод создания страницы с данными неоценённого выбранного задания для перехода 
        /// </summary>
        /// <returns></returns>
        private SelectedUnratedTuskDataViewModel CreateSelectedUnratedTuskDataPageViewModel()
        {
            return new SelectedUnratedTuskDataViewModel(_sessionDataStore, _navigationStore, _selectedTuskFilter.TuskPrimaryKey, this, LoadTuskList);
        }

        /// <summary>
        /// Метод создания страницы для назначения нового задания выбранному студенту
        /// </summary>
        /// <returns></returns>
        private AddNewTuskViewModel CreateAddNewTuskDataPageViewModel()
        {
            return new AddNewTuskViewModel(SelectedStudentFilter.StudentPrymaryKey, _navigationStore, _sessionDataStore,this, LoadTuskList);
        }
    }
}
