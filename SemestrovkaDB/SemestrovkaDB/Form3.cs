using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemestrovkaDB
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void FillGridByReader(object sender, EventArgs e)
        {
            if (!txtProd.Text.Equals(""))
            {
                MySqlConnection con = new MySqlConnection(Properties.Settings.Default.dbConnectionString);
                // создаем объект связь с бд, строку соединения берём из свойств проекта, можно задать самим строкой
                con.Open();
                // подключаемся к бд
                String str = "SELECT availability.prod_code, availability.war_number, storage.responsible, availability.quantity FROM storage, availability WHERE storage.war_number = availability.war_number AND prod_code = " + txtProd.Text;
                // задаем текст запроса, добавляем текст из txtProd

                MySqlCommand cmd = new MySqlCommand(str, con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                // создали команду и выполнили метод ExecuteReader

                DataTable dt = new DataTable();
                dt.Load(rdr);
                con.Clone();
                // при помощи ридера заполнили таблицу и закрыли соединение с бд

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                // программно создали объект BindingSource и связали его с таблицей, далее грид и навигатор укажем на него для связи с таблицей

                dataGridView1.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
                dataGridView1.Refresh();
            }
            else
            {
                this.dataGridView1.DataSource = task1BindingSource;
                dataGridView1.Refresh();
            }
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet1.Task11". При необходимости она может быть перемещена или удалена.
            this.task11TableAdapter.Fill(this.dbDataSet1.Task11);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.shipment". При необходимости она может быть перемещена или удалена.
            this.shipmentTableAdapter.Fill(this.dbDataSet.shipment);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Task1". При необходимости она может быть перемещена или удалена.
            this.task1TableAdapter.Fill(this.dbDataSet.Task1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.storage". При необходимости она может быть перемещена или удалена.
            //this.storageTableAdapter.Fill(this.dbDataSet.storage);

            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;
            //FillGridByReader();

            this.storageTableAdapter.Fill(this.dbDataSet.storage);
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);

        }

        private void Record(object sender, EventArgs e)
        {
            if (!txtProd2.Text.Equals(""))
            {
                //очистим таблицу от предыдущих значений
                dbDataSet.Task11.Clear();
                //пройдём по всем строкам таблицы «Наличие товара»
                foreach (dbDataSet.availabilityRow aRow in dbDataSet.availability.Rows)
                {
                    //если есть товар с заданным кодом
                    if (aRow.prod_code == Convert.ToInt32(txtProd2.Text))
                    {
                        //пройдем по всем строкам таблицы "Склады"
                        foreach (dbDataSet.storageRow sRow in dbDataSet.storage.Rows)
                        {
                            // если нашли соответствие        
                            if (aRow.war_number == sRow.war_number)
                            {
                                //создаем новую строку таблицы "Task11"
                                dbDataSet.Task11Row tRow = dbDataSet.Task11.NewTask11Row();
                                tRow.prod_code = Convert.ToInt32(txtProd2.Text);
                                tRow.war_number = aRow.war_number;
                                tRow.responsible = sRow.responsible;
                                tRow.quantity = aRow.quantity;
                                dbDataSet.Task11.AddTask11Row(tRow);
                            }
                        }
                    }
                }
                dataGridView2.DataSource = dbDataSet.Task11;
                dataGridView2.Refresh();
            }
            else
            {
                this.dataGridView2.DataSource = task11BindingSource;
                dataGridView2.Refresh();
            }
        }
    }
}
