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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.Task21". При необходимости она может быть перемещена или удалена.
            this.task21TableAdapter.Fill(this.dbDataSet.Task21);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.storage". При необходимости она может быть перемещена или удалена.
            this.storageTableAdapter.Fill(this.dbDataSet.storage);

        }

        private void FillGridByReader(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(Properties.Settings.Default.dbConnectionString);
            // создаем объект связь с бд, строку соединения берём из свойств проекта, можно задать самим строкой
            con.Open();
            // подключаемся к бд
            String str = "SELECT s.war_number, s.responsible, COUNT(DISTINCT a.prod_code) FROM storage s, availability a WHERE s.war_number = a.war_number GROUP BY s.war_number";
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
            //bindingNavigator1.BindingSource = bs;
            dataGridView1.Refresh();
        }

        private void Record(object sender, EventArgs e)
        {
            //очистим таблицу от предыдущих значений
            dbDataSet.Task21.Clear();           
            
            //пробегаем по складам
            foreach (dbDataSet.storageRow sRow in dbDataSet.storage.Rows)
            {
                //код товара
                int prod_code = -1;
                //количество товаров
                int count = 0;
                //создаем столько строк, сколько складов
                dbDataSet.Task21Row tRow = dbDataSet.Task21.NewTask21Row();

                tRow.war_number = sRow.war_number;
                tRow.responsible = sRow.responsible;

                foreach (dbDataSet.availabilityRow aRow in dbDataSet.availability.Rows)
                {
                    if (tRow.war_number == aRow.war_number && prod_code != aRow.prod_code)
                    {
                        Console.WriteLine("YEEE");
                        prod_code = aRow.prod_code;
                        count++;
                        
                    }
                }
                tRow.count = count;
                Console.WriteLine(tRow.count);
                dbDataSet.Task21.AddTask21Row(tRow);
            }



            /*
            //очистим таблицу от предыдущих значений
            dbDataSet.Task21.Clear();           
            //код товара
            int prod_code = -1;
            //количество товаров
            int count = 0;
            //пройдём по всем строкам таблицы «Наличие товара»
            foreach (dbDataSet.availabilityRow aRow in dbDataSet.availability.Rows)
            {
                //строка результирующей таблицы
                dbDataSet.Task21Row tRow = null;
                //если первая запись
                if (tRow == null)
                {                   
                    prod_code = aRow.prod_code;
                    count++;

                    tRow = dbDataSet.Task21.NewTask21Row();

                    tRow.war_number = aRow.war_number;
                    tRow.responsible = dbDataSet.storage.responsibleColumn.ToString();
                    tRow.count = count;
                    
                }
                //если тот же товар, но склад отличается
                else if (prod_code == aRow.prod_code && aRow.war_number != tRow.war_number)
                {
                    count++;
                }
                
                    
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
            }*/
            dataGridView2.DataSource = dbDataSet.Task21;
            dataGridView2.Refresh();
        }
    }
}
