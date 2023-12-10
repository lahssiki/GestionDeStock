using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeStock.DPR
{
    public partial class F_Rapport : Form
    {
        public F_Rapport()
        {
            InitializeComponent();
        }

        private void F_Rapport_Load(object sender, EventArgs e)
        {

            this.reportV.RefreshReport();
        }
    }
}
