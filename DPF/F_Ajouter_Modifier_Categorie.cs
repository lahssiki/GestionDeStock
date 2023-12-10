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
    public partial class F_Ajouter_Modifier_Categorie : Form
    {
        private UserControl usercat;
        public F_Ajouter_Modifier_Categorie(UserControl usercategorie)
        {
            InitializeComponent();
            this.usercat = usercategorie;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            DPC.Class_Categorie clscat = new DPC.Class_Categorie();
            if(textNomCategorie.Text == "")
            {
                MessageBox.Show("Entrer Nom de Categorie");
            }
            else
            {
                if (clscat.Ajouter_Categorie(textNomCategorie.Text) == false)
                {
                    MessageBox.Show("Nom de Categorie deja existe");
                }
                else
                {
                    MessageBox.Show("Categorie ajouter avec succes");
                    (usercat as UserListCategorie).remplirdatagrid();
                }
            }
        }
    }
}
