using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication.Interface;

namespace WindowsFormsApplication.Models
{
    public class MainModel : IModel
    {
        private IPresenter _presenter;

        public MainModel(IPresenter presenter)
        {
            _presenter = presenter;
        }

        public string GetText(string filename)
        {
            string text = "";
            try
            {
                File.ReadAllText(filename);
            }
            catch (Exception)
            {
                text = "Error, dude";
            }
            return text;
        }
    }
}
