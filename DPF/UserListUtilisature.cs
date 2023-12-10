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
    public partial class UserListUtilisature : UserControl
    {
        private static UserListUtilisature UserUtilisature;
        private dbStockContext db;
        public static UserListUtilisature Instance
        {
            get
            {
                if (UserUtilisature == null)
                {
                    UserUtilisature = new UserListUtilisature();
                }
                return UserUtilisature;
            }
        }
        public UserListUtilisature()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        public void Actualiserdgv()
        {
            db = new dbStockContext();
            dgvUser.Rows.Clear();
            foreach (var U in db.Utilisateurs)
            {
                dgvUser.Rows.Add(false, U.NomUtilisateur, U.mot_de_pass);
            }
        }

            private void btnAjouter_Click(object sender, EventArgs e)
            {
            DPF.FormUser frmU = new FormUser(this);
            frmU.ShowDialog();
            }

        private void UserListUtilisature_Load(object sender, EventArgs e)
        {
            Actualiserdgv();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {/*
            DPC.ClassUSER cluser = new DPC.ClassUSER();
            int select = 0;
            for (int i = 0; i < dgvUser.Rows.Count; i++)
            {
                if ((bool)dgvUser.Rows[i].Cells[0].Value == true)
                {
                    select++;
                }
            }
            if (select == 0)
            {
                MessageBox.Show("aucun user Selectionner");
            }
            else
            {
                DialogResult r =
                    MessageBox.Show("Voulez-vous Vraiment supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvUser.Rows.Count; i++)
                    {
                        if ((bool)dgvUser.Rows[i].Cells[0].Value == true)
                        {
                            cluser.Suprimer_user(int.Parse(dgvUser.Rows[i].Cells[1].Value.ToString()));
                        }
                    }
                    ActualiseGrid();
                    MessageBox.Show("Suppresion réussi");
                }
                else
                {
                    MessageBox.Show("Suppresion Annulé");
                }
            }*/
        }
    }
}
