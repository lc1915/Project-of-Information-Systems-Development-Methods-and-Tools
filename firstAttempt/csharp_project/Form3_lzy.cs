using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace csharp_project
{
    public partial class Form3_lzy : Form
    {
        DBOper odb = new DBOper();
        private string strSql = "";
        SqlConnection conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
        SqlDataAdapter dapt1;
        SqlDataAdapter dapt2;
        BindingSource bsTitle;
        NewComboBox mycombox = null;
        DataTable result = new DataTable();   //数据集

        //声明一个全局的DataSet
        DataSet ds = new DataSet();
        //声明一个全局的SqlDataAdapter
        SqlDataAdapter da = new SqlDataAdapter();
        //声明一个全局的SQLParameter
        SqlParameter param = new SqlParameter();

        string sql1;
        string sql2;


        public Form3_lzy()
        {
            InitializeComponent();
            mycombox = new NewComboBox(this.dataGridView1);
            this.dataGridView1.DataError += delegate(object sender, DataGridViewDataErrorEventArgs e) { };
        }

        private void Form3_lzy_Load(object sender, EventArgs e)
        {
            SqlConnection connectionToDatabase = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
           // SqlConnection connectionToDatabase = new SqlConnection(@"server=YAO-PC;user id=nanan;password=123;database=publish");
            string sqlSentence = "select *,title_id as ID from dbo.Table_1";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);

            adapter.Fill(ds);
            connectionToDatabase.Close();
            bsTitle = new BindingSource();
            bsTitle.DataSource = ds.Tables[0];
            this.dataGridView1.DataSource = bsTitle;
            mycombox.DropDownStyle = ComboBoxStyle.DropDown;
            mycombox.FlatStyle = FlatStyle.Standard;

            //this.dataGridView1.AllowUserToAddRows = false;
            //this.mycombox.DrawItem +=
            //    new DrawItemEventHandler(ComboBox1_DrawItem);
            //this.mycombox.MeasureItem +=
            //   new MeasureItemEventHandler(ComboBox1_MeasureItem);
            this.mycombox.TextChanged += mycombox_TextChanged;
            this.dataGridView1.Controls.Add(mycombox);

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
            this.dataGridView1.Columns[7].Visible = false;
            for (int m = 0; m < dataGridView1.Columns.Count; m++)
            {
                dataGridView1.Columns[m].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        void mycombox_TextChanged(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell.Value = ((NewComboBox)sender).Text;
            FindValue(((NewComboBox)sender).Text);
        }


        string strChongfu = "";
        private void FindValue(string strKey)
        {

            SqlConnection connectionToDatabase = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
            string sqlSentence = "select * from dbo.Table_2 where title_id='" + strKey.Replace("\t", "") + "'";
            //应该要用上面那行
            //string sqlSentence = "select * from Table_2";
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
                dataGridView1.CurrentRow.Cells[7].Value = result.Rows[0]["title_id"].ToString();
            }
            else 
            {
                connectionToDatabase = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
                //connectionToDatabase = new SqlConnection(@"server=YAO-PC;user id=nanan;password=123;database=publish");
                sqlSentence = "select * from dbo.Table_2 where title_id='" + strKey.Replace("\t", "") + "'";
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
                    dataGridView1.CurrentRow.Cells[7].Value = result2.Rows[0]["title_id"].ToString();
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

        //判断是否是数字
        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                {
                    continue;
                }
                if (!Char.IsNumber(str, i))
                    return false;
            }
            if (str.Length == 0)
            {
                return false;
            }
            return true;
        }


        private DataTable CreateDataTable(String s)
        {
            sql2 = "select * from dbo.Table_2 where title_id='" + s + "'";
            dapt2 = new SqlDataAdapter(sql2, conn);
            dapt2.Fill(ds, "t2");
            DataTable dtable = new DataTable();
            dtable.Columns[0].ColumnName = "作者编号";
            dtable.Columns[1].ColumnName = "书籍编号";
            dtable.Columns[2].ColumnName = "作者销售量";

            dtable.Columns.Add("au_id", typeof(System.String));    //创建下拉框中表格的显示字段
            dtable.Columns.Add("title_id", typeof(System.String));
            dtable.Columns.Add("au_ord", typeof(System.String));

            dtable = ds.Tables["t2"];
            return dtable;
        }



        //返回登陆主页
        private void gzxButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Welcome frm = new Welcome();
            //frm.Show();
        }


        //批量添加多条数据功能
        private void button4_Click(object sender, EventArgs e)
        {
            /*
            List<string> oSQLList = new List<string>();
            oSQLList.Add("delete from dbo.newtitles ");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //string strTitleID = dataGridView1.Rows[i].Cells[7].Value.ToString();

                //DataTable dtCunzai = odb.ExecuteSQL("select * from newtitles where title_id='" + strTitleID + "' ");
                //if (dtCunzai != null && dtCunzai.Rows.Count == 1)
                //{
                //   oSQLList.Add("update dbo.newtitles set title='" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "',type='" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "',price=" + float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + ",ytd_sales=" + int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) + ",notes='" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "',pubdate='" + DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + "' where title_id='" + strTitleID + "'");
                //    // MessageBox.Show("第" + (i + 1).ToString() + "行相同ID信息的图书已经存在，将做更细操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                    oSQLList.Add("insert into dbo.newtitles(title_id,title,type,price,ytd_sales,notes,pubdate)values('" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "'," + float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) + "," + int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) + ",'" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) + "')");
               // }

            }

            bool blResult = odb.BatchExecute(oSQLList);
            if (blResult)
            {
                MessageBox.Show("添加信息保存成功！", "提示");
              SqlConnection connectionToDatabase = new SqlConnection(@"server=2012-20140123sp\sqlexpress;user id=sa;password=zr199406;database=publish");
               // SqlConnection connectionToDatabase = new SqlConnection(@"server=YAO-PC;user id=nanan;password=123;database=publish");
                string sqlSentence = "select *,title_id as ID from dbo.newtitles";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);
                DataTable result = new DataTable();   
                adapter.Fill(result);
                connectionToDatabase.Close();
                bsTitle = new BindingSource();
                bsTitle.DataSource = result;
                this.dataGridView1.DataSource = bsTitle;
                this.dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                MessageBox.Show("添加信息保存失败，请检查！", "提示");
            }*/


        }


        //删除当前行（类似Excel）
        private void button5_Click(object sender, EventArgs e)
        {
            /*List<string> oSQLList = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                oSQLList.Add("delete from dbo.newtitles where title_id='" + this.dataGridView1.SelectedRows[0].Cells[7].Value.ToString() + "'");
            }

            bool blResult = odb.BatchExecute(oSQLList);
            if (blResult)
            {
                MessageBox.Show("删除信息成功！", "提示");
           SqlConnection connectionToDatabase = new SqlConnection(@"server=2012-20140123sp\sqlexpress;user id=sa;password=zr199406;database=publish");
              //  SqlConnection connectionToDatabase = new SqlConnection(@"server=YAO-PC;user id=nanan;password=123;database=publish");
                string sqlSentence = "select *,title_id as ID from dbo.newtitles";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);
                DataTable result = new DataTable();   //直接使用datatable
                adapter.Fill(result);
                connectionToDatabase.Close();
                bsTitle = new BindingSource();
                bsTitle.DataSource = result;
                this.dataGridView1.DataSource = bsTitle;
                this.dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                MessageBox.Show("删除信息失败，请检查！", "提示");
            }*/
        }

        //处理dataGridView1的CellEnter事件，点击title_id列时，根据title_id的值搜索数据库呈现combobox的下拉框
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("=========> dataGridView_cellEnter");
            try
            {
                //判断当前列是否为title_id列，不是就不处理
                if (this.dataGridView1.CurrentCell.ColumnIndex == this.dataGridView1.Columns["title_id"].Index && this.dataGridView1.CurrentCell.RowIndex != this.dataGridView1.Rows.Count)
                {
                    Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex, false);
                    string strCell = this.dataGridView1.CurrentCell.Value.ToString();
                    this.mycombox.valueText = strCell.Trim();
                    this.mycombox.InitItems();
                    this.mycombox.Text = strCell.Trim();
                    this.mycombox.Left = rect.Left;
                    this.mycombox.Width = rect.Width;
                    this.mycombox.Height = 1000;
                    this.mycombox.Top = rect.Top;
                    this.mycombox.Visible = true;
                    this.mycombox.Focus();
                }
            }
            catch
            {
                MessageBox.Show("错误！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


      /*  protected void onSelectionChangeCommitted(object sender, EventArgs e)
        {
            string id = this.mycombox.SelectedText;
            DataRow[] dr = this.mycombox.pubtable.Select("title_id='" + id + "'"); 
            foreach (DataRow d in dr)
            {
                
                //  MessageBox.Show(id);
            }
            this.dataGridView1.CurrentCell.Value = id;
        }*/

        //处理dataGridView1的CellValueChanged事件，当点击backspace按钮时触发事件
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("==========> dataGridView_cellValueChanged");
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1 && this.dataGridView1.CurrentRow.Cells[e.ColumnIndex] != null)
            {
                try
                {
                    /*dataGridView1.BeginEdit(true);
                    SendKeys.Send(" ");               
                    //“System.StackOverflowException”类型的未经处理的异常在 mscorlib.dll 中发生
                   SendKeys.Send("{BKSPT}");
                    dataGridView1.EndEdit(); */
                    //EndEdit(); 写在最后输入没问题了，但是添加不了新行
 
                   /* dataGridView1.BeginEdit(true);
                    SendKeys.Send(" ");
                    dataGridView1.EndEdit();
                    SendKeys.Send("{BKSP}");       */
                    dataGridView1.BeginEdit(true);
                    SendKeys.Send(" ");
                    //dataGridView1.EndEdit();
                    SendKeys.Send("{BKSP}");

                }
                catch { }
            }
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 && e.RowIndex == dataGridView1.Rows.Count - 1 && this.dataGridView1.CurrentRow.Cells[e.ColumnIndex] != null)
            {
                try
                {
                    /*dataGridView1.BeginEdit(true);
                    SendKeys.Send(" ");               
                    //“System.StackOverflowException”类型的未经处理的异常在 mscorlib.dll 中发生
                   SendKeys.Send("{BKSPT}");
                    dataGridView1.EndEdit(); */
                    //EndEdit(); 写在最后输入没问题了，但是添加不了新行

                    /* dataGridView1.BeginEdit(true);
                     SendKeys.Send(" ");
                     dataGridView1.EndEdit();
                     SendKeys.Send("{BKSP}");       */
                    dataGridView1.BeginEdit(true);
                    SendKeys.Send(" ");
                    dataGridView1.EndEdit();
                    SendKeys.Send("{BKSP}");

                }
                catch { }
            }
          
          /* else if (e.ColumnIndex ==7 && e.RowIndex >= 0 && e.RowIndex <= dataGridView1.Rows.Count - 1 && this.dataGridView1.CurrentRow.Cells[e.ColumnIndex] != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    strChongfu += "$" + dataGridView1.Rows[i].Cells[7].Value + "$";
                }
                if (dataGridView1.CurrentCell.Value.ToString().Trim() != "" && strChongfu.IndexOf("$" + dataGridView1.CurrentCell.Value.ToString() + "$") >= 0)
                {
                    MessageBox.Show("相同编号的信息在列表中已经存在！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   // return;
                }
            }*/
        }


        //撤销修改
        private void button6_Click_1(object sender, EventArgs e)
        {
            /*try
            {
                ds.Tables[0].RejectChanges();
           
                SqlConnection connectionToDatabase = new SqlConnection(@"server=2012-20140123sp\sqlexpress;user id=sa;password=zr199406;database=publish");
                //SqlConnection connectionToDatabase = new SqlConnection(@"server=YAO-PC;user id=nanan;password=123;database=publish");
                string sqlSentence = "select *,title_id as ID from dbo.newtitles";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlSentence, connectionToDatabase);
                DataTable result = new DataTable();
                adapter.Fill(result);
                connectionToDatabase.Close();
                bsTitle = new BindingSource();
                bsTitle.DataSource = result;
                this.dataGridView1.DataSource = bsTitle;
                this.dataGridView1.Columns[7].Visible = false;
                MessageBox.Show("撤销操作成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("回滚失败！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
        }




    }
}
