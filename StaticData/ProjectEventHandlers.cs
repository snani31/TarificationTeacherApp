using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerificationTeacherApp.StaticData
{
    public static class ProjectEventHandlers
    {
        public static void ErrorMassage(string Message,Exception ex)
        {
            MessageBox.Show($"{Message} Подробнее об ошибке: \n {ex}");
        }
    }
}
