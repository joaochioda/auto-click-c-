using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP = 0x0004;
        private const int LEFTDOWN = 0x0002;
        public bool Click = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread AC = new Thread(AutoClick);
            AC.Start();
            backgroundWorker1.RunWorkerAsync();

        }
        private void AutoClick()
        {
            while(true)
            {
                if (Click == true)
                {
                    mouse_event(dwFlags: LEFTUP, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: LEFTDOWN, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(100);
                }
                Thread.Sleep(2);
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(true)
            {
                if (checkBox1.Checked)
                {
                    if(GetAsyncKeyState(Keys.Down)<0)
                    {
                        Click = false;
                    } else if (GetAsyncKeyState(Keys.Up)<0)
                    {
                        Click = true;
                    }
                    Thread.Sleep(1);
                }
                Thread.Sleep(1);
            }
        }
    }
}
