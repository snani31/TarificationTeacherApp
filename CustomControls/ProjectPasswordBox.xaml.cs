using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TerificationTeacherApp.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для ProjectPasswordBox.xaml
    /// </summary>
    public partial class ProjectPasswordBox : UserControl
    {
        private bool _isPasswordChanging;
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(ProjectPasswordBox),
                new FrameworkPropertyMetadata(string.Empty,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged,null,false,UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ProjectPasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public ProjectPasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }
        private void UpdatePassword()
        {
            if (_isPasswordChanging) return; 
            passwordBox.Password = Password;
        }
    }
}
