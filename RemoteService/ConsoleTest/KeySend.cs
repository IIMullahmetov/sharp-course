using ConsoleTest.Presenters;
using System.Text;
using System.Windows.Forms;

namespace ConsoleTest
{
    class KeySend
    {
        //КОМАНДЫ

        // Следующий слайд
        const string codeNext = "-1";
        // Предыдущий слайд
        const string codePrev = "-2";
        // Запуск презентации
        const string codePlay = "-3";
        // Выход
        const string codeExit = "-4";
        // Закрытие программы
        const string codeClose = "-5";

        public static bool ParseCommand(Presenter presenter, byte[] receiveBuffer)
        {
            string code = Encoding.Unicode.GetString(receiveBuffer);
            if (WindowObserver.IsPresentationWindow(presenter)) //если сейчас активное окно - это окно презентации
            {
                string command = null;
                switch (code) // определяемся с командами клиента, 49 - это код символа в ASCII
                {
                    case codeNext:
                        command = presenter.GetKey(0);
                        break;
                    case codePrev:
                        command = presenter.GetKey(1);
                        break;
                    case codePlay: // запуск презентации
                        command = presenter.GetKey(2);
                        break;
                    case codeExit: // выход
                        command = presenter.GetKey(3);
                        break;
                    case codeClose: //закрытие программы
                        break;
                    default: //переход к слайду
                        command = presenter.GetCommandGoPage(code);
                        break;
                }
                SendKeys.SendWait(command);
                return true;
            }
            return false;
        }
    }
}
