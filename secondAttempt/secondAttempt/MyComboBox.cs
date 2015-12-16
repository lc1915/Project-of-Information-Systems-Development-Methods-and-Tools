using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace CustomDataGridViewColumn
{
    public partial class MyComboBox : ComboBox, IDataGridViewEditingControl
    {
        int columnPadding = 5;
        private float[] columnWidths = new float[0];  //项宽度
        private String[] columnNames = new String[0]; //项名称
        private int valueMemberColumnIndex = 0;       //valueMember属性列所在的索引
       
        private DataTable dataTable = new DataTable("Details");
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        public string valueText = "";
        

        //构造函数,调用InitItems()
        public MyComboBox()
        {
            InitializeComponent();
            this.DropDownHeight = 75;   //自己设置下拉框最长到多长显示滚动条
            this.IntegralHeight = false;
            InitItems();
        }

        #region 继承IDataGridViewEditingControl接口
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text.ToString();
            }
            set
            {
                if (value is String)
                {
                    this.Text = (string)value;
                }
            }
        }

        // 实现IDataGridViewEditingControl接口中GetEditingControlFormattedValue的方法
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

       // 实现IDataGridViewEditingControl接口中ApplyCellStyleToEditingControl的方法
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        // 实现IDataGridViewEditingControl接口中EditingControlRowIndex方法
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // 实现IDataGridViewEditingControl接口中EditingControlWantsInputKey的方法
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // 实现IDataGridViewEditingControl接口中PrepareEditingControlForEdit的属性
        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        
        // 实现IDataGridViewEditingControl接口中RepositionEditingControlOnValueChange的属性
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // 实现IDataGridViewEditingControl接口中EditingControlDataGridView的属性
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // 实现IDataGridViewEditingControl接口中EditingControlValueChanged的属性
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // 实现IDataGridViewEditingControl接口中EditingPanelCursor的属性
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        // 实现IDataGridViewEditingControl接口中OnTextChanged的方法
        protected override void OnTextChanged(EventArgs eventargs)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(eventargs);
        }  

