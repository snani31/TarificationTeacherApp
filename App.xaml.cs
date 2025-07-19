using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using TerificationTeacherApp.Model;
using TerificationTeacherApp.StaticData;
using TerificationTeacherApp.Stores;
using TerificationTeacherApp.ViewModel;

namespace TerificationTeacherApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly SessionDataStore _sessionDataStore;
        public App()
        {
            _navigationStore = new NavigationStore();
            _sessionDataStore = new SessionDataStore(new ConnectionParams().LoadObjFromJsonFile<ConnectionParams>(@"Json/MySQLConnectionParams.Json").ReturnConnStr(), ProjectEventHandlers.ErrorMassage);
            _meinVievModelObj = new MeinWindowViewModel(new ConnectionParams().LoadObjFromJsonFile<ConnectionParams>(@"Json/MySQLConnectionParams.Json").ReturnConnStr(), ProjectEventHandlers.ErrorMassage, _navigationStore, _sessionDataStore);
        }
        /// <summary>
        /// Основная вьюмодель главного окна;
        /// </summary>
        private readonly MeinWindowViewModel _meinVievModelObj;
        protected override void OnStartup(StartupEventArgs e)
        {
            // Указывается, что окно, которое должно быть открыто в первую очередь при запуске приложения - MeinWindow
            MainWindow = new MainWindow()
            {
                DataContext = _meinVievModelObj
            };

            _navigationStore.CurrentViewModel = new AuthorisationPageViewModel(_sessionDataStore,_navigationStore, _meinVievModelObj.CreateTarificationDataPageViewModel);
            JsonDataWork.UserMassage = delegate(string Massage) { MessageBox.Show(Massage); };
            MainWindow.Show();
        }
    }
}
