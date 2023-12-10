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
    public partial class F_Detail_Commande : Form
    {
        private dbStockContext db;
        
        public F_Detail_Commande()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        ///////////// Remplire DGV de commande par liste///////////////
        public void Actualise_DetailCommande()
        {
         //////// Calcule total ht,tva,Total ttc///////////////////

            float totalht = 0 ,TVA =0,totalttc = 0;
            if(textBoxTVA.Text != "")
            {
                TVA = float.Parse(textBoxTVA.Text);
            }

            dgvDCommande.Rows.Clear();

            foreach(var l in DPC.Class_D_Commande.listDetial)
            {
                dgvDCommande.Rows.Add(l.Id, l.Nom, l.Quantite, l.Prix, l.Total);
                totalht = totalht + float.Parse(l.Total);
            }
            textBoxHT.Text = totalht.ToString();
            // calcul TOTAL TTC
            totalttc = (totalht + (totalht * TVA / 100));
            // Afficher total ttc dans text box.
            textBoxTTC.Text = totalttc.ToString();


            dgvDCommande.Rows.Clear();
            foreach (var l in DPC.Class_D_Commande.listDetial)
            {
                dgvDCommande.Rows.Add(l.Id ,l.Nom, l.Quantite, l.Prix, l.Total);
            }

        }
        //function remplir de produit
        public void rempliredgvP()
        {
            db = new dbStockContext();
            foreach(var l in db.Produits)
            {
                dgvP.Rows.Add(l.Id_produit, l.Nom_produit, l.Quantite_produit, l.Prix_produit);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            // vider La liste Des Produits Commander
            DPC.Class_D_Commande.listDetial.Clear();
        }

        private void F_Detail_Commande_Load(object sender, EventArgs e)
        {
            rempliredgvP();
        }

        private void textBoxRech_TextChanged(object sender, EventArgs e)
        {

            db = new dbStockContext();
            var listerecherche = db.Produits.ToList();

            listerecherche = listerecherche.Where(s => s.Nom_produit.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            dgvP.Rows.Clear();
            foreach (var l in listerecherche)
            {
                dgvP.Rows.Add(l.Id_produit, l.Nom_produit, l.Quantite_produit, l.Prix_produit);
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            DPF.F_Client_Commande frmCl = new F_Client_Commande();
            frmCl.ShowDialog();
            // Afficher les information Client
            IDCLIENT = (int)frmCl.dgvC.CurrentRow.Cells[0].Value;
            textNom.Text = frmCl.dgvC.CurrentRow.Cells[1].Value.ToString();
            textPrenom.Text = frmCl.dgvC.CurrentRow.Cells[2].Value.ToString();
            textTele.Text = frmCl.dgvC.CurrentRow.Cells[3].Value.ToString();
            textVille.Text = frmCl.dgvC.CurrentRow.Cells[4].Value.ToString();
            textPays.Text = frmCl.dgvC.CurrentRow.Cells[5].Value.ToString();
        }

        private void dgvP_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            F_Produit_Commnde frmP = new F_Produit_Commnde(this);
            // si le stock vide 
            if ((int)dgvP.CurrentRow.Cells[2].Value == 0)
            {
                MessageBox.Show("Stock Vide");
            }
            else
            {
                // afficher les information de produit
                frmP.labelid.Text = dgvP.CurrentRow.Cells[0].Value.ToString();
                frmP.labelN.Text = dgvP.CurrentRow.Cells[1].Value.ToString();
                frmP.labelS.Text = dgvP.CurrentRow.Cells[2].Value.ToString();
                frmP.labelP.Text = dgvP.CurrentRow.Cells[3].Value.ToString();
                frmP.textbTotal.Text = dgvP.CurrentRow.Cells[3].Value.ToString();
                frmP.ShowDialog();
            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Produit_Commnde frm = new F_Produit_Commnde(this);
            Produit pr = new Produit();
            if (dgvDCommande.CurrentRow != null)
            {
                frm.groupProduit.Text = "Modifier Produit";

                //Afficher information de produit modifier
                frm.labelid.Text = dgvDCommande.CurrentRow.Cells[0].Value.ToString();
                frm.labelN.Text = dgvDCommande.CurrentRow.Cells[1].Value.ToString();

                // importer Stock de Produit
                int idP = int.Parse(dgvDCommande.CurrentRow.Cells[0].Value.ToString());
                pr = db.Produits.Single(s => s.Id_produit == idP);
                frm.labelS.Text = pr.Quantite_produit.ToString();

                frm.labelP.Text = dgvDCommande.CurrentRow.Cells[3].Value.ToString();
                frm.textbQuantite.Text = dgvDCommande.CurrentRow.Cells[2].Value.ToString();
                frm.textbTotal.Text = dgvDCommande.CurrentRow.Cells[4].Value.ToString();
                frm.ShowDialog();
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDCommande.CurrentRow != null)
            {
                DialogResult pr = MessageBox.Show("Voulez-vous Variment Supprimer", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(pr == DialogResult.Yes)
                {
                    //supprimer Produit Commande dans la liste 
                    int index = DPC.Class_D_Commande.listDetial.FindIndex(s => s.Id == int.Parse(dgvDCommande.CurrentRow.Cells[0].Value.ToString()));
                    DPC.Class_D_Commande.listDetial.RemoveAt(index);
                    //Actualiser DGV
                    Actualise_DetailCommande();
                    MessageBox.Show("Suppression Succes");
                }
                else
                {
                    MessageBox.Show("Suppression Annulé");

                }
            }
        }

        private void textBoxTVA_TextChanged(object sender, EventArgs e)
        {
            // CALCUL TTC
            Actualise_DetailCommande();
        }
        public int IDCLIENT;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            DPC.Class_Commande__DetailCommande ClsCommande = new DPC.Class_Commande__DetailCommande();
            //Verifier DGV Vide
            if(dgvDCommande.Rows.Count == 0)
            {
                MessageBox.Show("Ajouter des Produit !");
            }
            else
            {
                if (textNom.Text == "")
                {
                    MessageBox.Show("Ajouter un Client !");
                }
                else
                {
                    // enregister Commande
                    ClsCommande.Ajouter_Commande(dateTimeCom.Value, IDCLIENT, textBoxHT.Text, textBoxTVA.Text, textBoxTTC.Text);

                    // enregister Liste Detail commande dans base donnes
                    foreach(var DC in DPC.Class_D_Commande.listDetial)
                    {
                        ClsCommande.Ajouter_Detail(DC.Id, DC.Nom, DC.Quantite, DC.Prix, DC.Total);



                    }
                    MessageBox.Show("Commande Ajouter Avec Succes");

                }
            }
            
        }

        private void textBoxTTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPays_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
