using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.Stores
{
    public class SessionDataStore
    {
        public readonly string connParamsStr;
        public readonly Action<string,Exception>  errorHandler;
        private TeacherUser _sessionAuthorisedUser;

        public SessionDataStore(string connStr, Action<string, Exception> action)
        {
            this.connParamsStr = connStr;
            this.errorHandler = action;
        }

        public TeacherUser SessionAuthorisedUser
        {
            get => _sessionAuthorisedUser;
            set 
            { 
                _sessionAuthorisedUser = value;
                OnUserAuthorisationStateChange();
            }
        }
        
        public event Action UserAuthorisationStateChange;

        /// <summary>
        /// Возникает в случае, если статус авторизации пользователя изменился (авторизация нового пользователя или текущий пользователь разлогинился)
        /// </summary>
        private void OnUserAuthorisationStateChange()
        {
            UserAuthorisationStateChange?.Invoke();
        }
    }
}
