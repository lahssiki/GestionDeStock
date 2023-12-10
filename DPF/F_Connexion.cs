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
    public partial class F_Connexion : Form
    {
        private dbStockContext db;
        private Form frmmenu;
        DPC.Class_Connexion C = new DPC.Class_Connexion();

        public F_Connexion(Form menu)
        {
            InitializeComponent();
            this.frmmenu = menu;
            db = new dbStockContext();
        }
        string testobligatoire()
        {
            if(textBoxUser.Text == ""||textBoxUser.Text == "Nom d'utilisateur")
            {
                return "Entre le Nom d'utilisateur";
            }
            if (textBoxPassword.Text == "" || textBoxPassword.Text == "mot de passe")
            {
                return "Entre votre mot de passe";
            }
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxUser_Enter(object sender, EventArgs e)
        {
            if (textBoxUser.Text== "Nom d'utilisateur")
            {
                textBoxUser.Text = "";
                textBoxUser.ForeColor = Color.Black;

            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "mot de passe")
            {
                textBoxPassword.Text = "";
                textBoxPassword.UseSystemPasswordChar = false;
                textBoxPassword.PasswordChar = '*';
                textBoxPassword.ForeColor = Color.Black;

            }

        }

        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            if (textBoxUser.Text == "")
            {
                textBoxUser.Text = "Nom d'utilisateur";
                textBoxUser.ForeColor = Color.DimGray;

            }

        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "mot de passe";
                textBoxPassword.UseSystemPasswordChar = true;
                textBoxPassword.ForeColor = Color.DimGray;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (testobligatoire() == null)
            {
                if (C.ConnexionValide(db, textBoxUser.Text, textBoxPassword.Text)== true)
                {
                    MessageBox.Show("Connexion réussi", "Connexion", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    (frmmenu as Menu).activerForm();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connexion échoué", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show(testobligatoire(), "obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
