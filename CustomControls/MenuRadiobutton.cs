using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TerificationTeacherApp.CustomControls
{
    public class MenuRadiobutton : RadioButton
    {
        static MenuRadiobutton()
        {
             DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuRadiobutton), new FrameworkPropertyMetadata(typeof(MenuRadiobutton)));
        }
    }
}
