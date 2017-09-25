using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FernDesktopClient
{
    public partial class Main : Form
    {
        string[] adrs;
        TcpListener ln;
        Thread tn;
        bool on;

        int fps = 0;

        public Main(string ip)
        {
            InitializeComponent();
            adrs = ip.Split(':');
            ln = new TcpListener(IPAddress.Any,int.Parse(adrs[1]));
            tn = new Thread(new ThreadStart(GetImg));
            tn.Start();
            on = true;
        }

        private void GetImg()
        {
            ln.Start();
            while (on)
            {
                ShowIMG();
            }
        }

        private void ShowIMG()
        {
            fps++;
            TcpClient tl = ln.AcceptTcpClient();
            NetworkStream ns = tl.GetStream();
            ToFill.BackgroundImage = Image.FromStream(new MemoryStream(ReadFully(ns)));
            ns.Close();
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            on = false;
            ln.Stop();
            tn.Join();
        }

        private void SendControl(string msg)
        {
            try
            {
                TcpClient cn = new TcpClient(adrs[0], int.Parse(adrs[1]));
                NetworkStream nw = cn.GetStream();
                StreamWriter sw = new StreamWriter(nw);
                sw.Write(msg);
                sw.Flush();
                sw.Close();
                nw.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al conectar con el ordenador remoto");
                Application.Exit();
            }
        }

        private void ToFill_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                SendControl("rightdown" + e.X + ":" + e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                SendControl("leftdown" + e.X + ":" + e.Y);
            }
        }

        private void ToFill_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SendControl("rightup" + e.X + ":" + e.Y);
            }
            else if (e.Button == MouseButtons.Left)
            {
                SendControl("leftup" + e.X + ":" + e.Y);
            }
        }

        private void ToFill_MouseMove(object sender, MouseEventArgs e)
        {
            SendControl("move" + e.X + ":" + e.Y);
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            SendControl("key" + e.KeyCode.ToString());
        }
    }
}
