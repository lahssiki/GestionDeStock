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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            pnlPara.Visible = false;
        }
         public void desacriverForm()
        {
            btnClient.Enabled = false;
            btnProduit.Enabled = false;
            btnCategorie.Enabled = false;
            btnCommande.Enabled = false;
            btnUtilisateur.Enabled = false;
            btnDeconnecter.Enabled = false;
            btnConnecter.Enabled = true;

        }
        public void activerForm()
        {
            btnClient.Enabled = true;
            btnProduit.Enabled = true;
            btnCategorie.Enabled = true;
            btnCommande.Enabled = true;
            btnUtilisateur.Enabled = true;
            btnDeconnecter.Enabled = true;
            btnConnecter.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r =
                   MessageBox.Show("Voulez-vous Vraiment Quitte l'application ..", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {

                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnPara_Click(object sender, EventArgs e)
        {
            pnlPara.Size = new Size(282, 59);
            pnlPara.Visible = !pnlPara.Visible;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            F_Connexion frmC = new F_Connexion(this);
            frmC.ShowDialog();

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            desacriverForm();
        }

        private void btnDeconnecter_Click(object sender, EventArgs e)
        {
            if (!pnlaficher.Controls.Contains(UserDeconnecter.Instance))
            {
                pnlaficher.Controls.Add(UserDeconnecter.Instance);
                UserDeconnecter.Instance.Dock = DockStyle.Fill;
                UserDeconnecter.Instance.BringToFront();

            }
            else
            {
                UserDeconnecter.Instance.BringToFront();

            }
            desacriverForm();
           
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            if (!pnlaficher.Controls.Contains(UserListClient.Instance))
            {
                pnlaficher.Controls.Add(UserListClient.Instance);
                UserListClient.Instance.Dock = DockStyle.Fill;
                UserListClient.Instance.BringToFront();

            }
            else
            {
                UserListClient.Instance.BringToFront();

            }
        }

        private void btnProduit_Click(object sender, EventArgs e)
        {
            if (!pnlaficher.Controls.Contains(UserListProduit.Instance))
            {
                pnlaficher.Controls.Add(UserListProduit.Instance);
                UserListProduit.Instance.Dock = DockStyle.Fill;
                UserListProduit.Instance.BringToFront();

            }
            else
            {
                UserListProduit.Instance.BringToFront();

            }
        }

        private void btnCategorie_Click(object sender, EventArgs e)
        {
            if (!pnlaficher.Controls.Contains(UserListCategorie.Instance))
            {
                pnlaficher.Controls.Add(UserListCategorie.Instance);
                UserListCategorie.Instance.Dock = DockStyle.Fill;
                UserListCategorie.Instance.BringToFront();

            }
            else
            {
                UserListCategorie.Instance.BringToFront();

            }
        }

        private void btnCommande_Click(object sender, EventArgs e)
        {

            if (!pnlaficher.Controls.Contains(UserListCommandes.Instance))
            {
                pnlaficher.Controls.Add(UserListCommandes.Instance);
                UserListCommandes.Instance.Dock = DockStyle.Fill;
                UserListCommandes.Instance.BringToFront();

            }
            else
            {
                UserListCommandes.Instance.BringToFront();

            }
        }

        private void btnUtilisateur_Click(object sender, EventArgs e)
        {
            if (!pnlaficher.Controls.Contains(UserListUtilisature.Instance))
            {
                pnlaficher.Controls.Add(UserListUtilisature.Instance);
                UserListUtilisature.Instance.Dock = DockStyle.Fill;
                UserListUtilisature.Instance.BringToFront();

            }
            else
            {
                UserListUtilisature.Instance.BringToFront();

            }
        }
    }
}
