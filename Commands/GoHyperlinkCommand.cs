using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.Commands
{
    public class GoHyperlinkCommand : CommandBase
    {
        private readonly string hyperlinkAddress;

        public GoHyperlinkCommand(string hyperlinkAddress)
        {
            this.hyperlinkAddress = hyperlinkAddress;
        }

        public override void Execute(object parameter)
        {
            Process.Start(hyperlinkAddress);
        }
    }
}
