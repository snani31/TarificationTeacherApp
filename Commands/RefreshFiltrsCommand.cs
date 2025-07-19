using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerificationTeacherApp.Commands
{
    public class RefreshFiltrsCommand : CommandBase
    {
        private event Action _executeRefreshEvent;
        public RefreshFiltrsCommand(Action executeRefreshEvent)
        {
            _executeRefreshEvent += executeRefreshEvent;
        }

        public override void Execute(object parameter)
        {
            _executeRefreshEvent();
        }
    }
}
