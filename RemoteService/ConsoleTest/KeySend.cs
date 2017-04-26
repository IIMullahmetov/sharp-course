using ConsoleTest.Presenters;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace ConsoleTest
{
    class KeySend
    {
        //КОМАНДЫ

        // Следующий слайд
        private const int codeNext = -1;
        // Предыдущий слайд
        private const int codePrev = -2;
        // Запуск презентации
        private const int codePlay = -3;
        // Выход
        private const int codeClose = -4;
        // Закрытие программы
        private const int codeExit = -5;

        private static bool exitCommand = false;

        public static bool ParseCommand(ServerConnection connection, Presenter presenter, byte[] receiveBuffer)
        {
            int code = BitConverter.ToInt32(receiveBuffer, 0);

            Console.WriteLine("\nServerTask - " + code + "\n");

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
                case codeClose: // выход
                    command = presenter.GetKey(3);
                    break;
                case codeExit: //закрытие презентации
                    command = presenter.GetKey(4);
                    exitCommand = true;
                    break;
                default: //переход к слайду
                    command = presenter.GetCommandGoPage(code);
                    break;
            }
            if (WindowObserver.IsMyPresentation(presenter)) //если сейчас активное окно - это окно презентации
            {
                SendKeys.SendWait(command);
                if (exitCommand)
                {
                    connection.Shutdown();
                    presenter.DeleteDirectory();
                    exitCommand = false;
                }
                return true;
            }
            return false;
        }
    }
}
