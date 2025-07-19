using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    public class StudentRecordViewModel : ViewModelBase
    {
        private readonly Student _student;

        public StudentRecordViewModel(Student student)
        {
            _student = student;
        }

        public string StudentName => ($"{_student.surName} {_student.name} {_student.middleName}");
        public int StudentPrymaryKey => _student.id;
    }
}