////////////**********************************************************************************************////
        private DataTable dtfliter = new DataTable();
        // 特殊出标志
        private bool flag = false;     
       // private DataTable mdataSource = new DataTable();
        /// <summary>
        /// combobox 输入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)  
        {
            if (this.Text == string.Empty)
            {
                dataTable=CreateDataTable("");
                this.Text = string.Empty;
                return;
            }
            flag = false;
            string str = this.Text;
            // 根据输入的值自带筛选
            bool validate = Fillter(str);

            // 现实下拉框
            this.DroppedDown = true;
            // 输入无效的数据
            if (!validate)
            {
                MessageBox.Show("输入的数据无效,请重新输入");
            }
            else
            {
                this.Text = str;
                if (this.Text != null && this.Text != string.Empty)
                {
                    // 获得光标位置
                    this.SelectionStart =- this.valueText.Length;
                }

                }
            // 现实鼠标
            this.Cursor = Cursors.Default;
        }




        /// <summary>
        /// 筛选匹配的值
        /// </summary>
        /// <param name="str"></param>
        private bool Fillter(string str)
        {
            bool isvalida = true;
            DataTable dt = dataTable.Clone();
            // 循环筛选匹配的值      
            foreach (DataRow dr in dataTable.Rows)
            {
                int i = -1;
                i = dr["title_id"].ToString().IndexOf(str.ToString());
                if (i > -1)
                {
                    DataRow newrow = dt.NewRow();
                    newrow.ItemArray = dr.ItemArray;
                    // 构造新的数据
                    dt.Rows.Add(newrow);
                }

            }
            dt.AcceptChanges();
            InitItems();
            //if (flag  && dt.Rows.Count > 1)
            //{
            //    this.comboBox1.Text = str;
            //    return;
            //}
            // 当为一条数据要特殊处理/不然光标返回有问题
            if (dt.Rows.Count == 1)
            {
                // 特殊处理标志
                flag = true;
                this.Text = str;// str参数为输入的值; 
                int len = str.Length;
                // Fillter(str.Substring(0, len - 1));
            }

            // 当数据源中没有输入的值提示, 并且截断字符重新筛选
            if (dt.Rows.Count == 0)
            {
                if (str.Length > 0)
                {
                    int i = str.Length - 1;
                    Fillter(str.Substring(0, i));
                    isvalida = false;
                }

            }
            return isvalida;
        }




        /// <summary>
        /// 自动调整宽度
        /// </summary>
        /// <param name="sender"></param>
        public void AutoSizeComboBoxItem(object sender)
        {
            Graphics g = null; Font font = null;
            try
            {
                ComboBox senderComboBox = sender as ComboBox;

                int width = senderComboBox.Width;
                g = senderComboBox.CreateGraphics();
                font = senderComboBox.Font;

                //checks if a scrollbar will be displayed.
                //If yes, then get its width to adjust the size of the drop down list.
                int vertScrollBarWidth =
                     (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                     ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                // 取得数据源
                DataTable dt = (DataTable)senderComboBox.DataSource;
                foreach (DataRow s in dt.Rows)  //Loop through list items and check size of each items.
                {
                    if (s != null)
                    {
                        newWidth = (int)g.MeasureString(s["name"].ToString().Trim(), font).Width
                            + vertScrollBarWidth;
                        if (width < newWidth)
                            width = newWidth;   //set the width of the drop down list to the width of the largest item.
                    }
                }
                this.DropDownWidth = width;
            }
            catch
            { }
            finally
            {
                if (g != null)
                    g.Dispose();
            }

        }



        /// <summary>
        ///  下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void OnSelectedIndexChanged(object sender,EventArgs e)
        {
            AutoSizeComboBoxItem(this);
           // base.OnSelectedIndexChanged(e); 
        }

       /* #endregion*/
/******************************************************************************************************/

        public void setValueText(string text)
        {
            this.valueText = text;
            this.Text = text;
        }


        //获取相应的数据表
        private DataTable CreateDataTable(string s)
        {
            SqlConnection conn = new SqlConnection(@"server=2012-20140123sp\sqlexpress;user id=sa;password=zr199406;database=publish");
            SqlDataAdapter dapt2;
            DataSet ds = new DataSet();
            string sql2;
            sql2 = "select title_id,au_id,au_ord from dbo.titleauthor where title_id='" + s.Replace("'", "''") + "'";
            if (s == null||s == "")
            {
                sql2 = "select title_id,au_id,au_ord from dbo.titleauthor"; //可以把s改成想要的默认值
            }

            dapt2 = new SqlDataAdapter(sql2, conn);
            dapt2.Fill(ds, "table1");
            DataTable dtable = new DataTable();
            //创建下拉框中表格的显示字段
            dtable.Columns.Add("title_id", typeof(System.String));
            dtable.Columns.Add("au_id", typeof(System.String));
            dtable.Columns.Add("au_ord", typeof(System.String));
           
            dtable = ds.Tables["table1"];
            return dtable;
        }


        protected void InitItems()
        {
           dataTable = CreateDataTable(this.valueText);
            this.DataSource = dataTable;
            this.DisplayMember = "title_id";
            this.ValueMember = "title_id";
            this.DrawMode = DrawMode.OwnerDrawVariable;    //手动绘制所有元素
            this.DropDownStyle = ComboBoxStyle.DropDown;   //下拉框样式设置为不能编辑 
        }




        // 初始化数据源各列
        private void InitializeColumns()
        {
            PropertyDescriptorCollection propertyDescriptorCollection = DataManager.GetItemProperties();
            columnWidths = new float[propertyDescriptorCollection.Count];
            columnNames = new string[propertyDescriptorCollection.Count];

            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                string name = propertyDescriptorCollection[i].Name;
                columnNames[i] = name;
            }
        }


        protected override void OnValueMemberChanged(EventArgs e)
        {
            base.OnValueMemberChanged(e);
            InitializeValueMemberColumn();
        }

        //返回valuemember在显示列中的索引位置
        private void InitializeValueMemberColumn()
        {
            int colIndex = 0;
            foreach (String columnName in columnNames)
            {
                if (String.Compare(columnName, ValueMember, true, CultureInfo.CurrentUICulture) == 0)
                {
                    valueMemberColumnIndex = colIndex;
                    break;
                }
                colIndex++;
            }
        }

        //显示下拉框查询结果内容    
        protected override void OnDropDown(EventArgs e)
        {
            InitializeColumns();
            InitItems();   
            if ((this.DataSource != null) && (!string.IsNullOrEmpty(this.DisplayMember)))
            {
                if (!string.IsNullOrEmpty(this.Text))
                {

                    DataView dv = dataTable.DefaultView;
                    dv.RowFilter = string.Format("title_id like '{0}%'", this.Text);
                    DataTable NewDt = dv.ToTable("newTableName");
                    DataTable copyTable = new DataTable();
                    copyTable.Columns.Add("title_id", Type.GetType("System.String"));
                    copyTable.Columns.Add("au_id", Type.GetType("System.String"));
                    copyTable.Columns.Add("au_ord", Type.GetType("System.String"));
                   
                    copyTable.Rows.Add(new object[] { "书籍编号", "作者编号", "作者等级" });

                    for (int i = 0; i < NewDt.Rows.Count; i++)
                    {
                        copyTable.Rows.Add(new object[] { NewDt.Rows[i][0].ToString() + "\t", NewDt.Rows[i][1].ToString() + "\t", NewDt.Rows[i][2].ToString() + "\t" });
                    }
                    this.DataSource = copyTable;
                }
                else
                {
                    this.DataSource = dataTable;
                }

            }
            base.OnDropDown(e);
            this.DropDownWidth = (int)CalculateTotalWidth();//计算下拉框的总宽度 
        }


        private float CalculateTotalWidth()
        {
            columnPadding = 5;
            float totalWidth = 0;
            foreach (int width in columnWidths)
            {
                totalWidth += (width + columnPadding);
            }
            //总宽度加上垂直滚动条的宽度
            return totalWidth + SystemInformation.VerticalScrollBarWidth;
        }

        // 获取各列的宽度和项的总宽度
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (DesignMode)
            {
                return;
            }
            InitializeColumns();
            for (int i = 0; i < columnNames.Length; i++)
            {
                string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[i]));
                SizeF sizeF = e.Graphics.MeasureString(item, Font);//返回显示项字符串的大小
                columnWidths[i] = Math.Max(columnWidths[i], sizeF.Width + 40);
            }
            float totalWidth = CalculateTotalWidth() + 320;//计算combobox下拉框项的宽度
            e.ItemWidth = (int)totalWidth;//设置下拉框项的宽度
        }

        // 绘制下拉框的内容
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (DesignMode)
            {
                return;
            }
            Rectangle boundsRect = e.Bounds;//获取绘制项边界的矩形         
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            //第一行为表头，设置颜色
            if (e.Index ==0)   
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(127, 128, 0));  //定义画刷
                    Rectangle rect = e.Bounds;
                    Rectangle rectColor = new Rectangle(rect.Location, new Size(rect.Width, rect.Height));
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
                            boundsRect.Width = (int)columnWidths[i] + columnPadding;//列的宽度
                            lastRight = boundsRect.Right;

                            if (i==valueMemberColumnIndex)//如果是valuemember
                            {
                                using (Font font = new Font(Font, FontStyle.Bold))
                                {
                                    //绘制项的内容
                                    e.Graphics.DrawString(item, font, brush, boundsRect);
                                }
                            }
                            else
                            {
                                //绘制项的内容
                                e.Graphics.DrawString(item, Font, brush, boundsRect);
                            }

                            //绘制各项间的竖线
                            if (i<columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);                            
                            }
                        }
                        e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);                
                    }


                //绘制各行间横线
                 int hh=0;   //起点高度为0
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
                                int h = (int)ItemHeight;   //第一次绘制的高度
                                hh += h;
                                for (int j = 0; j < columnNames.Length;j++)
                                {                                 
                                    if (i < columnNames.Length - 1)
                                    {
                                        if (i < columnNames.Length - 1)
                                        {
                                            e.Graphics.DrawLine(linePen, boundsRect.Left, hh, boundsRect.Right*6, hh);
                                        }
                                    }
                                }
                                e.Graphics.DrawLine(linePen, boundsRect.Left, boundsRect.Bottom, boundsRect.Right, boundsRect.Bottom);
                            }

                        }
                    }             
                    e.DrawFocusRectangle();

                }

            }
        }
    }
}
        #endregion