using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TerificationTeacherApp.Model;
using TerificationTeacherApp.Stores;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp.Commands
{
    internal class MakeAuthorisationCommand : CommandBase
    {
        public Thread AuthorisationThread { get; private set; }
        private readonly string _sqlConnParams;
        private readonly Action<string, Exception> _action;
        private readonly AuthorisationPageViewModel _authorisationPageViewModel;
        private readonly SessionDataStore _sessionDataStore;
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _makeNextPageIfRegistrationComplite;
        public MakeAuthorisationCommand(AuthorisationPageViewModel authorisationPageViewModel, SessionDataStore sessionDataStore, NavigationStore navigationStore, Func<ViewModelBase> makeNextPageIfRegistrationComplite)
        {
            _sqlConnParams = sessionDataStore.connParamsStr;
            _action = sessionDataStore.errorHandler;
            _sessionDataStore = sessionDataStore;
            _navigationStore = navigationStore;
            _authorisationPageViewModel = authorisationPageViewModel;
            _authorisationPageViewModel.PropertyChanged += OnViewModelPropertyChange;
            _makeNextPageIfRegistrationComplite = makeNextPageIfRegistrationComplite;
            AuthorisationThread = new Thread(MakeAuthorisation);
        }
        public override void Execute(object parameter)
        {
            // поток пересоздаётся для того, чтобы один и тот же не пришлось вызывать повторно, с чем бы могли возникнуть проблемы
            AuthorisationThread = new Thread(MakeAuthorisation);
            AuthorisationThread.Start();
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return ((AuthorisationThread.ThreadState != ThreadState.Running && AuthorisationThread.ThreadState != ThreadState.WaitSleepJoin) && !string.IsNullOrEmpty(_authorisationPageViewModel.UserLogin) && !string.IsNullOrEmpty(_authorisationPageViewModel.UserPassword)) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AuthorisationPageViewModel.UserLogin) || e.PropertyName == nameof(AuthorisationPageViewModel.UserPassword))
            {
                OnCanExecuteChanged();
            }
        }

        private void MakeAuthorisation()
        {
            Model.Authorization authorisation = new Model.Authorization(_authorisationPageViewModel.UserLogin, _authorisationPageViewModel.UserPassword, _sqlConnParams, _action);
            if (authorisation.authorizationResult)
            {
                _sessionDataStore.SessionAuthorisedUser = authorisation.AuthorizedUser;
                _navigationStore.CurrentViewModel = _makeNextPageIfRegistrationComplite();
            }
        }

    }
}
