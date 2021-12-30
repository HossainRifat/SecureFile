using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureFile
{
    public partial class process : Form
    {
        private static process _obj3;
        public static process processInstance
        {
            get
            {
                if (_obj3 == null)
                {
                    _obj3 = new process();
                }
                return _obj3;
            }
        }
        public string PrecessStatus
        {
            set { label1.Text = value; }
        }

        public Timer t
        {
            set { timer1 = value; }
            get { return timer1; }
        }
        public Panel p2
        {
            set { this.panel2 = value; }
            get { return this.panel2; }
        }

        public Label number
        {
            set { this.label6 = value; }
            get { return this.label6; }
        }

        public Label Fname
        {
            set { this.label3 = value; }
            get { return this.label3; }
        }

        public Label Fsize
        {
            set { this.label5 = value; }
            get { return this.label5; }
        }

        public process()
        {
            InitializeComponent();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //panel2.Width += 4;
            //{
            //    if (panel2.Width > 400)
            //    {
            //        timer1.Stop();
            //    }
            //}
        }

        private void process_Load(object sender, EventArgs e)
        {
            _obj3 = this;
        }
    }
}
