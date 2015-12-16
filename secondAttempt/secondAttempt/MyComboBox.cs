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
        private float[] columnWidths = new float[0];  //����
        private String[] columnNames = new String[0]; //������
        private int valueMemberColumnIndex = 0;       //valueMember���������ڵ�����
       
        private DataTable dataTable = new DataTable("Details");
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        public string valueText = "";
        

        //���캯��,����InitItems()
        public MyComboBox()
        {
            InitializeComponent();
            this.DropDownHeight = 75;   //�Լ���������������೤��ʾ������
            this.IntegralHeight = false;
            InitItems();
        }

        #region �̳�IDataGridViewEditingControl�ӿ�
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

        // ʵ��IDataGridViewEditingControl�ӿ���GetEditingControlFormattedValue�ķ���
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

       // ʵ��IDataGridViewEditingControl�ӿ���ApplyCellStyleToEditingControl�ķ���
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        // ʵ��IDataGridViewEditingControl�ӿ���EditingControlRowIndex����
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

        // ʵ��IDataGridViewEditingControl�ӿ���EditingControlWantsInputKey�ķ���
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

        // ʵ��IDataGridViewEditingControl�ӿ���PrepareEditingControlForEdit������
        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }
        
        // ʵ��IDataGridViewEditingControl�ӿ���RepositionEditingControlOnValueChange������
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // ʵ��IDataGridViewEditingControl�ӿ���EditingControlDataGridView������
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

        // ʵ��IDataGridViewEditingControl�ӿ���EditingControlValueChanged������
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

        // ʵ��IDataGridViewEditingControl�ӿ���EditingPanelCursor������
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        // ʵ��IDataGridViewEditingControl�ӿ���OnTextChanged�ķ���
        protected override void OnTextChanged(EventArgs eventargs)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(eventargs);
        }  

