using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace TimeTable
{
    class Message
    {
        bool result;

        public Message(bool simpleMess, Page page, string title, string message, string answer, string yes, string no)
        {
            if (simpleMess)
                SimpleMessage(page, title, message, answer);
            else
                ComplexMessage(page, title, message, yes, no);
        }

        private void SimpleMessage(Page page, string title, string message, string answer)
        {
            page.DisplayAlert(title, message, answer);
        }

        private async void ComplexMessage(Page page, string title, string message, string yes, string no)
        {
            result = await page.DisplayAlert(title, message, yes, no);
        }

        public bool getResult()
        {
            return result;
        }
    }
}
