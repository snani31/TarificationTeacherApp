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
    public class     AddNewTuskViewModel : ViewModelBase
    {
        private readonly Student _selectedStudent;
        private readonly NavigationStore _navigationStore;
        private readonly SessionDataStore _sessionDataStore;
        private readonly ViewModelBase _pastForm;
        private readonly Action UpdateTuskList;
        public ICommand BackTusksForm { get; }
        public  ICommand MakeUploadNewTuskCommand { get; }

        public string StydentFIO { get => $"{_selectedStudent.surName} {_selectedStudent.name} {_selectedStudent.middleName}"; }
        public string DisciplineTytle 
        { 
            get => SelectedDiscipline?.DisciplineName ?? string.Empty; 
        }

        private string _tuskSelectedType = string.Empty;
        public string TuskSelectedType
        {
            get { return _tuskSelectedType; }
            set 
            { 
                _tuskSelectedType = value;
                OnPropertyChenged(nameof(TuskSelectedType));
            }
        }

        private string _tuskSelectedTime = string.Empty;
        public string TuskSelectedTime
        {
            get { return _tuskSelectedTime; }
            set 
            { 
                _tuskSelectedTime = value;
                OnPropertyChenged(nameof(TuskSelectedTime));
            }
        }

        private string _tuskTytle = string.Empty;
        public string TuskTytle
        {
            get { return _tuskTytle; }
            set { 
                _tuskTytle = value;
                OnPropertyChenged(nameof(TuskTytle));
            }
        }

        private string _tuskEssence = string.Empty;
        public string TuskEssence
        {
            get { return _tuskEssence; }
            set { 
                _tuskEssence = value;
                OnPropertyChenged(nameof(TuskEssence));
            }
        }

        private DateTime _selectedDate = default;
        public DateTime SelectedDate
        { 
            get => _selectedDate; 

            set
            {
                _selectedDate = value;
                OnPropertyChenged(nameof(SelectedDate));
            }  
        }


        private ObservableCollection<string> _selectTimeItems;
        public ObservableCollection<string> SelectTimeItems
        {
            get { return _selectTimeItems; }
        }

        private ObservableCollection<string> _tuskTypeItems;
        public ObservableCollection<string> TuskTypeItems
        {
            get { return _tuskTypeItems; }
        }

        private ObservableCollection<DisciplineListRecordViewModel> _disciplineItems;
        public IEnumerable<DisciplineListRecordViewModel> DisciplineItems
        {
            get
            {
                return _disciplineItems;
            }
        }

        private DisciplineListRecordViewModel _selectedDiscipline = null;
        public DisciplineListRecordViewModel SelectedDiscipline 
        {
            get => _selectedDiscipline; 
            set
            {
                _selectedDiscipline = value;
                OnPropertyChenged(nameof(SelectedDiscipline));
                OnPropertyChenged(nameof(DisciplineTytle));
            } 
        }
        public AddNewTuskViewModel(int selectedStudentPrimaryKey, NavigationStore navigationStore, SessionDataStore sessionDataStore, ViewModelBase pastForm,Action UpdateTuskList)
        {
            MakeUploadNewTuskCommand = new UploadNewTuskCommand(this, MakeUploadNewTusk);
            this.UpdateTuskList = UpdateTuskList;
            _navigationStore = navigationStore;
            _sessionDataStore = sessionDataStore;
            _selectedStudent = sessionDataStore.SessionAuthorisedUser.GetStudentData(selectedStudentPrimaryKey);
            _pastForm = pastForm;
            BackTusksForm = new NavigationCommand(_navigationStore, () => _pastForm);

            _disciplineItems = new ObservableCollection<DisciplineListRecordViewModel>();
            _tuskTypeItems = new ObservableCollection<string>();
            _selectTimeItems = new ObservableCollection<string>();

            _tuskTypeItems = JsonDataWork.LoadListFromJsonFile<string>(@"Json/TuskTypeCollection.Json") as ObservableCollection<string>;
            _selectTimeItems = JsonDataWork.LoadListFromJsonFile<string>(@"Json/TimesCollection.Json") as ObservableCollection<string>;
            LoadDisciplineList();
            
        }

        private void LoadDisciplineList()
        {
            foreach (Discipline Record in _sessionDataStore.SessionAuthorisedUser.GetDisciplineList())
            {
                _disciplineItems.Add(new DisciplineListRecordViewModel(Record));
            }
        }
        /// <summary>
        /// Метод для осуществления загрузки нового задания
        /// </summary>
        private void MakeUploadNewTusk()
        {
            try
            {
                string DateStr = $"{SelectedDate.ToString("yyyy-MM-dd")} {TuskSelectedTime}:00";
                DateTime assignmentDate = DateTime.Parse(DateStr);
                Tusk newTusk = new Tusk(TuskTytle, TuskEssence, TuskSelectedType, new Discipline(SelectedDiscipline.DisciplinePrimaryKey), assignmentDate, _selectedStudent);
                _sessionDataStore.SessionAuthorisedUser.AddNewTusk(newTusk);
            }
            catch
            {
                MessageBox.Show("Ошибка, входные данные не соответствуют формату");
            }
            MessageBox.Show("Операция по добавлению задания прошла успешно");
            _navigationStore.CurrentViewModel = _pastForm;
            UpdateTuskList?.Invoke();
        }
    }
}     
