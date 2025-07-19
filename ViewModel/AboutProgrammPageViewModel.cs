using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TerificationTeacherApp.Commands;

namespace TerificationTeacherApp.ViewModel
{
    public class AboutProgrammPageViewModel : ViewModelBase
    {
        public ICommand RazrabHyperlink { get; }
        public string AboutProgrammText 
        { 
            get { return "Программа создана специально для Челябинского юридического колледжа." +
                    " В первую очередь она необходима для оптимизации рабочего процесса педагогического персонала. \r\n" +
                    " Оптимизация происходит за счёт сокращения преподавателями временных издержек на выполнение рутинных процедур, так или иначе связанных" +
                    " с получением или обработкой рабочей информации. \r\n В первую очередь программа нужна, чтобы преподаватели могли беспрепятственно узнавать" +
                    " свою загруженность и работать со студентами, назначая или оценивая индивидуальные задания."; }
        }
        public string AuthorizationAboutNavText
        {
            get
            {
                return "Стартовая открывающаяся в приложении страница - страница авторизации. Нужна для подтверждения права доступа пользователя основываясь на" +
                    " введённых им верифицированных данных, если вы повторно откроете эту страницу, вам придётся авторизоваться повторно";
            }
        }
        public string TarificationAboutNavText
        {
            get
            {
                return "Страница тарификации отображает выгруженные из базы данных сведения о загруженности авторизованного преподавателя, она будет открыта автоматически после авторизации";
            }
        }
        public string StudentsPageAboutNavText
        {
            get
            {
                return "Страница студентов призвана выполнять следующие функции: " +
                    "\r\n1) Доступ к списку групп и студентов" +
                    "\r\n2) Доступ к списку заданий студента" +
                    "\r\n3) Оценивание выбранного задания" +
                    "\r\n4) Сведения о ранее оценённых заданиях" +
                    "\r\n5) Функция выдачи нового задания"
                    ;
            }
        }
        public string ProgrammInfoPageAboutNavText
        {
            get
            {
                return "Страница сведений о программе, именно находясь на ней Вы сейчас всё это читаете." +
                    "\r\n Нужна для предоставления основных справочных данных касательно выполняемых функций и принципа работы приложения"
                    ;
            }
        }
        public string ProgrammerInfoText
        {
            get
            {
                return "Разработана студентом группы ИС 1-19 Поповым Артёмом Эдуардовичем в качестве программного решения в рамках работы над дипломным проектом";
            }
        }
        public AboutProgrammPageViewModel()
        {
            RazrabHyperlink = new GoHyperlinkCommand("https://vk.com/id349243673");
        }
    }
}
