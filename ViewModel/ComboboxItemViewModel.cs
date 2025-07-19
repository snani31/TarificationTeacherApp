using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    public class ComboboxItemViewModel
    {
        private readonly StudentGroup _studentGroupListRecord;

        public ComboboxItemViewModel(StudentGroup studentGroupListRecord)
        {
            _studentGroupListRecord = studentGroupListRecord;
        }
        public int GroupPrimaryKey => _studentGroupListRecord.id;
        public string StudentGroupName => _studentGroupListRecord.tytle;
    }
}
