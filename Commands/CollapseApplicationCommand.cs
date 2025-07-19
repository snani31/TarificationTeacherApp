using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.Commands
{
    public class CollapseApplicationCommand : CommandBase
    {
        private readonly Action _collapseAction;

        public CollapseApplicationCommand(Action collapseAction)
        {
            _collapseAction = collapseAction;
        }

        public override void Execute(object parameter)
        {
            _collapseAction?.Invoke();
        }
    }
}
