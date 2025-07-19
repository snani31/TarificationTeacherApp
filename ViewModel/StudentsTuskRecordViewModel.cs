using ModelSQLBussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerificationTeacherApp.ViewModel
{
    public class StudentsTuskRecordViewModel
    {
        private readonly string _defaultCardTytle = string.Empty;
        private readonly string _defaultCardColorBackground;
        private readonly string _defaultCardPicture;
        private readonly string _defaultCardColorBrush;
        private readonly Tusk _tusk;
        public readonly bool _isRated;
        private readonly Dictionary<int, string> _CardColorsBackground = new Dictionary<int, string>()
        {
             { 2, "#c72e32"},
             { 3, "#f28a03"},
             { 4, "#02fffe"},
             { 5, "#14f905"}
        };
        private readonly Dictionary<int, string> _CardColorsBrush = new Dictionary<int, string>()
        {
             { 2, "#590027"},
             { 3, "#704206"},
             { 4, "#092e92"},
             { 5, "#1a2c2d"}
        };
        private readonly Dictionary<int, string> _CardPictures = new Dictionary<int, string>()
        {
             { 2, "/Styles/Icons/Grades/Bad.png"},
             { 3, "/Styles/Icons/Grades/Satisfactorily.png"},
             { 4, "/Styles/Icons/Grades/Good.png"},
             { 5, "/Styles/Icons/Grades/Great.png"}
        };
        public StudentsTuskRecordViewModel(string defaultCardTytle)
        {
            _defaultCardTytle = defaultCardTytle;
            _defaultCardColorBackground = "#94928b";
            _defaultCardPicture = "/Styles/Icons/Grades/Plus.png";
            _defaultCardColorBrush = "#706f6c";
        }
        public StudentsTuskRecordViewModel(Tusk tusk)
        {
            _tusk = tusk;
            _isRated = tusk.grade == null ? false : true ;
            _defaultCardColorBackground = "#c24b98";
            _defaultCardPicture = "/Styles/Icons/Grades/NonGraded.png";
            _defaultCardColorBrush = "#301746";
        }
        public int TuskPrimaryKey => _tusk.id;
        public string TuskTytle => _tusk?.tytle ?? _defaultCardTytle;

        public string TuskCardPicture
        { 
            get
            {
                return _tusk?.grade == null ? _defaultCardPicture : _CardPictures[_tusk.grade.grade];
            } 
        }
        public string TuskCardColorBackground
        {
            get
            {
                return _tusk?.grade == null ? _defaultCardColorBackground : _CardColorsBackground[_tusk.grade.grade];
            }
        }
        public string TuskCardColorBrush
        {
            get
            {
                return _tusk?.grade == null ? _defaultCardColorBrush : _CardColorsBrush[_tusk.grade.grade];
            }
        }
    }
}
