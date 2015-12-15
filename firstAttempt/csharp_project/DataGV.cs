using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mine.FormUtils;

namespace csharp_project
{
    public partial class DataGV : Form
    {
        NewComboBox mycombobox = null;

        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();
        BindingSource bsTitle;

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from OrderPrimary";

        public DataGV()
        {
            InitializeComponent();
            mycombobox = new NewComboBox(this.dataGridView1);
        }

        private void DataGV_Load(object sender, EventArgs e)
        {
            SqlConnection connectionToDatabase = new SqlConnection(databaseConn);
            string sqlSentence = sql;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);

            adapter.Fill(ds);
            connectionToDatabase.Close();
            bsTitle = new BindingSource();
            bsTitle.DataSource = ds.Tables[0];
            this.dataGridView1.DataSource = bsTitle;
            mycombobox.DropDownStyle = ComboBoxStyle.DropDown;
            mycombobox.FlatStyle = FlatStyle.Standard;

            //this.dataGridView1.AllowUserToAddRows = false;
            //this.mycombox.DrawItem +=
            //    new DrawItemEventHandler(ComboBox1_DrawItem);
            //this.mycombox.MeasureItem +=
            //   new MeasureItemEventHandler(ComboBox1_MeasureItem);
            this.mycombobox.TextChanged += mycombobox_TextChanged;
            this.dataGridView1.Controls.Add(mycombobox);

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Chocolate;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 12, FontStyle.Bold);

            this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns[1].HeaderCell.Value = "书籍名称";
            this.dataGridView1.Columns[2].HeaderCell.Value = "所属种类";
            this.dataGridView1.Columns[3].HeaderCell.Value = "发行价格";
            this.dataGridView1.Columns[4].HeaderCell.Value = "销售总量";
            this.dataGridView1.Columns[5].HeaderCell.Value = "备 注";
            this.dataGridView1.Columns[6].HeaderCell.Value = "出版日期";
            for (int m = 0; m < dataGridView1.Columns.Count; m++)
            {
                dataGridView1.Columns[m].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        void mycombobox_TextChanged(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell.Value = ((NewComboBox)sender).Text;
            FindValue(((NewComboBox)sender).Text);
        }

        string strChongfu = "";
        private void FindValue(string strKey)
        {

            SqlConnection connectionToDatabase = new SqlConnection(databaseConn);
            string sqlSentence = sql;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);
            DataTable result = new DataTable();
            adapter.Fill(result);
            connectionToDatabase.Close();
            if (result.Rows.Count > 0)
            {
                dataGridView1.CurrentRow.Cells[0].Value = result.Rows[0]["title_id"].ToString();
                dataGridView1.CurrentRow.Cells[1].Value = result.Rows[0]["title"].ToString();
                dataGridView1.CurrentRow.Cells[2].Value = result.Rows[0]["type"].ToString();
                dataGridView1.CurrentRow.Cells[3].Value = result.Rows[0]["price"].ToString();
                dataGridView1.CurrentRow.Cells[4].Value = result.Rows[0]["ytd_sales"].ToString();
                dataGridView1.CurrentRow.Cells[5].Value = result.Rows[0]["notes"].ToString();
                dataGridView1.CurrentRow.Cells[6].Value = result.Rows[0]["pubdate"].ToString();
            }
            else
            {
                connectionToDatabase = new SqlConnection(databaseConn);
                sqlSentence = sql;
                adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);
                DataTable result2 = new DataTable();
                adapter.Fill(result2);
                connectionToDatabase.Close();
                if (result2.Rows.Count > 0)
                {
                    dataGridView1.CurrentRow.Cells[0].Value = result2.Rows[0]["title_id"].ToString();
                    dataGridView1.CurrentRow.Cells[1].Value = result2.Rows[0]["title"].ToString();
                    dataGridView1.CurrentRow.Cells[2].Value = result2.Rows[0]["type"].ToString();
                    dataGridView1.CurrentRow.Cells[3].Value = result2.Rows[0]["price"].ToString();
                    dataGridView1.CurrentRow.Cells[4].Value = result2.Rows[0]["ytd_sales"].ToString();
                    dataGridView1.CurrentRow.Cells[5].Value = result2.Rows[0]["notes"].ToString();
                    dataGridView1.CurrentRow.Cells[6].Value = result2.Rows[0]["pubdate"].ToString();
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                strChongfu += "$" + dataGridView1.Rows[i].Cells[7].Value + "$";
                if (i != dataGridView1.CurrentRow.Index)
                {

                    dataGridView1.Rows[i].Cells[0].Value = dataGridView1.Rows[i].Cells[7].Value;
                }
            }
        }

        //处理dataGridView1的CellEnter事件，点击title_id列时，根据title_id的值搜索数据库呈现combobox的下拉框
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("=========> dataGridView_cellEnter");
            try
            {
                //判断当前列是否为title_id列，不是就不处理
                //if (this.dataGridView1.CurrentCell.ColumnIndex == this.dataGridView1.Columns["title_id"].Index && this.dataGridView1.CurrentCell.RowIndex != this.dataGridView1.Rows.Count)
                //{
                    Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, false);
                    string strCell = this.dataGridView1.CurrentCell.Value.ToString();
                    this.mycombobox.valueText = strCell.Trim();
                    this.mycombobox.InitItems();
                    this.mycombobox.Text = strCell.Trim();
                    this.mycombobox.Left = rect.Left;
                    this.mycombobox.Width = rect.Width;
                    this.mycombobox.Height = 1000;
                    this.mycombobox.Top = rect.Top;
                    this.mycombobox.Visible = true;
                    this.mycombobox.Focus();
                //}
            }
            catch
            {
                MessageBox.Show("错误！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
