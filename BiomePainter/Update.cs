using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BiomePainter
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
            this.AcceptButton = btnClose;
            this.CancelButton = btnClose;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
