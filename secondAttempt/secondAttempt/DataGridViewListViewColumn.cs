using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace CustomDataGridViewColumn
{
    //定义新的一列用于扩展的下拉列表框
    public class DataGridViewListViewColumn : DataGridViewColumn
    {
        public DataGridViewListViewColumn()
            : base(new DataGridViewListViewCell())
        {
            
        }        

        //设置用于创建新单元格的模板。
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {               
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewListViewCell)))  //DataGridViewListViewCell
                {
                    throw new InvalidCastException("这不是一个DataGridCell！");
                }
                base.CellTemplate = value;
            }
        }


        //设置DataTable格式的数据源
        private DataTable _dataSource;
        public DataTable DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = null;
                _dataSource = value;                
            }
        }

        // 显示的内容
        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
            }
        }
        
        // 设置显示的列
        private string[] _columnsToDisplay;
        public string[] ColumnsToDisplay 
        {
            get { return _columnsToDisplay; }
            set
            {
                _columnsToDisplay = value;
            }
        }
    }

    
    // DataGridViewListViewCell
    public class DataGridViewListViewCell : DataGridViewTextBoxCell
    {
        public DataGridViewListViewCell()
            : base()
        {                    
        }

      
        // 附加并初始化寄宿的编辑控件      
        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            MyComboBox combo =
               DataGridView.EditingControl as MyComboBox;
           
            combo.setValueText((string)this.Value);
            combo.Text = combo.valueText;
            combo.EditingControlFormattedValue = combo.valueText;
            combo.DisplayMember = ((DataGridViewListViewColumn)this.OwningColumn).DisplayMember;
            combo.Refresh();

        }

    
        // 编辑时的类型
        public override Type EditType
        {
            get
            {
                return typeof(MyComboBox);
            }
        }

 
        // 值类型
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        // 默认值
        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
    }
}
