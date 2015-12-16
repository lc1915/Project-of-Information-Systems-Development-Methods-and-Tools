using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomDataGridViewColumn
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
        DataSet ds;
        DataSet ds2;
        SqlDataAdapter adpt;
        DBOper odb = new DBOper();

        public Form1()
        {
            InitializeComponent();
        }

        // 初始化DataGridView
       private void iniDataGridViewDW()
        {
            DataGridViewColumn column = new DataGridViewDataComboColumn();
            column.Name = "title_id";
            (column as DataGridViewDataComboColumn).SDisplayField = "title_id,title,type,price,ytd_sales,notes,pubdate";
            (column as DataGridViewDataComboColumn).SDisplayMember = "title_id";
            (column as DataGridViewDataComboColumn).SKeyWords = "title_id";
            
            (column as DataGridViewDataComboColumn).DataSource = createTable();
            dataGridView1.Columns.Add(column);     
        }

       //创建下拉框数据源
       private DataTable createTable()
        {
            ds = new DataSet();            
            adpt = new SqlDataAdapter("select * from dbo.Table_2", conn);
            adpt.Fill(ds, "total");
  
            DataTable dt = new DataTable();
            dt = ds.Tables["total"];         
            return dt;
        }


        // 获取在下拉框中选择的数据
        void dataWindow1_AfterSelector(object sender, AfterSelectorEventArgs e)
        {
           ;
            DataGridViewRow row = e.Value as DataGridViewRow;
            DataRowView dataRow = row.DataBoundItem as DataRowView;

            int intRowIndex = this.dataGridView1.CurrentCell.RowIndex;
            this.dataGridView1.Rows[intRowIndex].Cells[0].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[2].Value = dataRow["title"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[3].Value = dataRow["type"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[4].Value = dataRow["price"].ToString().Trim();
            this.dataGridView1.Rows[intRowIndex].Cells[5].Value = dataRow["ytd_sales"];
            this.dataGridView1.Rows[intRowIndex].Cells[6].Value = dataRow["notes"];
            this.dataGridView1.Rows[intRowIndex].Cells[7].Value = dataRow["pubdate"];
        }

        // DataGridView中的下拉框选择事件
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is dataCombobox)
            {
                (e.Control as dataCombobox).AfterSelector -= new AfterSelectorEventHandler(SalePageAddOrEditForm_AfterSelector);
                (e.Control as dataCombobox).AfterSelector += new AfterSelectorEventHandler(SalePageAddOrEditForm_AfterSelector);
            }
        }


        // DataGridView中dataCombo选择后事件处理（获取在下拉框中选择的数据）
        void SalePageAddOrEditForm_AfterSelector(object sender, AfterSelectorEventArgs e)
        {
            DataGridViewRow row = e.Value as DataGridViewRow;
            DataRowView dataRow = row.DataBoundItem as DataRowView;
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataRow["title_id"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = dataRow["title"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[3].Value = dataRow["type"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[4].Value = dataRow["price"].ToString().Trim();
            this.dataGridView1.Rows[e.RowIndex].Cells[5].Value = dataRow["ytd_sales"].ToString().Trim(); ;
            this.dataGridView1.Rows[e.RowIndex].Cells[6].Value = dataRow["notes"].ToString().Trim(); ;
            this.dataGridView1.Rows[e.RowIndex].Cells[7].Value = dataRow["pubdate"].ToString().Trim(); ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            iniDataGridViewDW();
            ds2 = new DataSet();  // 创建数据集          
            adpt = new SqlDataAdapter("select * from dbo.Table_1", conn);
            adpt.Fill(ds2, "titless");
            dataGridView1.DataSource = ds2.Tables["titless"];
            //隐藏
            dataGridView1.Columns[1].Visible = false;
            this.dataGridView1.Columns[0].HeaderCell.Value = "图书编号";
            this.dataGridView1.Columns[2].HeaderCell.Value = "图书名称";
            this.dataGridView1.Columns[3].HeaderCell.Value = "图书种类";
            this.dataGridView1.Columns[4].HeaderCell.Value = "图书价格";
            this.dataGridView1.Columns[5].HeaderCell.Value = "累计销量";
            this.dataGridView1.Columns[6].HeaderCell.Value = "信息备注";
            this.dataGridView1.Columns[7].HeaderCell.Value = "出版日期";

            //使下拉框列显示内容
            for (int i = 0; i < dataGridView1.RowCount;i++ )
            {
                this.dataGridView1.Rows[i].Cells[0].Value  = this.dataGridView1.Rows[i].Cells[1].Value;
            }

        }

       
        //返回主页
        private void gzxButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Welcome frm = new Welcome();
            //frm.Show();
        }

        //保存
        private void button4_Click(object sender, EventArgs e)
        {
            List<string> oSQLList = new List<string>();
            oSQLList.Add("delete from dbo.Table_1 ");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                oSQLList.Add("insert into dbo.Table_1(title_id,title,type,price,ytd_sales,notes,pubdate)values('" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "'," + float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) + "," + int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','" + DateTime.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString()) + "')");
            }

            bool blResult = odb.BatchExecute(oSQLList);
            if (blResult)
            {
                MessageBox.Show("保存成功！", "提示");
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Remove("title_id");  //否则数据显示会出问题
                iniDataGridViewDW();
                DataSet ds3 = new DataSet();  // 创建数据集          
                adpt = new SqlDataAdapter("select * from dbo.Table_1", conn);
                adpt.Fill(ds3, "titless");
                dataGridView1.DataSource = ds3.Tables["titless"];
                //隐藏
                dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[0].HeaderCell.Value = "图书编号";
                this.dataGridView1.Columns[2].HeaderCell.Value = "图书名称";
                this.dataGridView1.Columns[3].HeaderCell.Value = "图书种类";
                this.dataGridView1.Columns[4].HeaderCell.Value = "图书价格";
                this.dataGridView1.Columns[5].HeaderCell.Value = "累计销量";
                this.dataGridView1.Columns[6].HeaderCell.Value = "信息备注";
                this.dataGridView1.Columns[7].HeaderCell.Value = "出版日期";

                //使下拉框列显示内容
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                   this.dataGridView1.Rows[i].Cells[0].Value = this.dataGridView1.Rows[i].Cells[1].Value;
                }
            }
            else
            {
                MessageBox.Show("保存操作失败，请检查！", "提示");
            }

        }

        //删除
        private void button5_Click(object sender, EventArgs e)
        {
            List<string> oSQLList = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                oSQLList.Add("delete from dbo.Table_1 where title_id='" + this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "'");
            }

            bool blResult = odb.BatchExecute(oSQLList);
            //刷新界面
            if (blResult)
            {
                MessageBox.Show("删除信息成功！", "提示");
                ds2 = new DataSet();  // 创建数据集          
                adpt = new SqlDataAdapter("select * from dbo.Table_1", conn);
                adpt.Fill(ds2, "titless");
                dataGridView1.DataSource = ds2.Tables["titless"];
                //隐藏
                dataGridView1.Columns[1].Visible = false;
                this.dataGridView1.Columns[0].HeaderCell.Value = "图书编号";
                this.dataGridView1.Columns[2].HeaderCell.Value = "图书名称";
                this.dataGridView1.Columns[3].HeaderCell.Value = "图书种类";
                this.dataGridView1.Columns[4].HeaderCell.Value = "图书价格";
                this.dataGridView1.Columns[5].HeaderCell.Value = "累计销量";
                this.dataGridView1.Columns[6].HeaderCell.Value = "信息备注";
                this.dataGridView1.Columns[7].HeaderCell.Value = "出版日期";

                //使下拉框列显示内容
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = this.dataGridView1.Rows[i].Cells[1].Value;
                }
            }
            else
            {
                MessageBox.Show("删除信息失败，请检查！", "提示");
            }
        }

        //撤销操作
       private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                ds2.Tables["titless"].RejectChanges();  
            }
            catch
            {
                MessageBox.Show("撤销失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

      
    }
}
