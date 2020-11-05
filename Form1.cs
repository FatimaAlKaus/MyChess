using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Form1 : Form
    {
        Server server;
        Client client;
        Field field;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
            server = new Server();
            field = new Field();
            client = new Client();



            //TODO: подключать в зависмости от нажатой кнопки
            client.Read += field.Move;
            server.Read += field.Move;
            server.Connected += field.Reset;
            client.Connected += field.Reset;
            field.Refresh += Refresh;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            field.Show(e.Graphics);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            field.X = e.X;
            field.Y = e.Y;
            field.Put();
            field.Select();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            field.X = e.X;
            field.Y = e.Y;
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            field.Reset();
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            field.ChessEventHandler = server.Write;
            server.Listen();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            field.ChessEventHandler = client.Write;
            client.Connect();
        }
    }
}
