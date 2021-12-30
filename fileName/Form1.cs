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
using SecureFile;
namespace fileName
{
    public partial class Form1 : Form
    {
        private string[] args;
        private string[] data;
        private int t = 0, d = 0;

        public string[] Args
        {
            set { this.args = value; }
            get { return args; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == data[0])
            {
                //for (int i = 0; i < Args.Length; i++)
                //{
                //    MessageBox.Show(Args[i]);
                //}
               
                if (this.Args.Length > 0)
                {
                    

                    File.Exists(Args[0]);
                    {
                        //MessageBox.Show(Args);
                        this.d = 0;
                        string output = data[2] + "\\temp";
                        if (!Directory.Exists(output))
                        {
                            Directory.CreateDirectory(output);
                        }
                        t = 1;

                        string[] name = Path.GetFileName(Args[0]).Split('.');
                        output = output + "\\" + name[0];/* + "." + name[1];*/
                        for (int i = 1; i < name.Length - 1; i++)
                        {
                            output = output + "." + name[i];
                        }

                        if (name[name.Length - 1] == "SFile")
                        {
                            //Crypto.FileEncrypt(b, data[2] + "//Encrypted", "1234");
                            Processing p = new Processing();
                            p.Fname.Text = Path.GetFileName(Args[0]);
                            p.Fsize.Text = new FileInfo(Args[0]).Length.ToString() + "bytes";
                            p.Visible = true;
                            //MessageBox.Show(output);
                            if (SecureFile.Crypto.FileDecrypt(Args[0], output, "1234"))
                            {
                                p.p2.Width = 421;
                                this.textBox1.Text = "";
                                System.Diagnostics.Process.Start(output);
                                p.Visible = false;
                            }
                            else
                            {
                                p.Visible = false;
                                MessageBox.Show("Something went wrong.\nError Code: f.f.flistopen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            //MessageBox.Show(Args[0]+"\n"+Path.GetDirectoryName(Args[0]));
                            Processing p = new Processing();
                            p.Fname.Text = Path.GetFileName(Args[0]);
                            p.Fsize.Text = new FileInfo(Args[0]).Length.ToString() + "bytes";
                            p.Visible = true;
                            if(Crypto.FileEncrypt(Args[0], Path.GetDirectoryName(Args[0]), "1234"))
                            {
                                this.d = 1;
                                p.p2.Width = 421;
                                this.textBox1.Text = "";
                                p.Visible = false;
                                Application.Exit();
                            }
                            else
                            {
                                MessageBox.Show("Something went wrong.\nError Code: flistopen.");
                            }
                            
                        }

                        
                    }
                    //string a = "E:\\USER\\Downloads\\icons8-bell-488.png.SFile";
                   
                }
            }
            else
            {
                MessageBox.Show("Incorrect Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            SecureFile.Form1 f = new SecureFile.Form1();
            f.Visible = true;
            this.Visible = false;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.UseSystemPasswordChar = false;
            }
            else
            {
                textBox1.UseSystemPasswordChar = true;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Args.Length > 0)
            {
                string filePath = @"E:\USER\Documents\SecureFileData.txt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Something went wrong! Try again.\nError code: f.f.f1load.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    data = File.ReadAllLines(filePath).ToArray();
                    this.label2.Text = "Hints: " + data[1] + ".";
                }
            }
          
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.d != 1)
            {
                DialogResult d = MessageBox.Show("Exit Application?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    if (t == 1)
                    {
                        string tempPath = data[2] + "\\temp";
                        if (Directory.Exists(data[2] + "\\temp"))
                        {
                            try
                            {
                                Directory.Delete(data[2] + "\\temp", true);
                                Application.ExitThread();
                            }
                            catch
                            {
                                MessageBox.Show("Unable to close.\nApplication's content is using by another application.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            Application.ExitThread();
                        }
                    }



                }
                else if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (d == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                DialogResult d = MessageBox.Show("Encrycption Done! Exit Application?", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    if (t == 1)
                    {
                        string tempPath = data[2] + "\\temp";
                        if (Directory.Exists(data[2] + "\\temp"))
                        {
                            try
                            {
                                Directory.Delete(data[2] + "\\temp", true);
                                Application.ExitThread();
                            }
                            catch
                            {
                                MessageBox.Show("Unable to close.\nApplication's content is using by another application.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            Application.ExitThread();
                        }
                    }



                }
                else if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (d == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
           
        }
    }
}
