using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TerificationTeacherApp.Commands;
using TerificationTeacherApp.Stores;
using static System.Net.Mime.MediaTypeNames;

namespace TerificationTeacherApp.ViewModel
{
    public class TarificationPageViewModel : ViewModelBase
    {
        public ICommand RefreshFilters { get; }
        private readonly SessionDataStore _sessionDataStore;
        public int SelectedIndex
        {
            get
            {
                return -1;
            }
        }
        private string _hoursFilteredCount;
        public string HoursFilteredCount 
        {
            get 
            { 
                return _hoursFilteredCount; 
            }
            set
            {
                _hoursFilteredCount= value;
                OnPropertyChenged(nameof(HoursFilteredCount));
            }
        }

        private ComboboxItemViewModel _selectedGroup = null;
        public ComboboxItemViewModel SelectedGroup 
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
                GetFilteredTarificationList();
                GetHourseCount();
            } 
        }
        private DisciplineListRecordViewModel _selectedDiscipline = null;
        public DisciplineListRecordViewModel SelectedDiscipline
        {
            get
            {
                return _selectedDiscipline;
            }
            set
            {
                _selectedDiscipline = value;
                GetFilteredTarificationList();
                GetHourseCount();
            }
        }
        private ObservableCollection<TarificationRecordViewModel> _workloadsUser;
        public ObservableCollection<TarificationRecordViewModel> WorkloadsUser
        {
            get
            {
                return _workloadsUser;
            }
            set
            {
                _workloadsUser = value;
                OnPropertyChenged(nameof(WorkloadsUser));
            }
        }
        private ObservableCollection<ComboboxItemViewModel> _groupItems;
        public IEnumerable<ComboboxItemViewModel> GroupItems
        {
            get
            {
                return _groupItems;
            }
        }
        private ObservableCollection<DisciplineListRecordViewModel> _disciplineItems;
        public IEnumerable<DisciplineListRecordViewModel> DisciplineItems
        {
            get
            {
                return _disciplineItems;
            }
        }
        public TarificationPageViewModel(SessionDataStore sessionDataStore)
        {
            RefreshFilters = new RefreshFiltrsCommand(RefreshFiltersHandler);
            _workloadsUser = new ObservableCollection<TarificationRecordViewModel>();
            _groupItems = new ObservableCollection<ComboboxItemViewModel>();
            _disciplineItems = new ObservableCollection<DisciplineListRecordViewModel>();
            _sessionDataStore = sessionDataStore;
            loadTarificationList();
            GetHourseCount();
            LoadStudentGroup();
            LeadDisciplineList();
        }
        /// <summary>
        /// Метод производить загрузку данных коллекции групп студентов
        /// </summary>
        private void LoadStudentGroup()
        {
            foreach (StudentGroup Record in _sessionDataStore.SessionAuthorisedUser.GetStudentGroupsList())
            {
                _groupItems.Add(new ComboboxItemViewModel(Record));
            }
        }
        /// <summary>
        /// Метод производит загрузку данных коллекции учебных дисциплин
        /// </summary>
        private void LeadDisciplineList()
        {
            foreach (Discipline Record in _sessionDataStore.SessionAuthorisedUser.GetDisciplineList())
            {
                _disciplineItems.Add(new DisciplineListRecordViewModel(Record));
            }
        }
        /// <summary>
        /// Метод производить загрузку данных коллекции загруженности пользователя
        /// </summary>
        private void loadTarificationList()
        {
            WorkloadsUser.Clear();
            foreach (TarificationWorkloadRecord Record in _sessionDataStore.SessionAuthorisedUser.GetUserTarificationList())
            {
                _workloadsUser.Add(new TarificationRecordViewModel(Record));
            }
        }
        private void GetHourseCount()
        {
            double localCount = 0;
            foreach (TarificationRecordViewModel Record in WorkloadsUser)
            {
                localCount += Convert.ToDouble(Record.HourCount);
            }
            HoursFilteredCount = localCount.ToString();
        }
        /// <summary>
        /// Метод обнуления фильтров 
        /// </summary>
        private void RefreshFiltersHandler() 
        {
            (SelectedDiscipline,SelectedGroup) = (null,null);
            OnPropertyChenged(nameof(SelectedIndex));
        }
        /// <summary>
        /// Метод осуществляет фильтрацию данных коллекции записей тарификации
        /// </summary>
        private void GetFilteredTarificationList()
        {
            // коллекция обновляется
            loadTarificationList();
            // Создаётся локальная коллекция для добавления в неё подходящих по фильтру записей
            ObservableCollection<TarificationRecordViewModel> localCollection = new ObservableCollection<TarificationRecordViewModel>();
            if (SelectedDiscipline != null && SelectedGroup != null)
            {
                for (int i = 0; i < WorkloadsUser.Count; i++)
                {
                    if (WorkloadsUser[i].DisciplinePrymaryKey == SelectedDiscipline.DisciplinePrimaryKey && WorkloadsUser[i].StudentGroupPrymaryKey == SelectedGroup?.GroupPrimaryKey)
                    {
                        localCollection.Add(WorkloadsUser[i]);
                    }
                }
                WorkloadsUser = localCollection;
            }
            else if (SelectedDiscipline != null)
            {
                for (int i = 0; i < WorkloadsUser.Count; i++)
                {
                    if (WorkloadsUser[i].DisciplinePrymaryKey == SelectedDiscipline.DisciplinePrimaryKey)
                    {
                        localCollection.Add(WorkloadsUser[i]);
                    }
                }
                WorkloadsUser = localCollection;
            }
            else if (SelectedGroup != null)
            {
                for (int i = 0; i < WorkloadsUser.Count; i++)
                {
                    if (WorkloadsUser[i].StudentGroupPrymaryKey == SelectedGroup.GroupPrimaryKey)
                    {
                        localCollection.Add(WorkloadsUser[i]);
                    }
                }
                WorkloadsUser = localCollection;
            }
        }
    }
}
