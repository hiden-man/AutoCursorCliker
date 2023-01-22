using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCursorCliker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0X20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Location = new Point(0,0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Click_Mouse();
        }
        private void Click_Mouse()
        {
            int c = 0;
            POINT p = new POINT();
            if (radioButton1.Checked)
            {
                Thread.Sleep(Convert.ToInt32(textBox3.Text)*1000);
                while (true)
                {
                    GetCursorPos(ref p);
                    ClientToScreen(Handle, ref p);
                    DoMouseLeftClick(p.x, p.y); // Ліва кнопка миши
                    c++;
                    Thread.Sleep(Convert.ToInt32(textBox2.Text));
                    if (c == Convert.ToInt32(textBox1.Text))
                    {
                        break;
                    }
                }
            }
            else if (radioButton2.Checked)
            {
                Thread.Sleep(Convert.ToInt32(textBox3.Text)*1000);
                while (true)
                {
                    GetCursorPos(ref p);
                    ClientToScreen(Handle, ref p);
                    DoMouseRightClick(p.x, p.y); // права кнопка миши
                    c++;
                    Thread.Sleep(Convert.ToInt32(textBox2.Text));
                    if (c == Convert.ToInt32(textBox1.Text))
                    {
                        break;
                    }
                }
            }
            else if (radioButton3.Checked)
            {
                Thread.Sleep(Convert.ToInt32(textBox3.Text)*1000);
                while (true)
                {
                    GetCursorPos(ref p);
                    ClientToScreen(Handle, ref p);
                    DoMouseDoubleLeftClick(p.x, p.y); // подвійний клік лівою кнопкою миши
                    c++;
                    Thread.Sleep(Convert.ToInt32(textBox2.Text));
                    if (c == Convert.ToInt32(textBox1.Text))
                    {
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Оберіть кнопку яку хочете використовувати, будь-ласка","Увага", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            //Thread.Sleep(Convert.ToInt32(textBox3.Text));
            //while (true)
            //{
            //    GetCursorPos(ref p);
            //    ClientToScreen(Handle, ref p);
            //    //DoMouseLeftClick(p.x, p.y); // Ліва кнопка миши
            //    //DoMouseRightClick(p.x, p.y); // права кнопка миши
            //    //DoMouseDoubleLeftClick(p.x, p.y); // подвійний клік лівою кнопкою миши
            //    c++;
            //    Thread.Sleep(Convert.ToInt32(textBox2.Text));
            //    if (c == Convert.ToInt32(textBox1.Text))
            //    {
            //        break;
            //    }
            //}

        }

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);

        public const int MOUSE_EVENT_F_LEFTDOWN = 0X02;
        public const int MOUSE_EVENT_F_LEFTUP = 0X04;

        public const int MOUSE_EVENT_F_RIGHTDOWN = 0x08;
        public const int MOUSE_EVENT_F_RIGHTUP = 0x10;

        private void DoMouseLeftClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);
        }
        private void DoMouseRightClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_RIGHTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_RIGHTUP, x, y, 0, 0);
        }
        private void DoMouseDoubleLeftClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);

            mouse_event(MOUSE_EVENT_F_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_F_LEFTUP, x, y, 0, 0);
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT lpPoint);
    }
}
