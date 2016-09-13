using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication.Interface;
using WindowsFormsApplication.Models;

namespace WindowsFormsApplication.Presenters
{
    public class MainPresenter : IPresenter
    {
        private IView _view;
        private IModel _model;

        public MainPresenter()
        {
            _view = new Form1(this);
            _model = new MainModel(this);
        }

        public void OpenFile(string filename)
        {
            string text = _model.GetText(filename);
            _view.ShowText(text);
        }

        public void Start()
        {
            _view.Show();
        }
    }
}
