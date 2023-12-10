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
    public partial class UserListCategorie : UserControl
    {
        private static UserListCategorie UserCategorie;
        private dbStockContext db;
        // => creer un instance pour UserControle
        public static UserListCategorie Instance
        {
            get
            {
                if (UserCategorie == null)
                {
                    UserCategorie = new UserListCategorie();
                }
                return UserCategorie;
            }
        }
        public UserListCategorie()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        public void remplirdatagrid() // remplir data Grid Viwe
        {
            db = new dbStockContext();
            dgvCategorie.Rows.Clear();
            foreach (var Cat in db.categories)
            {
                dgvCategorie.Rows.Add(false, Cat.Id_categorie, Cat.Nom_categorie);
            }
        }

        private void UserListCategorie_Load(object sender, EventArgs e)
        {
            remplirdatagrid();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DPF.F_Ajouter_Modifier_Categorie frmcat = new DPF.F_Ajouter_Modifier_Categorie(this);
            frmcat.ShowDialog();
            
            
        }

        private void dgvCategorie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorie.Columns[e.ColumnIndex].Name == "Supprimer")
            {
                DPC.Class_Categorie clscat = new DPC.Class_Categorie();
                DialogResult r = MessageBox.Show("supprimer categorie ?", "suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(r ==DialogResult.Yes)
                {
                    //=> Verifier si il y a des Produits dans cette categorie
                    int id = (int)dgvCategorie.Rows[e.RowIndex].Cells[1].Value;
                    int P = db.Produits.Count(s => s.Id_categorie == id); // Count = combein de produit dans categorie
                    if (P == 0)//=>aucun produit dans categorie 
                    {
                        clscat.Suprimer_Categorie(id);
                        remplirdatagrid(); // <= Actualiser data grid
                        MessageBox.Show("Categorie supprimer avec succes");   
                    }
                    else
                    {
                        //=> il ya des produits dans categorie 
                        DialogResult DP = MessageBox.Show("il ya "+P+"Proudit dans cette categorie Voulez-Vous Vaiment Supprimer", "Suppresion" ,MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                        if (DP== DialogResult.Yes)
                        {
                            clscat.Suprimer_Categorie(id);
                            clscat.Suprimer_Categorie(id);
                            remplirdatagrid(); // <= Actualiser data grid
                            MessageBox.Show("Suppresion ressui");
                        }
                        else
                        {
                            MessageBox.Show("Suppresion Annullé");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Suppresion Annullé");
                }
            }
        }

        private void textBoxRech_TextChanged(object sender, EventArgs e)
        {
            var listcat = db.categories.ToList();
            listcat = listcat.Where(s => s.Nom_categorie.IndexOf(textRechcat.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            dgvCategorie.Rows.Clear();
            foreach(var l in listcat)
            {
                dgvCategorie.Rows.Add(false, l.Id_categorie, l.Nom_categorie);
            }
        }
    }
}
