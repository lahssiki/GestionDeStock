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
    public partial class F_Produit_Commnde : Form
    {
        public Form frmdetail;
        public F_Produit_Commnde(Form frm)
        {
            InitializeComponent();
            frmdetail = frm;
        }

        private void textbQuantite_KeyPress(object sender, KeyPressEventArgs e)
        {
            // text num
            if (!char.IsDigit(e.KeyChar)&& e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

       // public int Quantite_produit;
        private void textbQuantite_TextChanged(object sender, EventArgs e)
        {
            // Verifier Stock 
            if (textbQuantite.Text != "")
            {
                int quan = int.Parse(textbQuantite.Text);
                int prix = int.Parse(labelP.Text);
                if (int.Parse(textbQuantite.Text) > int.Parse(labelS.Text))
                {
                    MessageBox.Show("stock actuel " + int.Parse(labelS.Text),"STOCK",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    //pour vide text box Quantite
                    textbQuantite.Text = "";
                }
                else
                {
                    // Calcul total
                    textbTotal.Text = (quan * prix).ToString();
                }
            }
            else
            {
                textbTotal.Text = labelP.Text;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            // text box Quantité Vide
            int quant;
            if(textbQuantite.Text != "")
            {
                quant = int.Parse(textbQuantite.Text);
            }
            else
            {
                quant = 1;
            }

            // Ajouter Produit Dans Commande
            DPC.Class_D_Commande Detail = new DPC.Class_D_Commande
            {
                Id = int.Parse(labelid.Text),
                Nom = labelN.Text,
                Quantite = quant,
                Prix = labelP.Text,
                Total = textbTotal.Text,

            };
            // Ajouter Dans liste
            if(groupProduit.Text == "Vendre Produit")
            {
                if (DPC.Class_D_Commande.listDetial.SingleOrDefault(s => s.Id == Detail.Id) != null)
                {
                    MessageBox.Show("Produit deja existe dans Commandes", "Produit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DPC.Class_D_Commande.listDetial.Add(Detail);
                }

            }
            else
            {
                //Modifier liste

                DialogResult pr = MessageBox.Show("Voulez-Vous Vraiment Modifier", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pr == DialogResult.Yes)
                {
                    //trouver index de produit modifier
                    int index = DPC.Class_D_Commande.listDetial.FindIndex(s => s.Id == int.Parse(labelid.Text));
                    DPC.Class_D_Commande.listDetial[index] = Detail;
                    MessageBox.Show("Modification succes");
                    Close();
                }
                else
                {
                    MessageBox.Show("Modification Annulé");
                }
            }
           
            // actualiser DGV
            (frmdetail as F_Detail_Commande).Actualise_DetailCommande();
            Close();
            
        }


    }
}
