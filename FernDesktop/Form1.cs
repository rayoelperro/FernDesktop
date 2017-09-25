using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FernDesktop
{
    public partial class Form1 : Form
    {
        TcpListener ln = null;

        Thread ct = null;
        Thread lt = null;

        bool on = true;

        string ipt = "";
        int getcommandport;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            TcpListener nl = new TcpListener(IPAddress.Any, int.Parse(textBox1.Text));
            nl.Start();
            TcpClient cl = nl.AcceptTcpClient();
            ipt = ((IPEndPoint)cl.Client.RemoteEndPoint).Address.ToString();
            cl.Close();
            nl.Stop();
            ct = new Thread(new ThreadStart(SendImg));
            getcommandport = int.Parse(textBox1.Text);
            lt = new Thread(new ThreadStart(GetCommand));
            ct.Start();
            lt.Start();
        }

        private void SendImg()
        {
            while (on)
            {
                try
                {
                    byte[] content = BytesfromRender(RenderImg());
                    TcpClient cn = new TcpClient(ipt, int.Parse(textBox1.Text));
                    NetworkStream nw = cn.GetStream();
                    nw.Write(content,0,content.Length);
                    nw.Flush();
                    nw.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error enviando al equipo remoto");
                    close();
                }
            }
        }

        private void close()
        {
            MethodInvoker mi = delegate {
                Close();
            };
            if (InvokeRequired)
                Invoke(mi);
        }

        private void GetCommand()
        {
            ln = new TcpListener(IPAddress.Any, getcommandport);
            ln.Start();
            while (on)
            {
                TcpClient tl = ln.AcceptTcpClient();
                NetworkStream ns = tl.GetStream();
                StreamReader sr = new StreamReader(ns);
                string ret = sr.ReadToEnd();
                sr.Close();
                ns.Close();
                Controles.ReadCommand(ret);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            on = false;
            if(ln != null)
                ln.Stop();
            if (lt != null)
                lt.Join();
            if (ct != null)
                ct.Join();
        }

        private Bitmap RenderImg()
        {
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return bmpScreenshot;
        }

        private byte[] BytesfromRender(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
