using System;
using System.Windows.Forms;
using ConsoleTest.Presenters;

namespace ConsoleTest
{
    class KeySend
    {
        private static int previousCommand = 1;

        // Следующий слайд
        private const int codeNext = -1;
        // Предыдущий слайд
        private const int codePrev = -2;
        // Запуск режима презентации
        private const int codePlay = -3;
        // Выход из режима презентации
        private const int codeClose = -4;
        // Закрытие презентации
        private const int codeExit = -5;

        public static bool ParseCommand(Presenter presenter, byte[] receiveBuffer)
        {
            int code = BitConverter.ToInt32(receiveBuffer, 0);

            Console.WriteLine("ServerTask - " + code);
            /*
            int deviceCommand = previousCommand - code;
            switch (deviceCommand)
            {
                case -1:
                    code = -1;
                    break;
                case 1:
                    code = -2;
                    break;
            }
            previousCommand = code;
            */
            string command = null;
            switch (code)
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
                case codeClose: // выход
                    command = presenter.GetKey(3);
                    break;
                case codeExit: //закрытие презентации
                    command = presenter.GetCommandExitProgram();
                    break;
                default: //переход к слайду
                    command = presenter.GetCommandGoPage(code);
                    break;
            }
            if (WindowObserver.IsMyPresentation(presenter)) //если сейчас активное окно - это окно презентации
            {
                SendKeys.SendWait(command);
                return true;
            }
            return false;
        }
    }
}
