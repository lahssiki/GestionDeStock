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
    public partial class F_Ajouter_Modifier_Produit : Form
    {
        private dbStockContext db;
        private UserControl userProduit;
        public F_Ajouter_Modifier_Produit(UserControl user)
        {
            InitializeComponent();
            db = new dbStockContext();
            this.userProduit = user;
           // => afficher les categorie dans Compo
            comboCategorie.DataSource = db.categories.ToList();  //=> pour filltrer selment les nom de Categories
            comboCategorie.DisplayMember = "Nom_categorie";       
            comboCategorie.ValueMember = "Id_categorie";
        }
        string testObligatoire() //=> test Obligatoire
        {
            if (comboCategorie.Text == "")
            {
                return "entre la Categorie de Produit";
            }
            if (textBoxProduit.Text == "")
            {
                return "entre le Nom de Produit";
            }
            if (textBoxQuantité.Text == "")
            {
                return "entre la Quantité de Produit";
            }
            if (textBoxPrix.Text == "")
            {
                return "entre le Prix de Produit";
            }
           
            
            return null;
        
        }

        private void F_Ajouter_Modifier_Produit_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActu_Click(object sender, EventArgs e)
        {
            textBoxProduit.Text = "";
            textBoxQuantité.Text = "";
            textBoxPrix.Text = "";
            comboCategorie.Text="";
        }

        private void textBoxQuantité_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57) //=> textbox num
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        public int IDPRODUIT;

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (testObligatoire() != null)
            {
                MessageBox.Show(testObligatoire());
            }
            else
            {
                if(labelP.Text == "Ajouter Produit")
                {

                    DPC.Class_Produit clsproduit = new DPC.Class_Produit();
                    if (clsproduit.Ajouter_Produit(textBoxProduit.Text, int.Parse(textBoxQuantité.Text), textBoxPrix.Text, Convert.ToInt32(comboCategorie.SelectedValue))== true) 
                    {
                        MessageBox.Show("Produit ajouter avec Succes");
                        (userProduit as UserListProduit).Actualiserdgv();

                    }
                    else
                    {
                        MessageBox.Show("Existe déja");
                    }
                }
                else // modifier
                {
                    DPC.Class_Produit clsProduit = new DPC.Class_Produit();
                    DialogResult rs = MessageBox.Show("Voulez-Vous vriament Modifier","Modification",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        clsProduit.Modifier_Produit(IDPRODUIT, textBoxProduit.Text, int.Parse(textBoxQuantité.Text), textBoxPrix.Text, Convert.ToInt32(comboCategorie.SelectedValue));
                        MessageBox.Show("Produit modifier avec succes");
                        //=> Actualiser datagridV
                        (userProduit as UserListProduit).Actualiserdgv();
                        Close(); //=> pour quitte form modifier

                    }
                    else
                    {
                        MessageBox.Show("modification annulé");
                    }
                }
            }
        }
    }
}
