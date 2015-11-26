namespace csharp_project
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.kDBSDataSet = new csharp_project.KDBSDataSet();
            this.emercyOrderDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.emercyOrderDetailTableAdapter = new csharp_project.KDBSDataSetTableAdapters.EmercyOrderDetailTableAdapter();
            this.emercyOrderPrimaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.emercyOrderPrimaryTableAdapter = new csharp_project.KDBSDataSetTableAdapters.EmercyOrderPrimaryTableAdapter();
            this.justForTestBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.justForTestTableAdapter = new csharp_project.KDBSDataSetTableAdapters.JustForTestTableAdapter();
            this.number = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.materialIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kDBSDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emercyOrderDetailBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emercyOrderPrimaryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.justForTestBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.name,
            this.date,
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn,
            this.materialIDDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn,
            this.unitDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.emercyOrderDetailBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(787, 449);
            this.dataGridView1.TabIndex = 0;
            // 
            // kDBSDataSet
            // 
            this.kDBSDataSet.DataSetName = "KDBSDataSet";
            this.kDBSDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // emercyOrderDetailBindingSource
            // 
            this.emercyOrderDetailBindingSource.DataMember = "EmercyOrderDetail";
            this.emercyOrderDetailBindingSource.DataSource = this.kDBSDataSet;
            // 
            // emercyOrderDetailTableAdapter
            // 
            this.emercyOrderDetailTableAdapter.ClearBeforeFill = true;
            // 
            // emercyOrderPrimaryBindingSource
            // 
            this.emercyOrderPrimaryBindingSource.DataMember = "EmercyOrderPrimary";
            this.emercyOrderPrimaryBindingSource.DataSource = this.kDBSDataSet;
            // 
            // emercyOrderPrimaryTableAdapter
            // 
            this.emercyOrderPrimaryTableAdapter.ClearBeforeFill = true;
            // 
            // justForTestBindingSource
            // 
            this.justForTestBindingSource.DataMember = "JustForTest";
            this.justForTestBindingSource.DataSource = this.kDBSDataSet;
            // 
            // justForTestTableAdapter
            // 
            this.justForTestTableAdapter.ClearBeforeFill = true;
            // 
            // number
            // 
            this.number.HeaderText = "编号";
            this.number.Name = "number";
            this.number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // name
            // 
            this.name.HeaderText = "航班名";
            this.name.Name = "name";
            // 
            // date
            // 
            this.date.HeaderText = "日期";
            this.date.Name = "date";
            // 
            // emercyOrderPrimaryIDDataGridViewComboBoxColumn
            // 
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.DataPropertyName = "EmercyOrderPrimaryID";
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.DataSource = this.justForTestBindingSource;
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.HeaderText = "EmercyOrderPrimaryID";
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.Name = "emercyOrderPrimaryIDDataGridViewComboBoxColumn";
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.emercyOrderPrimaryIDDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // materialIDDataGridViewTextBoxColumn
            // 
            this.materialIDDataGridViewTextBoxColumn.DataPropertyName = "MaterialID";
            this.materialIDDataGridViewTextBoxColumn.HeaderText = "MaterialID";
            this.materialIDDataGridViewTextBoxColumn.Name = "materialIDDataGridViewTextBoxColumn";
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            this.numberDataGridViewTextBoxColumn.HeaderText = "Number";
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            // 
            // unitDataGridViewTextBoxColumn
            // 
            this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            this.unitDataGridViewTextBoxColumn.HeaderText = "Unit";
            this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 473);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kDBSDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emercyOrderDetailBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emercyOrderPrimaryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.justForTestBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private KDBSDataSet kDBSDataSet;
        private System.Windows.Forms.BindingSource emercyOrderDetailBindingSource;
        private KDBSDataSetTableAdapters.EmercyOrderDetailTableAdapter emercyOrderDetailTableAdapter;
        private System.Windows.Forms.BindingSource emercyOrderPrimaryBindingSource;
        private KDBSDataSetTableAdapters.EmercyOrderPrimaryTableAdapter emercyOrderPrimaryTableAdapter;
        private System.Windows.Forms.BindingSource justForTestBindingSource;
        private KDBSDataSetTableAdapters.JustForTestTableAdapter justForTestTableAdapter;
        private System.Windows.Forms.DataGridViewComboBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewComboBoxColumn emercyOrderPrimaryIDDataGridViewComboBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn materialIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
    }
}

