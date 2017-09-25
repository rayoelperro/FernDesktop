using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace FernDesktopClient
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient cl = new TcpClient(textBox1.Text.Split(':')[0],int.Parse(textBox1.Text.Split(':')[1]));
            cl.Close();
            Main mn = new Main(textBox1.Text);
            mn.FormClosing += (sender1, e1) =>
            {
                Close();
            };
            mn.Show();
            Hide();
        }
    }
}
