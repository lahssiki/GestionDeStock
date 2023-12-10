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
    public partial class FormUser : Form
    {
        private UserControl UserLU;
        public FormUser (UserControl userU)
        
        {
            InitializeComponent();
            this.UserLU = userU;
        }
        
        string testObligatoire() //=> test Obligatoire
        {
            if (textUser.Text == "")
            {
                return "Username !";
            }
            if (textmot2pass.Text == "")
            {
                return "Password ! ";
            }

            return null;
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (testObligatoire() != null)
            {
                MessageBox.Show(testObligatoire());
            }
           
            else
            {
                DPC.ClassUSER clsUser = new DPC.ClassUSER();
                if (clsUser.Ajouter_USER(textUser.Text, textmot2pass.Text) == true)
                {
                    MessageBox.Show("Ajoute reussi");
                    (UserLU as UserListUtilisature).Actualiserdgv();
                }
                else
                {
                    MessageBox.Show("Client déja existant");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
