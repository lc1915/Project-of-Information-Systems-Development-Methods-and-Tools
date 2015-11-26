using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace csharp_project
{
    public partial class OurProject : Form
    {
        SqlConnection sqlConn;
        DataTable dataTab;
        DataSet ds = new DataSet();

        String databaseConn = "Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True";
        String sql = "select * from OrderPrimary";

        public OurProject()
        {
            InitializeComponent();
        }

        private void OurProject_Load(object sender, EventArgs e)
        {
            sqlConn = new SqlConnection(databaseConn);
            try
            {
                //将数据库中的数据绑定到DataGridView控件
                SqlDataAdapter sqlAdap = new SqlDataAdapter(sql, sqlConn); //创建数据适配器对象
                DataSet ds = new DataSet(); //创建数据集对象
                sqlAdap.Fill(ds); //填充数据集

                //dataGridView1.DataSource = ds.Tables[0]; //绑定到数据表
                ds.Dispose(); //释放资源

                //对数据库执行sql语句
                sqlConn.Open();
                //TODO: 执行sql语句

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //列宽设为fill

                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.DataSource = ds.Tables[0];
                string[] rows = new string[ds.Tables[0].Rows.Count + 1];
                for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rows[i] = ds.Tables[0].Rows[i][0] + "";
                }
                cmb.DataSource = rows;
                dataGridView1.Columns.Add(cmb);
                dataGridView1.Columns[0].HeaderCell.Value = "编号";

                DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(column1);
                dataGridView1.Columns[1].HeaderCell.Value = "哈哈哈";

                DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(column2);
                dataGridView1.Columns[2].HeaderCell.Value = "哈哈哈";

                DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
                dataGridView1.Columns.Add(column3);
                dataGridView1.Columns[3].HeaderCell.Value = "哈哈哈";


                //改变datagridview标题的文字
                /*dataGridView1.Columns[0].HeaderCell.Value = "点菜单编号";
                dataGridView1.Columns[1].HeaderCell.Value = "门店编号";
                dataGridView1.Columns[2].HeaderCell.Value = "时间";
                dataGridView1.Columns[3].HeaderCell.Value = "桌号";
                dataGridView1.Columns[4].HeaderCell.Value = "服务员编号";
                dataGridView1.Columns[5].HeaderCell.Value = "总价";
                dataGridView1.Columns[6].HeaderCell.Value = "备注";*/

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            finally
            {
                sqlConn.Close();
            }
        }

    }
}
