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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void FillGridByReader(object sender, EventArgs e)
        {
            if (!aQuantity.Text.Equals("") && !sQuantity.Text.Equals(""))
            {
                MySqlConnection con = new MySqlConnection(Properties.Settings.Default.dbConnectionString);
                // создаем объект связь с бд, строку соединения берём из свойств проекта, можно задать самим строкой
                con.Open();
                // подключаемся к бд
                // задаем текст запроса
                //String str = "SELECT a.war_number FROM availability a WHERE a.quantity > " + aQuantity.Text + " AND NOT EXISTS (SELECT s.quantity FROM shipment s WHERE s.quantity < " + sQuantity.Text + " AND a.prod_code = s.prod_code)";
                String str = "SELECT DISTINCT a.war_number FROM availability a WHERE a.quantity > " + aQuantity.Text + " AND NOT EXISTS (SELECT s.prod_code FROM shipment s WHERE s.quantity < " + sQuantity.Text + " AND a.prod_code = s.prod_code AND a.war_number = s.war_number)";

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
                //bindingNavigator1.BindingSource = bs;
                dataGridView1.Refresh();
            }
        }

        private void Record(object sender, EventArgs e)
        {
            if (!aQuantity2.Text.Equals("") && !sQuantity2.Text.Equals(""))
            {
                //очистим таблицу от предыдущих значений
                dbDataSet.Task31.Clear();
                //пройдём по всем строкам таблицы «Наличие товара»
                foreach (dbDataSet.availabilityRow aRow in dbDataSet.availability.Rows)
                {
                    //если кол-во товара больше введеного
                    if (aRow.quantity > Convert.ToInt32(aQuantity2.Text))
                    {
                        //пройдем по всем строкам таблицы "Отгрузка"
                        foreach (dbDataSet.shipmentRow sRow in dbDataSet.shipment.Rows)
                        {
                            bool ok = true;
                            // если кол-во отгрузки больше введеного и это тот же склад и тот же товар      
                            if (sRow.quantity > Convert.ToInt32(sQuantity2.Text) && aRow.war_number == sRow.war_number && aRow.prod_code == sRow.prod_code)
                            {
                                //проверим не ли отгрузок меньше введеного значения
                                foreach (dbDataSet.shipmentRow s2Row in dbDataSet.shipment.Rows)
                                {
                                    //проверяем, не сравниваем ли мы отгрузку с собой же - если нет, то проверяем кол-во отгрузки больше введеного 
                                    if ((sRow.war_number == s2Row.war_number && sRow.prod_code == s2Row.prod_code && sRow.ship_date != s2Row.ship_date) && s2Row.quantity < Convert.ToInt32(sQuantity2.Text))
                                    {
                                        ok = false;
                                        break;
                                    }
                                }
                                //если все отгрузки товара больше введеного
                                if (ok)
                                {
                                    //создаем новую строку таблицы "Task31"
                                    dbDataSet.Task31Row tRow = dbDataSet.Task31.NewTask31Row();
                                    tRow.war_number = aRow.war_number;
                                    dbDataSet.Task31.AddTask31Row(tRow);
                                }
                            }
                        }
                    }
                }
                dataGridView2.DataSource = dbDataSet.Task31;
                dataGridView2.Refresh();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.shipment". При необходимости она может быть перемещена или удалена.
            this.shipmentTableAdapter.Fill(this.dbDataSet.shipment);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Task31". При необходимости она может быть перемещена или удалена.
            this.task31TableAdapter1.Fill(this.dbDataSet.Task31);

            dataGridView1.AutoGenerateColumns = true;

            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            this.shipmentTableAdapter.Fill(this.dbDataSet.shipment);
        }
    }
}
