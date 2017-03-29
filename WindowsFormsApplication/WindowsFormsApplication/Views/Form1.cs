using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication.Interface;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form, IView
    {
        private IPresenter _presenter;

        public Form1(IPresenter presenter)
        {
            InitializeComponent();

            _presenter = presenter;
        }

        public new void Show()
        {
            Application.Run(this);
        }

        public void ShowText(string text)
        {
            textBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _presenter.OpenFile(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            var result = fd.ShowDialog();
            if (result != DialogResult.Cancel)
                textBox1.Text = fd.FileName;
        }
    }
}
