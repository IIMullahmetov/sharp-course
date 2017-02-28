namespace SemestrovkaDB
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.aQuantity = new System.Windows.Forms.TextBox();
            this.sQuantity = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.aQuantity2 = new System.Windows.Forms.TextBox();
            this.sQuantity2 = new System.Windows.Forms.TextBox();
            this.dbDataSet = new SemestrovkaDB.dbDataSet();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.task31TableAdapter1 = new SemestrovkaDB.dbDataSetTableAdapters.Task31TableAdapter();
            this.warnumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.availabilityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.availabilityTableAdapter = new SemestrovkaDB.dbDataSetTableAdapters.availabilityTableAdapter();
            this.shipmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shipmentTableAdapter = new SemestrovkaDB.dbDataSetTableAdapters.shipmentTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.availabilityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shipmentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(260, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // aQuantity
            // 
            this.aQuantity.Location = new System.Drawing.Point(13, 13);
            this.aQuantity.Name = "aQuantity";
            this.aQuantity.Size = new System.Drawing.Size(128, 20);
            this.aQuantity.TabIndex = 1;
            // 
            // sQuantity
            // 
            this.sQuantity.Location = new System.Drawing.Point(147, 13);
            this.sQuantity.Name = "sQuantity";
            this.sQuantity.Size = new System.Drawing.Size(125, 20);
            this.sQuantity.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 195);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(260, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Запуск SQL";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.FillGridByReader);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.warnumberDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.shipmentBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(12, 268);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(260, 150);
            this.dataGridView2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 424);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(259, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Запуск Record";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Record);
            // 
            // aQuantity2
            // 
            this.aQuantity2.Location = new System.Drawing.Point(12, 242);
            this.aQuantity2.Name = "aQuantity2";
            this.aQuantity2.Size = new System.Drawing.Size(129, 20);
            this.aQuantity2.TabIndex = 6;
            // 
            // sQuantity2
            // 
            this.sQuantity2.Location = new System.Drawing.Point(147, 242);
            this.sQuantity2.Name = "sQuantity2";
            this.sQuantity2.Size = new System.Drawing.Size(125, 20);
            this.sQuantity2.TabIndex = 7;
            // 
            // dbDataSet
            // 
            this.dbDataSet.DataSetName = "dbDataSet";
            this.dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "Task31";
            this.bindingSource1.DataSource = this.dbDataSet;
            // 
            // task31TableAdapter1
            // 
            this.task31TableAdapter1.ClearBeforeFill = true;
            // 
            // warnumberDataGridViewTextBoxColumn
            // 
            this.warnumberDataGridViewTextBoxColumn.DataPropertyName = "war_number";
            this.warnumberDataGridViewTextBoxColumn.HeaderText = "war_number";
            this.warnumberDataGridViewTextBoxColumn.Name = "warnumberDataGridViewTextBoxColumn";
            // 
            // availabilityBindingSource
            // 
            this.availabilityBindingSource.DataMember = "availability";
            this.availabilityBindingSource.DataSource = this.dbDataSet;
            // 
            // availabilityTableAdapter
            // 
            this.availabilityTableAdapter.ClearBeforeFill = true;
            // 
            // shipmentBindingSource
            // 
            this.shipmentBindingSource.DataMember = "shipment";
            this.shipmentBindingSource.DataSource = this.dbDataSet;
            // 
            // shipmentTableAdapter
            // 
            this.shipmentTableAdapter.ClearBeforeFill = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 451);
            this.Controls.Add(this.sQuantity2);
            this.Controls.Add(this.aQuantity2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.sQuantity);
            this.Controls.Add(this.aQuantity);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.availabilityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shipmentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox aQuantity;
        private System.Windows.Forms.TextBox sQuantity;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox aQuantity2;
        private System.Windows.Forms.TextBox sQuantity2;
        private dbDataSet dbDataSet;
        private System.Windows.Forms.BindingSource bindingSource1;
        private dbDataSetTableAdapters.Task31TableAdapter task31TableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn warnumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource availabilityBindingSource;
        private dbDataSetTableAdapters.availabilityTableAdapter availabilityTableAdapter;
        private System.Windows.Forms.BindingSource shipmentBindingSource;
        private dbDataSetTableAdapters.shipmentTableAdapter shipmentTableAdapter;
    }
}