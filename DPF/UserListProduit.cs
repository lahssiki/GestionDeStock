using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeStock.DPF
{
    public partial class UserListProduit : UserControl
    {
        private static UserListProduit UserProduit;
        private dbStockContext db;
        public static UserListProduit Instance
        {
            get
            {
                if (UserProduit == null)
                {
                    UserProduit = new UserListProduit();
                }
                return UserProduit;
            }
        }
        public UserListProduit()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        public void Actualiserdgv()
        {
            db = new dbStockContext();
            dgvProduit.Rows.Clear();
            //=> afficher le nom categorie a partier de id_categorie
            categorie cat = new categorie();
            foreach(var lis in db.Produits)
            {
                cat = db.categories.SingleOrDefault(s => s.Id_categorie == lis.Id_categorie); 
                if(cat!=null)//=> existe
                {
                    dgvProduit.Rows.Add(false, lis.Id_produit, lis.Nom_produit, lis.Quantite_produit, lis.Prix_produit, cat.Nom_categorie);//=> cat.nom_ctegorire = afficher nom categorie
                }
            }
           
        }
        //=> Verifier Combien de linge est selectionner
        public string SelectVerif()
        {
            int NombrelingeSelect = 0;
            for (int i = 0; i < dgvProduit.Rows.Count; i++)
            {
                if((bool)dgvProduit.Rows[i].Cells[0].Value == true)// => si linge est selectionner
                {
                    NombrelingeSelect++; //=> Nombrelinge +1

                }
            }
            if(NombrelingeSelect == 0)
            {
                return "Selectionner Produit";
            }
            if(NombrelingeSelect > 1)
            {
                return "Selectionner Seulement 1 Produit";
            }
            return null;
  
        }

        private void UserListProduit_Load(object sender, EventArgs e)
        {
            Actualiserdgv();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DPF.F_Ajouter_Modifier_Produit frmProduit = new F_Ajouter_Modifier_Produit(this);
            frmProduit.ShowDialog();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (SelectVerif()!= null)
            {
                MessageBox.Show(SelectVerif());
            }
            else
            {
                DPF.F_Ajouter_Modifier_Produit frmproduit = new F_Ajouter_Modifier_Produit(this);
                frmproduit.labelP.Text = "Modifier Produit";
                frmproduit.btnActu.Visible = false;
                for(int i = 0; i < dgvProduit.Rows.Count; i++) //=> Verifier ligne selectionner
                {
                    if((bool)dgvProduit.Rows[i].Cells[0].Value == true) // => si linge est selectionner
                    {
                        frmproduit.comboCategorie.Text = dgvProduit.Rows[i].Cells[5].Value.ToString();
                        frmproduit.textBoxProduit.Text = dgvProduit.Rows[i].Cells[2].Value.ToString();
                        frmproduit.textBoxQuantité.Text = dgvProduit.Rows[i].Cells[3].Value.ToString();
                        frmproduit.textBoxPrix.Text = dgvProduit.Rows[i].Cells[4].Value.ToString();
                        frmproduit.IDPRODUIT =(int)dgvProduit.Rows[i].Cells[1].Value;
                    }
                }

                frmproduit.ShowDialog();
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            DPC.Class_Produit clproduit = new DPC.Class_Produit();
            int select = 0;
            for (int i = 0; i < dgvProduit.Rows.Count; i++)
            {
                if ((bool)dgvProduit.Rows[i].Cells[0].Value == true)
                {
                    select++;
                }
            }
            if (select == 0)
            {
                MessageBox.Show("aucun Produit Selectionner");
            }
            else
            {
                DialogResult r =
                    MessageBox.Show("Voulez-vous Vraiment supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvProduit.Rows.Count; i++)
                    {
                        if ((bool)dgvProduit.Rows[i].Cells[0].Value == true)
                        {
                            clproduit.Supprimer_Produit(int.Parse(dgvProduit.Rows[i].Cells[1].Value.ToString()));
                        }
                    }
                    Actualiserdgv();
                    MessageBox.Show("Suppresion réussi");
                }
                else
                {
                    MessageBox.Show("Suppresion Annulé");
                }
            }

        }

        private void textBoxRech_TextChanged(object sender, EventArgs e)
        {

            db = new dbStockContext();
            var listerecherche = db.Produits.ToList(); // => lists Produits 

            listerecherche = listerecherche.Where(s => s.Nom_produit.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            //=> la première lettre en majusculle ou en minusculle | (!= -1) existe dans la base donnée
             //<== vider DGV Client
            dgvProduit.Rows.Clear();
            categorie cat = new categorie();
            
            foreach (var l in listerecherche)    //<== Ajouter listRecherche dans DGV Client
            {
                cat = db.categories.SingleOrDefault(s => s.Id_categorie == l.Id_categorie); // pour Afficher nom categorie
                dgvProduit.Rows.Add(false,l.Id_produit,l.Nom_produit,l.Quantite_produit,l.Prix_produit,cat.Id_categorie);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Actualiserdgv();
        }
    }
}
