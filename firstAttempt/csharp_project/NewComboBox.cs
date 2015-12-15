using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace csharp_project
{
    public partial class NewComboBox : ComboBox
    {
        public int columnPadding = 5;
        private DataGridView DGV;
        public float[] columnWidths = { 150, 300, 100, 100, 100, 200, 100 };  //项宽度
        public String[] columnNames = { "ID", "书籍名称", "所属种类", "发行价格", "销售总量", "备 注", "出版日期" }; //项名称
        public int valueMemberColumnIndex = 0;       //valueMember属性列所在的索引
        private DataTable dataTable = new DataTable("Student");
        public string valueText = "";
        public DataTable pubtable = new DataTable();
        public NewComboBox(DataGridView dgv)
        {
            this.DGV = dgv;
            InitializeComponent();
            this.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.DrawMode = DrawMode.OwnerDrawVariable;//手动绘制所有元素
            this.DropDownHeight = 350;   //自己设置下拉框最长到多长显示滚动条
            this.IntegralHeight = false;
            this.ItemHeight = 23;
            InitItems();
        }
        public NewComboBox(string valuetext)
        {
            this.valueText = valuetext;
            InitializeComponent();
            this.DropDownHeight =350;   //自己设置下拉框最长到多长显示滚动条
            this.IntegralHeight = false;
            this.ItemHeight = 23;

            InitItems();
        }
       //重写选中事件，不处理标题
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndex != 0)
            {
                this.Text = this.SelectedText.ToString().Replace("ID", "");
            }
            else
            {
                this.Text = "";
               // this.valueText = "";

            }
        }
        //重写combobox的点击事件，点击后将单元格的值置空
        protected override void OnClick(EventArgs e)
        {
            try
            {
                //之前这两行有用
                //this.valueText = "";
                //this.Text = "";
            }
            catch { }
        }


        //根据传入的Title_id查询对应的书籍信息，构造DataTable对象
        private DataTable CreateDataTable(string s)
        {
            SqlConnection conn = new SqlConnection("Data Source=A\\B;Initial Catalog=KDBS;Integrated Security=True");
            SqlDataAdapter dapt2;
            DataSet ds = new DataSet();
            string sql2;
            sql2 = "select title_id as ID,title as 书籍名称,type as 所属种类,price as 发行价格,ytd_sales as 销售总量,notes as \"备 注\",pubdate as 出版日期  from dbo.Table_2 where title_id like '" + s.Replace("'", "''") + "%'";
            if (s == null || s == "")
            {
                sql2 = "select title_id as ID,title as 书籍名称,type as 所属种类,price as 发行价格,ytd_sales as 销售总量,notes as \"备 注\",pubdate as 出版日期 from dbo.Table_2"; 
            }

            dapt2 = new SqlDataAdapter(sql2, conn);
            dapt2.Fill(ds, "table1");
            DataTable dtable = new DataTable();
            dtable.Columns.Add("ID", typeof(System.String));
            dtable.Columns.Add("书籍名称", typeof(System.String));
            dtable.Columns.Add("所属种类", typeof(System.String));
            dtable.Columns.Add("发行价格", typeof(System.String));
            dtable.Columns.Add("销售总量", typeof(System.String));
            dtable.Columns.Add("备 注", typeof(System.String));
            dtable.Columns.Add("出版日期", typeof(System.String));
            dtable = ds.Tables["table1"];
            return dtable;
        }
        //获取相应的数据table

        //初始化相应的数据和属性
        public void InitItems()
        {
            dataTable = CreateDataTable(this.valueText);
            pubtable = dataTable;
            this.DataSource = dataTable;
            this.DisplayMember = "ID";
            this.ValueMember = "ID";

            if ((this.DataSource != null) && (!string.IsNullOrEmpty(this.DisplayMember)))
            {

                if (!string.IsNullOrEmpty(this.Text))
                {

                    DataView dv = dataTable.DefaultView;
                    dv.RowFilter = string.Format("ID like '{0}%'", this.valueText);
                  
                    DataTable NewDt = dv.ToTable("newTableName");
                    DataTable copyTable = new DataTable();
                    copyTable.Columns.Add("ID", Type.GetType("System.String"));
                    copyTable.Columns.Add("书籍名称", Type.GetType("System.String"));
                    copyTable.Columns.Add("所属种类", Type.GetType("System.String"));
                    copyTable.Columns.Add("发行价格", Type.GetType("System.String"));
                    copyTable.Columns.Add("销售总量", Type.GetType("System.String"));
                    copyTable.Columns.Add("备 注", Type.GetType("System.String"));
                    copyTable.Columns.Add("出版日期", Type.GetType("System.String"));
                    copyTable.Rows.Add(new object[] { "ID", "书籍名称", "所属种类", "发行价格", "销售总量", "备 注", "出版日期" });

                    for (int i = 0; i < NewDt.Rows.Count; i++)
                    {
                        copyTable.Rows.Add(new object[] { NewDt.Rows[i][0].ToString() + "\t", NewDt.Rows[i][1].ToString().Length > 20 ? NewDt.Rows[i][1].ToString().Substring(0, 20) : NewDt.Rows[i][1].ToString() + "\t", NewDt.Rows[i][2].ToString() + "\t", NewDt.Rows[i][3].ToString() + "\t", NewDt.Rows[i][4].ToString() + "\t", NewDt.Rows[i][5].ToString().Length > 20 ? NewDt.Rows[i][5].ToString().Substring(0, 20) : NewDt.Rows[i][5].ToString() + "\t", NewDt.Rows[i][6].ToString().Length >= 10 ? NewDt.Rows[i][6].ToString().Substring(0, 10) : NewDt.Rows[i][6].ToString() + "\t" });
                    }
                    //copyTable.Rows.Add(new object[] {this.valueText,"","",""});
                    this.DataSource = copyTable;
                    pubtable = copyTable;
                }
                else
                {
                    this.DataSource = dataTable;
                    pubtable = dataTable;
                }
                this.DropDownWidth = (int)CalculateTotalWidth();//计算下拉框的总宽度 
                //this.SelectedText = pubtable.Rows[1][0].ToString();
                if (this.Items.Count > 0)
                {
                    this.SelectedIndex = -1;
                }
            }
        }

        //计算下拉框的总宽度
        private float CalculateTotalWidth()
        {
            columnPadding = 5;
            float totalWidth = 0;
            foreach (int width in columnWidths)
            {
                totalWidth += (width + columnPadding);
            }
            return totalWidth + SystemInformation.VerticalScrollBarWidth;
        }

        //重写绘制方法，自定义绘制item
        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            base.OnDrawItem(e);
            Rectangle boundsRect = e.Bounds;//获取绘制项边界的矩形
            boundsRect.Height = 23;
            e = new DrawItemEventArgs(e.Graphics, e.Font, boundsRect, e.Index, e.State, e.ForeColor, e.BackColor);


            if (DesignMode)
            {
                return;
            }

            //  e.Graphics.FillRectangle(Brushes.White, boundsRect);
            if (e.State == DrawItemState.Selected)
            {
                //this code keeps the last item drawn from having a Bisque background. 
                boundsRect.Y = e.Index * 23;
                e.Graphics.FillRectangle(Brushes.Bisque, boundsRect);
            }
            else 
            {
                boundsRect.Y = e.Index * 23;
                e.Graphics.FillRectangle(Brushes.White, boundsRect);
            }

            //第一行为表头，设置颜色
            if (e.Index == 0)
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(127, 128, 0));  //定义画刷
                    // Rectangle rect = e.Bounds;
                    Rectangle rectColor = new Rectangle(boundsRect.Location, new Size(boundsRect.Width, boundsRect.Height));
                    e.Graphics.FillRectangle(brush, rectColor);   //填充颜色
                }
            }



            int lastRight = 0;
            using (Pen linePen = new Pen(SystemColors.GrayText))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    if (columnNames.Length == 0)
                    {
                        e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush, boundsRect);
                    }
                    else
                    {
                        //循环各列
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[i]));
                            boundsRect.X = lastRight;//列的左边位置
                            boundsRect.Width = (int)columnWidths[i] + columnPadding;//列的宽度//
                            lastRight = boundsRect.Right;
                            //垂直居中与水平居中
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;



                            if (i == valueMemberColumnIndex)//如果是valuemember
                            {
                                boundsRect.Y = e.Index * 23;
                                using (Font font = new Font("宋体", 12))
                                {
                                    //绘制项的内容
                                   e.Graphics.DrawString(item, font, brush, boundsRect, sf);
                                }
                            }
                            else
                            {
                                //绘制项的内容
                                e.Graphics.DrawString(item, new Font("宋体", 12), brush, boundsRect, sf);
                            }

                            //绘制各项间的竖线
                            if (i < columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                            }
                        }
                          //e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);   
                    }

                    //绘制各行间横线
                    int hh = 0;   //起点高度为0
                    using (SolidBrush brush2 = new SolidBrush(ForeColor))
                    {
                        if (this.Items.Count == 0)
                        {
                            e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush2, boundsRect);
                        }
                        else
                        {
                            //循环各列
                            for (int i = 0; i < this.Items.Count; i++)
                            {
                                boundsRect.X = 0;
                                int h = 23;   //第一次绘制的高度
                                hh += h;
                                for (int j = 0; j < columnNames.Length + 1; j++)
                                {
                                    if (i < columnNames.Length - 1)
                                    {
                                        if (i < columnNames.Length - 1)
                                        {
                                            e.Graphics.DrawLine(linePen, boundsRect.Left, hh, (int)CalculateTotalWidth(), hh);
                                        }
                                    }
                                }
                                //e.Graphics.DrawLine(linePen, boundsRect.Left, boundsRect.Bottom, (int)CalculateTotalWidth(), boundsRect.Bottom);
                            }

                        }
                    }
                   e.DrawFocusRectangle();
                }
            }


        }
        protected string strTemp;
        //重写按键点击事件
        protected override void OnKeyUp(KeyEventArgs e)
        {
            strTemp = this.Text;
            this.valueText = this.Text;
            InitItems();
            // 
            this.DroppedDown = true;
            this.Text = strTemp;
            if (this.Text != null && this.Text != string.Empty)
            {
                // 获得光标位置
                this.Text = this.valueText;
                this.SelectionStart = this.Text.Length;
            }

            this.Cursor = Cursors.Default;
            base.OnKeyUp(e);
        }
    }
}
