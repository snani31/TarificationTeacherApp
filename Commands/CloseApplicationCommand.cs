using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerificationTeacherApp.Commands
{
    public class CloseApplicationCommand : CommandBase
    {
        private readonly Action CloseApplication;

        public CloseApplicationCommand(Action closeApplication)
        {
            CloseApplication = closeApplication;
        }

        public override void Execute(object parameter)
        {
            CloseApplication?.Invoke();
        }
    }
}
