using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    public class GradeRecordViewModel
    {
        public string GradeTytle { get; }
        public string GradePicture { get; }
        public string GradeColorBackground { get; }
        public string GradeColorBrush { get; }
        public string UpdatedTuskStatus { get; }
        public int GradeNum { get; }
        public GradeRecordViewModel(string gradeTytle, string gradePicture, string gradeColorBackground, string gradeColorBrush, int gradeNum, string updatedTuskStatus)
        {
            GradeTytle = gradeTytle;
            GradePicture = gradePicture;
            GradeColorBackground = gradeColorBackground;
            GradeColorBrush = gradeColorBrush;
            GradeNum = gradeNum;
            UpdatedTuskStatus = updatedTuskStatus;
        }
    }
}