////////////**********************************************************************************************////
        private DataTable dtfliter = new DataTable();
        // �������־
        private bool flag = false;     
       // private DataTable mdataSource = new DataTable();
        /// <summary>
        /// combobox �����¼�
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
            // ���������ֵ�Դ�ɸѡ
            bool validate = Fillter(str);

            // ��ʵ������
            this.DroppedDown = true;
            // ������Ч������
            if (!validate)
            {
                MessageBox.Show("�����������Ч,����������");
            }
            else
            {
                this.Text = str;
                if (this.Text != null && this.Text != string.Empty)
                {
                    // ��ù��λ��
                    this.SelectionStart =- this.valueText.Length;
                }

                }
            // ��ʵ���
            this.Cursor = Cursors.Default;
        }




        /// <summary>
        /// ɸѡƥ���ֵ
        /// </summary>
        /// <param name="str"></param>
        private bool Fillter(string str)
        {
            bool isvalida = true;
            DataTable dt = dataTable.Clone();
            // ѭ��ɸѡƥ���ֵ      
            foreach (DataRow dr in dataTable.Rows)
            {
                int i = -1;
                i = dr["title_id"].ToString().IndexOf(str.ToString());
                if (i > -1)
                {
                    DataRow newrow = dt.NewRow();
                    newrow.ItemArray = dr.ItemArray;
                    // �����µ�����
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
            // ��Ϊһ������Ҫ���⴦��/��Ȼ��귵��������
            if (dt.Rows.Count == 1)
            {
                // ���⴦���־
                flag = true;
                this.Text = str;// str����Ϊ�����ֵ; 
                int len = str.Length;
                // Fillter(str.Substring(0, len - 1));
            }

            // ������Դ��û�������ֵ��ʾ, ���ҽض��ַ�����ɸѡ
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
        /// �Զ��������
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
                // ȡ������Դ
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
        ///  �����¼�
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


        //��ȡ��Ӧ�����ݱ�
        private DataTable CreateDataTable(string s)
        {
            SqlConnection conn = new SqlConnection(@"server=2012-20140123sp\sqlexpress;user id=sa;password=zr199406;database=publish");
            SqlDataAdapter dapt2;
            DataSet ds = new DataSet();
            string sql2;
            sql2 = "select title_id,au_id,au_ord from dbo.titleauthor where title_id='" + s.Replace("'", "''") + "'";
            if (s == null||s == "")
            {
                sql2 = "select title_id,au_id,au_ord from dbo.titleauthor"; //���԰�s�ĳ���Ҫ��Ĭ��ֵ
            }

            dapt2 = new SqlDataAdapter(sql2, conn);
            dapt2.Fill(ds, "table1");
            DataTable dtable = new DataTable();
            //�����������б�����ʾ�ֶ�
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
            this.DrawMode = DrawMode.OwnerDrawVariable;    //�ֶ���������Ԫ��
            this.DropDownStyle = ComboBoxStyle.DropDown;   //��������ʽ����Ϊ���ܱ༭ 
        }




        // ��ʼ������Դ����
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

        //����valuemember����ʾ���е�����λ��
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

        //��ʾ�������ѯ�������    
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
                   
                    copyTable.Rows.Add(new object[] { "�鼮���", "���߱��", "���ߵȼ�" });

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
            this.DropDownWidth = (int)CalculateTotalWidth();//������������ܿ�� 
        }


        private float CalculateTotalWidth()
        {
            columnPadding = 5;
            float totalWidth = 0;
            foreach (int width in columnWidths)
            {
                totalWidth += (width + columnPadding);
            }
            //�ܿ�ȼ��ϴ�ֱ�������Ŀ��
            return totalWidth + SystemInformation.VerticalScrollBarWidth;
        }

        // ��ȡ���еĿ�Ⱥ�����ܿ��
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
                SizeF sizeF = e.Graphics.MeasureString(item, Font);//������ʾ���ַ����Ĵ�С
                columnWidths[i] = Math.Max(columnWidths[i], sizeF.Width + 40);
            }
            float totalWidth = CalculateTotalWidth() + 320;//����combobox��������Ŀ��
            e.ItemWidth = (int)totalWidth;//������������Ŀ��
        }

        // ���������������
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (DesignMode)
            {
                return;
            }
            Rectangle boundsRect = e.Bounds;//��ȡ������߽�ľ���         
            e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            //��һ��Ϊ��ͷ��������ɫ
            if (e.Index ==0)   
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(127, 128, 0));  //���廭ˢ
                    Rectangle rect = e.Bounds;
                    Rectangle rectColor = new Rectangle(rect.Location, new Size(rect.Width, rect.Height));
                    e.Graphics.FillRectangle(brush, rectColor);   //�����ɫ
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
                        //ѭ������
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[i]));
                            boundsRect.X = lastRight;//�е����λ��
                            boundsRect.Width = (int)columnWidths[i] + columnPadding;//�еĿ��
                            lastRight = boundsRect.Right;

                            if (i==valueMemberColumnIndex)//�����valuemember
                            {
                                using (Font font = new Font(Font, FontStyle.Bold))
                                {
                                    //�����������
                                    e.Graphics.DrawString(item, font, brush, boundsRect);
                                }
                            }
                            else
                            {
                                //�����������
                                e.Graphics.DrawString(item, Font, brush, boundsRect);
                            }

                            //���Ƹ���������
                            if (i<columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);                            
                            }
                        }
                        e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);                
                    }


                //���Ƹ��м����
                 int hh=0;   //���߶�Ϊ0
                 using (SolidBrush brush2 = new SolidBrush(ForeColor))
                    {
                        if (this.Items.Count == 0)
                        {
                            e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush2, boundsRect);
                        }
                        else
                        {
                            //ѭ������
                            for (int i = 0; i < this.Items.Count; i++)
                            {
                                boundsRect.X = 0;     
                                int h = (int)ItemHeight;   //��һ�λ��Ƶĸ߶�
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