using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MyButton
{
    public partial class GZXButton : Button
    {
        private enum MouseActionType
        {
            None,
            Hover,
            Click
        }
        private MouseActionType mouseActionType;
        private ImageAttributes imgattr = new ImageAttributes();//实例化一个图像属性类
        private Bitmap btnbmp;    //定义一个位图
        private Rectangle btnrc; //定义一个矩形

        public GZXButton()
        {
            InitializeComponent();
            mouseActionType = MouseActionType.None;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |//禁止擦除背景
                ControlStyles.DoubleBuffer |//双缓冲
                ControlStyles.UserPaint, true);
            
            this.Font = new Font("Aial Black", 12, FontStyle.Bold);
            this.BackColor = Color.DarkTurquoise;
            this.Size = new Size(112, 48);
        }
       
        //按钮外观
        private GraphicsPath GetGraphicsPath(Rectangle rc, int r)
        {
            int x = rc.X, y = rc.Y, w = rc.Width, h = rc.Height;
            GraphicsPath gpath = new GraphicsPath();
            gpath.AddArc(x, y, r, r, 180, 90);//左上角圆弧
            gpath.AddArc(x + w - r, y, r, r, 270, 90);//右上角圆弧
            gpath.AddArc(x + w - r, y + h - r, r, r, 0, 90);//右下角圆弧
            gpath.AddArc(x, y + h - r, r, r, 90, 90);//左下角圆弧
            gpath.CloseFigure();//闭合
            return gpath;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            // base.OnPaint(pe);
            Graphics g = pe.Graphics;//创建画布
            g.Clear(SystemColors.ButtonFace);//重置背景颜色,可以自定义
            Color clr = this.BackColor;
            int btnOff = 0;//按钮边距
            int shadowOff = 0;//阴影边距
            switch (mouseActionType)
            {
                case MouseActionType.None:
                    break;
                case MouseActionType.Hover:
                    clr = Color.LightGray;
                    break;
                case MouseActionType.Click:
                    shadowOff = 4;
                    clr = Color.LightGray;
                    btnOff = 2;
                    break;
                default:
                    break;
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿
            //创建按钮本身的图形
            Rectangle rc1 = new Rectangle(btnOff, btnOff, this.ClientSize.Width - 8 - btnOff, this.ClientSize.Height - 8 - btnOff);
            GraphicsPath gpath1 = this.GetGraphicsPath(rc1, 20);
            LinearGradientBrush br1 = new LinearGradientBrush(new Point(0, 0), new Point(0, rc1.Height + 6), clr, Color.White);
            //创建按钮阴影
            Rectangle rc2 = rc1;
            rc2.Offset(shadowOff, shadowOff);
            GraphicsPath gpath2 = this.GetGraphicsPath(rc2, 20);
            PathGradientBrush br2 = new PathGradientBrush(gpath2);
            br2.CenterColor = Color.Black;
            br2.SurroundColors = new Color[] { SystemColors.ButtonFace };
            //将渐变结束颜色设定为窗体前景色，可以根据窗口的前景颜色适当调整

            //创建按钮顶部白色渐变
            Rectangle rc3 = rc1;
            rc3.Inflate(-5, -5);
            rc3.Height = 15;
            GraphicsPath gpath3 = GetGraphicsPath(rc3, 20);
            LinearGradientBrush br3 = new LinearGradientBrush(rc3, Color.FromArgb(255, Color.White), Color.FromArgb(0, Color.White), LinearGradientMode.Vertical);
            //绘制图形
            g.FillPath(br2, gpath2);//绘制阴影
            g.FillPath(br1, gpath1);//绘制按钮
            g.FillPath(br3, gpath3);//绘制顶部白色泡泡
            //设定内存位图对象，进行二级缓存绘图操作
            btnrc = new Rectangle(rc1.Location, rc1.Size);
            btnbmp = new Bitmap(btnrc.Width, btnrc.Height);
            Graphics g_bmp = Graphics.FromImage(btnbmp);
            g_bmp.SmoothingMode = SmoothingMode.AntiAlias;
            g_bmp.FillPath(br1, gpath1);
            g_bmp.FillPath(br3, gpath3);
            //将region赋值给button
            Region rgn = new Region(gpath1);
            rgn.Union(gpath2);
            this.Region = rgn;
            //绘制按钮的文本
            GraphicsPath gpath4 = new GraphicsPath();
            RectangleF gpath1bounds = gpath1.GetBounds();
            Rectangle rcText = new Rectangle((int)gpath1bounds.X + btnOff, (int)gpath1bounds.Y + btnOff, (int)gpath1bounds.Width,
                (int)gpath1bounds.Height);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;//横竖都居中
            gpath4.AddString(this.Text, this.Font.FontFamily, (int)this.Font.Style, this.Font.Size, rcText, strFormat);
            Pen txtPen = new Pen(this.ForeColor, 1);
            g.DrawPath(txtPen, gpath4);
            g_bmp.DrawPath(txtPen, gpath4);
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                this.mouseActionType = MouseActionType.Click;
                this.Invalidate();
            }
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            this.mouseActionType = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            this.mouseActionType = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseHover(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.mouseActionType = MouseActionType.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)//鼠标离开事件
        {
            this.mouseActionType = MouseActionType.None;
            this.Invalidate();
            base.OnMouseLeave(e);
        }
    }
}