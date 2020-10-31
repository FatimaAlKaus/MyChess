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
        Field field;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
            field = new Field();

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
    }
}
