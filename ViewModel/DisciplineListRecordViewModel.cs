using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    public class DisciplineListRecordViewModel : ViewModelBase
    {

        private readonly Discipline _disciplineListRecord;

        public DisciplineListRecordViewModel(Discipline disciplineListRecord)
        {
            _disciplineListRecord = disciplineListRecord;
        }
        public int DisciplinePrimaryKey => _disciplineListRecord.id;
        public string DisciplineName => _disciplineListRecord.tytle;
    }
    
}
