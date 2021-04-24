using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragAndDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                label1.Text = "Отпустите мышь";
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panel1_DragLeave(object sender, EventArgs e)
        {
            label1.Text = "Перетащите файлы сюда";
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            List<string> paths = new List<string>();
            foreach (string obj in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                if (Directory.Exists(obj))
                    paths.AddRange(Directory.GetFiles(obj, "*.*", SearchOption.AllDirectories));
                else
                    paths.Add(obj);
            }
            label2.Text = string.Join("\r\n", paths);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Укажите какой-нибудь файл";
            file.InitialDirectory = @"C:\Users\Dmitry\Desktop";
            if(file.ShowDialog() == DialogResult.OK)
            {
                label2.Text = file.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private async void panel1_Paint(object sender, PaintEventArgs e)
        {
            await Task.Run(async () =>
            {
                Pen pen = new Pen(Color.Black, 2);
                for (int i = 30; i > 2; i--, await Task.Delay(30))
                {
                    panel1.CreateGraphics().Clear(SystemColors.Control);
                    pen.DashPattern = new float[] { 2, i };
                    panel1.CreateGraphics().DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);
                }
            }
            );
                //float[] dashes = { 2, 2, 2, 2 };
                //Pen pen = new Pen(Color.Black, 2);
                //pen.DashPattern = dashes;
                //e.Graphics.DrawRectangle(pen, 1, 1, panel1.Width - 2, panel1.Height - 2);
        }
    }
}
