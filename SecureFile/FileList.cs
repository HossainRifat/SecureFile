using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureFile
{
    public partial class FileList : UserControl
    {
        private string fileName;
        private string filePath;
        private string fileType;
        private string fileSize;
        private string operation;

        public string Operation
        {
            set { this.operation = value; }
            get { return this.operation; }
        }

        public string FilePath
        {
            set { this.filePath = value; }
            get { return this.filePath; }
        }

        public string FileSize
        {
            set { this.fileSize = value; this.label2.Text = (long.Parse(value)/1000000).ToString()+" MB"; }
            get { return this.fileSize; }
        }

        public string FileType
        {
            set { this.fileType = value; this.label3.Text = value; }
            get { return this.fileType; }
        }

        public string FileName
        {
            set { this.fileName = value;this.label1.Text = value; }
            get { return this.fileName; }
        }

        public FileList()
        {
            InitializeComponent();
        }

        private void FileList_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;
        }

        private void FileList_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;

        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;

        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightGray;

        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

        }

        private void label2_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.operation == "open")
            {
                //string a = "E:\\USER\\Downloads\\icons8-bell-488.png.SFile";
                string output = Form1.Instance.data[2] + "\\temp";
                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }
                
                string[] name = this.fileName.Split('.');
                output = output + "\\" + name[0];
                for (int i = 1; i < name.Length - 1; i++)
                {
                    output = output + "." + name[i];
                }
                //Crypto.FileEncrypt(b, data[2] + "//Encrypted", "1234");
                //MessageBox.Show(filePath);
                process p = new process();
                p.Visible = true;
                p.Fname.Text = Path.GetFileName(filePath);
                p.Fsize.Text = new FileInfo(filePath).Length.ToString() +" bytes";
                

                if (Crypto.FileDecrypt(this.filePath, output, "1234"))
                {
                    p.p2.Width = 421;
                    System.Diagnostics.Process.Start(output);
                    Thread.Sleep(1000);
                    p.Visible = false;
                }
                else
                {
                    MessageBox.Show("Something went wrong.\nError Code: flistopen.");
                }
            }
            else if(this.operation == "import")
            {
                string output = Form1.Instance.data[2] + "\\Encrypted";
                if (Crypto.FileEncrypt(this.filePath, output, "1234"))
                {
                    MessageBox.Show("Sussess.\nFile Name: " + this.fileName);
                }
                else
                {
                    MessageBox.Show("Something went wrong.\nError Code: flistopen.");
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
