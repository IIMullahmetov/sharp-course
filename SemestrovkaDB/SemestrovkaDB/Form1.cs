using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemestrovkaDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void storageBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.storageBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dbDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.shipment". При необходимости она может быть перемещена или удалена.
            this.shipmentTableAdapter.Fill(this.dbDataSet.shipment);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.storage". При необходимости она может быть перемещена или удалена.
            this.storageTableAdapter.Fill(this.dbDataSet.storage);

        }
        
        private void storageDataGridView_Click(object sender, EventArgs e)
        {
            BindingNavigator.BindingSource = storageBindingSource;

            this.availabilityDataGridView.DataSource = nocascadefkBindingSource;

        }

        private void availabilityDataGridView_Click(object sender, EventArgs e)
        {
            //BindingNavigator.BindingSource = availabilityBindingSource;
            //this.availabilityDataGridView.DataSource = nocascadefkBindingSource;
            //this.shipmentDataGridView.DataSource = ;
            this.availabilityDataGridView.DataSource = availabilityBindingSource;
            this.shipmentDataGridView.DataSource = cascadefkBindingSource;
        }

        private void shipmentDataGridView_Click(object sender, EventArgs e)
        {
            BindingNavigator.BindingSource = shipmentBindingSource;

            this.shipmentDataGridView.DataSource = shipmentBindingSource;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            //Считываем номер строки и столбца активной ячейки.
            int nRow = availabilityDataGridView.CurrentCell.RowIndex;
            int nCol = availabilityDataGridView.CurrentCell.ColumnIndex;
            // Если строка – не первая, уменьшаем номер строки на 1,
            // в противном случае соответствующая
            if (nRow > 0)
                availabilityDataGridView.CurrentCell = availabilityDataGridView[nCol, --nRow];
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Считываем номер строки и столбца активной ячейки.
            int nRow = availabilityDataGridView.CurrentCell.RowIndex;
            int nCol = availabilityDataGridView.CurrentCell.ColumnIndex;
            // Если строка – не последняя, увеличиваем номер строки на 1,
            // в противном случае соответствующая
            if (nRow < availabilityDataGridView.RowCount - 1)
                availabilityDataGridView.CurrentCell = availabilityDataGridView[nCol, ++nRow];
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            //Считываем номер строки и столбца активной ячейки.
            int nRow = availabilityDataGridView.CurrentCell.RowIndex;
            int nCol = availabilityDataGridView.CurrentCell.ColumnIndex;
            // Если строка – не первая, то делаем первой,
            // в противном случае соответствующая
            if (nRow != 0)
                availabilityDataGridView.CurrentCell = availabilityDataGridView[nCol, 0];
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            //Считываем номер строки и столбца активной ячейки.
            int nRow = availabilityDataGridView.CurrentCell.RowIndex;
            int nCol = availabilityDataGridView.CurrentCell.ColumnIndex;
            // Если строка – не последняя, то делаем последней,
            // в противном случае соответствующая
            if (nRow != availabilityDataGridView.RowCount - 1)
                availabilityDataGridView.CurrentCell = availabilityDataGridView[nCol, availabilityDataGridView.RowCount - 1];
        }

        private void availabilityDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (availabilityDataGridView.CurrentCell != null)
            {
                //Если существует выбранная ячейка
                int nRow = availabilityDataGridView.CurrentCell.RowIndex;
                //Первая строка
                if (nRow == 0)
                {
                    btnPrev.Enabled = false;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnPrev.Enabled = true;
                    btnFirst.Enabled = true;
                }
                //Последняя строка
                if (nRow == availabilityDataGridView.RowCount - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Сохранение данных
            this.availabilityTableAdapter.Update(this.dbDataSet.availability);
            //Обновление данных из источника
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            //Обновление состояния навигатора
            this.availabilityDataGridView_CurrentCellChanged(availabilityDataGridView, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Обновление данных из источника
            this.availabilityTableAdapter.Fill(dbDataSet.availability);
            //Обновление состояния навигатора
            this.availabilityDataGridView_CurrentCellChanged(availabilityDataGridView, e);
        }

        private void availabilityDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (availabilityDataGridView.Columns[e.ColumnIndex].Name.Equals("unit"))
            {
                int prod_code;
                string unit;
                //Получение кода товара в таблице availability
                prod_code = Convert.ToInt32(availabilityDataGridView.Rows[e.RowIndex].Cells["prod_code"].Value);
                //Поиск единицы измерения в таблице shipment по коду товара
                unit = this.dbDataSet.shipment.FindByprod_code(prod_code).unit;
                availabilityDataGridView.Rows[e.RowIndex].Cells["unit"].Value = unit;
            }
            */
        }

        private void btnSProc_Click(object sender, EventArgs e)
        {
            try
            {
                String product = availabilityDataGridView.CurrentRow.Cells["product"].Value.ToString();
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = Properties.Settings.Default.dbConnectionString;
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT count(prod_code) FROM availability WHERE prod_code = @prod_code AND last_operation > @dateFrom AND last_operation < @dateTo";

                cmd.Parameters.Add(new MySqlParameter("@prod_code", prod_TextBox.Text));
                cmd.Parameters.Add(new MySqlParameter("@dateFrom", dateFrom.Value));
                cmd.Parameters.Add(new MySqlParameter("@dateTo", dateTo.Value));

                con.Open();
                label1.Text = "Количество товаров в данном промежутке - " + cmd.ExecuteScalar();
                con.Close();

            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void task1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void task2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void task3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }
    }
}
