using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TerificationTeacherApp.Commands;
using TerificationTeacherApp.Stores;
using TerificationTeacherApp.View;

namespace TerificationTeacherApp.ViewModel
{
    public class MeinWindowViewModel : ViewModelBase
    {
        private readonly SessionDataStore _sessionDataStore;
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public readonly string _sqlConnParams;
        public readonly Action<string, Exception> _errorMassageHandler;

        private bool _tarificationPageChaked = false;
        public bool TarificationPageChaked
        {
            get
            {
                return _tarificationPageChaked;
            }
            set
            {
                _tarificationPageChaked = value;
                OnPropertyChenged(nameof(TarificationPageChaked));
            }
        }

        public ICommand GoTarificationCommand { get; }
        public ICommand GoStudentsTusksPage { get; }
        public ICommand GoAuthorisationPage { get; }
        public ICommand GoAboutProgrammPage { get; }
        public ICommand ExitApplication { get; }
        public ICommand CollapseApplication { get; }

        public bool UserIsAuthorised
        {
            get
            {
                return !(_sessionDataStore.SessionAuthorisedUser == null);
            }
        }

        public MeinWindowViewModel(string sqlConnParams, Action<string, Exception> errorAction, NavigationStore navigationStore, SessionDataStore sessionDataStore)
        {
            _sessionDataStore = sessionDataStore;
            _sqlConnParams = sqlConnParams;
            _errorMassageHandler = errorAction;
            _navigationStore = navigationStore;
            GoTarificationCommand = new NavigationCommand(_navigationStore, CreateTarificationDataPageViewModel);
            GoStudentsTusksPage = new NavigationCommand(_navigationStore, CreateStudentsTusksDataPageViewModel);
            GoAuthorisationPage = new NavigationCommand(_navigationStore, CreateAuthorisationPageViewModel);
            ExitApplication = new CloseApplicationCommand(CloseApplicationHandler);
            CollapseApplication = new CollapseApplicationCommand(CollapseWindowHandler);
            GoAboutProgrammPage = new NavigationCommand(_navigationStore,CreateAboutPageViewModel);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChange;
            _sessionDataStore.UserAuthorisationStateChange += OnUserAuthorisationStateChange;
        }

        public TarificationPageViewModel CreateTarificationDataPageViewModel()
        {
            TarificationPageChaked = true;
            return new TarificationPageViewModel(_sessionDataStore);
        }
        private StudentsTusksDataViewModel CreateStudentsTusksDataPageViewModel()
        {
            return new StudentsTusksDataViewModel(_sessionDataStore,_navigationStore);
        }
        private AuthorisationPageViewModel CreateAuthorisationPageViewModel()
        {
            _sessionDataStore.SessionAuthorisedUser = null;
            return new AuthorisationPageViewModel(_sessionDataStore,_navigationStore, CreateTarificationDataPageViewModel);
        }
        private AboutProgrammPageViewModel CreateAboutPageViewModel()
        {
            return new AboutProgrammPageViewModel();
        }
        private void OnCurrentViewModelChange()
        {
            OnPropertyChenged(nameof(CurrentViewModel));
        }
        private void OnUserAuthorisationStateChange()
        {
            OnPropertyChenged(nameof(UserIsAuthorised));
        }
        /// <summary>
        /// Обработчик для завершения работы приложения
        /// </summary>
        private void CloseApplicationHandler()
        {
            Environment.Exit(0);
        }

        private void CollapseWindowHandler()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
} 