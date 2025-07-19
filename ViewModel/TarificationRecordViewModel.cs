using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    /// <summary>
    /// Данные одной записи тарификации пользователя, которые должны быть записаны в строке таблицы
    /// </summary>
    public class TarificationRecordViewModel : ViewModelBase
    {
        private readonly TarificationWorkloadRecord _tarificationWorkloadRecord;

        public TarificationRecordViewModel(TarificationWorkloadRecord tarificationWorkloadRecord)
        {
            _tarificationWorkloadRecord = tarificationWorkloadRecord;
        }

        public string TeacherName => _tarificationWorkloadRecord.Teacher.UserFirstname;
        public string StudentGroupName => _tarificationWorkloadRecord.StudentGroup.tytle;
        public string HourCount => _tarificationWorkloadRecord.HourCount.ToString();
        public string Discipline => _tarificationWorkloadRecord.Discipline.tytle;
        public int DisciplinePrymaryKey => _tarificationWorkloadRecord.Discipline.id;
        public int StudentGroupPrymaryKey => _tarificationWorkloadRecord.StudentGroup.id;
    }
}
