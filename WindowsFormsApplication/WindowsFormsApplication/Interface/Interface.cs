using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication.Interface
{
    public interface IView
    {
        void Show();
        void ShowText(string text);
    }

    public interface IModel
    {
        string GetText(string filename);
    }

    public interface IPresenter
    {
        void Start();
        void OpenFile(string filename);
    }
}
