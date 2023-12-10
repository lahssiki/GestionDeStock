using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeStock.DPF
{
    public partial class F_Client_Commande : Form
    {
        private dbStockContext db;
        public F_Client_Commande()
        {
            InitializeComponent();
            db = new dbStockContext();
        }

        private void F_Client_Commande_Load(object sender, EventArgs e)
        {// remplire Data grid View => liste Clients
            foreach(var lC in db.Clients)
            {
                dgvC.Rows.Add(lC.id_client,lC.Nom_client,lC.Prenom_client, lC.Telephone_client,lC.Ville_client,lC.pays_client);
            }

        }

        private void dgvC_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
