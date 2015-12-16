using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace CustomDataGridViewColumn
{
    class newDGV:DataGridView
    {
         private bool showRowHeaderNumbers = true;
         private Image myImage;
         
         public newDGV()
        {
            base.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.DataGridViewEx_RowPostPaint);
        }

        #region 方法
        //绘制行号
         private void DataGridViewEx_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.showRowHeaderNumbers)
            {
                string s = (e.RowIndex + 1).ToString();
                Brush black = Brushes.Black;
                e.Graphics.DrawString(s, base.DefaultCellStyle.Font, black, (float) ((e.RowBounds.Location.X + (base.RowHeadersWidth / 2)) - 4), (float) (e.RowBounds.Location.Y + 4));
            }
        }
        //绘制背景图片
         protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
         {
             base.PaintBackground(graphics, clipBounds, gridBounds);
             if (myImage != (Image)null)
                 graphics.DrawImage(myImage, gridBounds);
         }
         #endregion
         
        #region 属性
         [Description("背景图片")]
         public Image BackGroundImage
         {
             get { return myImage; }
             set { myImage = value; }
         }

         [Description("是否显示行号"), DefaultValue(true)]
        public bool ShowRowHeaderNumbers
        {
            get
            {
                return this.showRowHeaderNumbers;
            }
            set
            {
                if (this.showRowHeaderNumbers != value)
                {
                    base.Invalidate();
                }
                this.showRowHeaderNumbers = value;
            }
        }
        #endregion


    }
}
