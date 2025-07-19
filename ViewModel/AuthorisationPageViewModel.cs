using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TerificationTeacherApp.Commands;
using TerificationTeacherApp.Stores;

namespace TerificationTeacherApp.ViewModel
{
    public class AuthorisationPageViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly SessionDataStore _sessionDataStore;
        private string _userLogin;
        public string UserLogin
        { 
            get 
            {
                return _userLogin; 
            } 
            set 
            {
                _userLogin = value;
                OnPropertyChenged(nameof(UserLogin));
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                _userPassword = value;
                OnPropertyChenged(nameof(UserPassword));
            }
        }

        public ICommand SubmitAuthorization { get;}

        public AuthorisationPageViewModel(SessionDataStore sessionDataStore, NavigationStore navigationStore, Func<ViewModelBase> makeNextPageIfRegistrationComplite)
        {
            _sessionDataStore = sessionDataStore;
            _navigationStore = navigationStore;
            SubmitAuthorization = new MakeAuthorisationCommand(this, _sessionDataStore, navigationStore, makeNextPageIfRegistrationComplite);
        }
    }
}
