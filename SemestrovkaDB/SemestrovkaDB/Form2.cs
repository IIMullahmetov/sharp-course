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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.shipment". При необходимости она может быть перемещена или удалена.
            this.shipmentTableAdapter.Fill(this.dbDataSet.shipment);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dbDataSet.availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.storage". При необходимости она может быть перемещена или удалена.
            this.storageTableAdapter.Fill(this.dbDataSet.storage);

            dataGridView1.AutoGenerateColumns = true;
        }

        private void btnStorage_Click(object sender, EventArgs e)
        {
            if (lblTableName.Text != "Storage")
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = storageBindingSource;
                bindingNavigator1.BindingSource = storageBindingSource;
                lblTableName.Text = "Storage";
            }
        }

        private void btnAvailability_Click(object sender, EventArgs e)
        {
            if (lblTableName.Text != "Availability")
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = availabilityBindingSource;
                bindingNavigator1.BindingSource = availabilityBindingSource;
                lblTableName.Text = "Availability";
            }
        }

        private void btnShipment_Click(object sender, EventArgs e)
        {
            if (lblTableName.Text != "Shipment")
            {
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = shipmentBindingSource;
                bindingNavigator1.BindingSource = shipmentBindingSource;
                lblTableName.Text = "Shipment";
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            tableAdapterManager1.UpdateAll(dbDataSet);
            MessageBox.Show("Изменения сохранены");
        }
    }
}
