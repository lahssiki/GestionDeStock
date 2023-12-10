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
    public partial class F_Ajouter_Modifier : Form
    {
        private UserControl usclient;
        public F_Ajouter_Modifier(UserControl userC)
        {
            InitializeComponent();
            this.usclient = userC;
        }
        string testObligatoire()
        {
            if(textNom.Text == "")
            {
                return "entre le Nom de Client";
            }
            if (textPrenom.Text == "")
            {
                return "entre le Prenom de Client";
            }
            if (textAdresse.Text == "")
            {
                return "entre la Adresse de Client";
            }
            if (textTelephone.Text == "")
            {
                return "entre le Telephone de Client";
            }
            if (textVille.Text == "")
            {
                return "entre le Ville de Client";
            }
            return null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar<48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (testObligatoire() != null)
            {
                MessageBox.Show(testObligatoire());

            }else
            {
                if (labelA.Text == "Ajouter Client")
                {
                    DPC.Class_Client clclient = new DPC.Class_Client();
                    if (clclient.Ajouter_Client(textNom.Text, textPrenom.Text, textAdresse.Text, textTelephone.Text, textVille.Text, textPays.Text) == true)
                    {
                        MessageBox.Show("Ajoute reussi");
                        (usclient as UserListClient).ActualiseGrid();
                    }
                    else
                    {
                        MessageBox.Show("Client déja existant");
                    }
                }                
                else // modifier
                {
                    DPC.Class_Client clsclient = new DPC.Class_Client();
                    DialogResult rs = MessageBox.Show("Voulez-Vous vriament Modifier", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        clsclient.Modifier_Client(IDCLIENT, textNom.Text, textPrenom.Text, textAdresse.Text, textTelephone.Text, textVille.Text, textPays.Text);
                        MessageBox.Show("Client modifier avec succes");
                        //=> Actualiser datagridV
                        (usclient as UserListClient).ActualiseGrid();
                        Close(); //=> pour quitte form modifier

                    }
                    else
                    {
                        MessageBox.Show("modification annulé");
                    }
                }
            }
        }
        public int IDCLIENT;

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            textNom.Text = "";
            textPrenom.Text = "";
            textAdresse.Text = "";
            textTelephone.Text = "";
            textVille.Text = "";
            textPays.Text = "";
        }

        private void F_Ajouter_Modifier_Load(object sender, EventArgs e)
        {

        }
    }
}
