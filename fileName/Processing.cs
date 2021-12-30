using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fileName
{
    public partial class Processing : Form
    {
        private string status;
        private static Processing _obj2;

        public static Processing progressInstance
        {
            get
            {
                if (_obj2 == null)
                {
                    _obj2 = new Processing();
                }
                return _obj2;
            }
        }

        public string PStatus
        {
            set { this.label1.Text = value;  this.status = value; }
        }

        public Panel p2
        {
            set { this.panel2 = value; }
            get { return this.panel2; }
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

        public Processing()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 4;
            {
                if (panel2.Width > 400)
                {
                    timer1.Stop();
                }
            }

        }

        private void Processing_Load(object sender, EventArgs e)
        {
            _obj2 = this;
        }
    }
}
