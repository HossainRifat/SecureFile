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

namespace SecureFile
{
    public partial class Form1 : Form
    {
        private static List<string> importData = new List<string>();
        private static Form1 _obj;
        public List<string> data = new List<string>();

        public static Form1 Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new Form1();
                }
                return _obj;
            }
        }

        public FlowLayoutPanel f2mainpanel
        {
            set { flowLayoutPanel1 = value; }
            get { return flowLayoutPanel1; }
        }

        public Panel p7
        {
            set { this.panel7 = value; }
            get { return this.panel7; }
        }

        public Panel p1
        {
            set { this.panel1 = value; }
            get { return this.panel1; }
        }

        public Panel p2
        {
            set { this.panel3 = value; }
            get { return this.panel3; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _obj = this;
            this.panel1.Visible = false;
            this.panel3.Visible = false;
            this.panel3.Size = new System.Drawing.Size(1063, 33);
            this.f2mainpanel.Size = new System.Drawing.Size(1062, 689);
            


            this.getData();
        }

        public void getData()
        {
            string filePath = @"E:\USER\Documents\SecureFileData.txt";
            if (!File.Exists(filePath))
            {
                login l = new login();
                l.g1.Visible = false;
                l.g2.Visible = true;
                
                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(l);
            }
            else
            {
                data = File.ReadAllLines(filePath).ToList();
                login l = new login();
                l.g2.Visible = false;
                l.g1.Visible = true;
                l.Hints.Text = "Hints: " + this.data[1];
                this.flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.Controls.Add(l);
                
            }
            ////string a = "E:\\USER\\Downloads\\icons8-bell-488.png.SFile";
            //string b = "E:\\USER\\Downloads\\icons8-bell-488.png";
            //Crypto.FileEncrypt(b, data[2]+ "//Encrypted", "1234");
            ////Crypto.FileDecrypt(a, b, "1234");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.p7.Visible = false;
            this.panel3.Size = new System.Drawing.Size(1063, 33);
            this.f2mainpanel.Size = new System.Drawing.Size(1062, 689);

            string[] filePaths = Directory.GetFiles(@"E:\5.SECURE FOLDER\Encrypted", "*.SFile", SearchOption.TopDirectoryOnly);
            FileList[] f = new FileList[filePaths.Length];

            Form1.Instance.f2mainpanel.Controls.Clear();

            for (int i = 0; i < filePaths.Length; i++)
            {
                f[i] = new FileList();
                f[i].FileName = Path.GetFileName(filePaths[i]);
                f[i].FilePath = filePaths[i];
                f[i].FileSize = new FileInfo(filePaths[i]).Length.ToString();
                f[i].FileType = Path.GetExtension(filePaths[i]);
                f[i].Operation = "open";
                Form1.Instance.f2mainpanel.Controls.Add(f[i]);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = this.data[2] + "\\temp";
            if (Directory.Exists(output))
            {
                Directory.Delete(output);
            }

            this.panel1.Visible = false;
            this.panel3.Visible = false;
            login l = new login();
            l.g2.Visible = false;
            l.g1.Visible = true;
            this.flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanel1.Controls.Add(l);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.Instance.f2mainpanel.Controls.Clear(); 
            this.panel3.Size = new System.Drawing.Size(1063, 80);
            this.f2mainpanel.Size = new System.Drawing.Size(1062, 645);
            
            this.p7.Visible = true;
            openFileDialog1.FileName = null;
            openFileDialog1.Multiselect = true;
            openFileDialog1.InitialDirectory = "E:\\USER\\videos";
            openFileDialog1.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;" +
                "*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;" +
                "*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;" +
                "*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;" +
                "*.MIDI;*.RMI;*.MKV";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = openFileDialog1.FileNames;
                if(importData.Count < 1)
                {
                    importData = filePaths.ToList();
                }
                else
                {
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        importData.Add(filePaths[i]);
                    }
                }
                filePaths = importData.ToArray();
                FileList[] f = new FileList[filePaths.Length];

                Form1.Instance.f2mainpanel.Controls.Clear();

                for (int i = 0; i < filePaths.Length; i++)
                {
                    f[i] = new FileList();
                    f[i].FileName = Path.GetFileName(filePaths[i]);
                    f[i].FilePath = filePaths[i];
                    f[i].FileSize = new FileInfo(filePaths[i]).Length.ToString();
                    f[i].FileType = Path.GetExtension(filePaths[i]);
                    f[i].Operation = "import";
                    Form1.Instance.f2mainpanel.Controls.Add(f[i]);
                }
            }
            else
            {
                if(importData.Count > 0)
                {
                    string[] filePaths = importData.ToArray();
                    FileList[] f = new FileList[filePaths.Length];

                    Form1.Instance.f2mainpanel.Controls.Clear();

                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        f[i] = new FileList();
                        f[i].FileName = Path.GetFileName(filePaths[i]);
                        f[i].FilePath = filePaths[i];
                        f[i].FileSize = new FileInfo(filePaths[i]).Length.ToString();
                        f[i].FileType = Path.GetExtension(filePaths[i]);
                        f[i].Operation = "encrypt";
                        Form1.Instance.f2mainpanel.Controls.Add(f[i]);
                    }
                }
            }
            
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            importData.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (importData.Count > 0)
            {
                process p = new process();
                p.number.Visible = true;
                p.number.Text = "0/" + importData.Count.ToString();
                p.p2.Width = 0;
                p.t.Enabled = false;
                int a = 400 / (importData.Count+1);
                MessageBox.Show(a.ToString());
                for (int i = 0; i < importData.Count; i++)
                {
                    p.Fname.Text = Path.GetFileName(importData[i]);
                    p.Fsize.Text = new FileInfo(importData[i]).Length.ToString() + " bytes";
                    p.number.Text = (i+1).ToString() + "/" + importData.Count.ToString();
                    if(i+1 == importData.Count)
                    {
                        p.p2.Width = 421;
                    }
                    else
                    {
                        p.p2.Width += a;
                    }

                    p.Visible = true;
                    string output = Form1.Instance.data[2] + "\\Encrypted";
                    try
                    {
                        Crypto.FileEncrypt(importData[i], output, "1234");
                    }
                    catch
                    {
                        MessageBox.Show("Something went wrong.\nError Code: flistopen.");
                    }
                        
                }
                
                p.Visible = false;
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d = MessageBox.Show("Exit Application?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if(d == DialogResult.Yes)
            {
                string tempPath = data[2] + "\\temp";
                if (Directory.Exists(tempPath))
                {
                    try
                    {
                        Directory.Delete(tempPath, true);
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
            else if(d == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if(d == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
    }
}
