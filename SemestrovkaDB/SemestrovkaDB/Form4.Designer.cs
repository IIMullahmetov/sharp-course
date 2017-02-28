namespace SemestrovkaDB
{
    partial class Form4
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.task21BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet = new SemestrovkaDB.dbDataSet();
            this.availabilityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.storageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.storageTableAdapter = new SemestrovkaDB.dbDataSetTableAdapters.storageTableAdapter();
            this.availabilityTableAdapter = new SemestrovkaDB.dbDataSetTableAdapters.availabilityTableAdapter();
            this.task21TableAdapter = new SemestrovkaDB.dbDataSetTableAdapters.Task21TableAdapter();
            this.task21BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.warnumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsibleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.task21BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.availabilityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.task21BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 14);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(345, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(345, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Запуск SQL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.FillGridByReader);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(345, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Запуск Record";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Record);
            // 
            // task21BindingSource
            // 
            this.task21BindingSource.DataMember = "Task21";
            this.task21BindingSource.DataSource = this.dbDataSet;
            // 
            // dbDataSet
            // 
            this.dbDataSet.DataSetName = "dbDataSet";
            this.dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // availabilityBindingSource
            // 
            this.availabilityBindingSource.DataMember = "availability";
            this.availabilityBindingSource.DataSource = this.dbDataSet;
            // 
            // storageBindingSource
            // 
            this.storageBindingSource.DataMember = "storage";
            this.storageBindingSource.DataSource = this.dbDataSet;
            // 
            // storageTableAdapter
            // 
            this.storageTableAdapter.ClearBeforeFill = true;
            // 
            // availabilityTableAdapter
            // 
            this.availabilityTableAdapter.ClearBeforeFill = true;
            // 
            // task21TableAdapter
            // 
            this.task21TableAdapter.ClearBeforeFill = true;
            // 
            // task21BindingSource1
            // 
            this.task21BindingSource1.DataMember = "Task21";
            this.task21BindingSource1.DataSource = this.dbDataSet;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.warnumberDataGridViewTextBoxColumn,
            this.responsibleDataGridViewTextBoxColumn,
            this.countDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.task21BindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(13, 217);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(345, 150);
            this.dataGridView2.TabIndex = 4;
            // 
            // warnumberDataGridViewTextBoxColumn
            // 
            this.warnumberDataGridViewTextBoxColumn.DataPropertyName = "war_number";
            this.warnumberDataGridViewTextBoxColumn.HeaderText = "war_number";
            this.warnumberDataGridViewTextBoxColumn.Name = "warnumberDataGridViewTextBoxColumn";
            // 
            // responsibleDataGridViewTextBoxColumn
            // 
            this.responsibleDataGridViewTextBoxColumn.DataPropertyName = "responsible";
            this.responsibleDataGridViewTextBoxColumn.HeaderText = "responsible";
            this.responsibleDataGridViewTextBoxColumn.Name = "responsibleDataGridViewTextBoxColumn";
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "count";
            this.countDataGridViewTextBoxColumn.HeaderText = "count";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 417);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.task21BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.availabilityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.task21BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private dbDataSet dbDataSet;
        private System.Windows.Forms.BindingSource storageBindingSource;
        private dbDataSetTableAdapters.storageTableAdapter storageTableAdapter;
        private System.Windows.Forms.BindingSource availabilityBindingSource;
        private dbDataSetTableAdapters.availabilityTableAdapter availabilityTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource task21BindingSource;
        private dbDataSetTableAdapters.Task21TableAdapter task21TableAdapter;
        private System.Windows.Forms.BindingSource task21BindingSource1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn warnumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsibleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
    }
}