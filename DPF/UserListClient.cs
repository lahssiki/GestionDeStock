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
    public partial class UserListClient : UserControl
    {
        private static UserListClient UserClient;
        private dbStockContext db;
        public static UserListClient Instance
        {
            get
            {
                if (UserClient == null)
                {
                    UserClient = new UserListClient();
                }
                return UserClient;
            }
        }

        public UserListClient()
        {
            InitializeComponent();
            db = new dbStockContext();
            textBoxRech.Enabled = false; //=> desactive textbox recherche 
           
        }
        public void ActualiseGrid()
        {
            dgvclient.Rows.Clear();
            foreach(var S in db.Clients)
            {
                dgvclient.Rows.Add(false,S.id_client,S.Nom_client,S.Prenom_client,S.Adresse_client,S.Telephone_client,S.Ville_client,S.pays_client);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DPF.F_Ajouter_Modifier frmClient = new F_Ajouter_Modifier(this);
            frmClient.ShowDialog();
        }

        private void UserListClient_Load(object sender, EventArgs e)
        {
            ActualiseGrid();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            DPC.Class_Client clclient = new DPC.Class_Client();
            int select = 0;
            for(int i = 0; i < dgvclient.Rows.Count; i++)
            {
                if ((bool)dgvclient.Rows[i].Cells[0].Value == true)
                {
                    select++;
                }
            }
            if (select == 0)
            {
                MessageBox.Show("aucun client Selectionner");
            }
            else
            {
                DialogResult r =
                    MessageBox.Show("Voulez-vous Vraiment supprimer", "Suppresion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    for (int i = 0; i < dgvclient.Rows.Count; i++)
                    {
                        if ((bool)dgvclient.Rows[i].Cells[0].Value == true)
                        {
                            clclient.Suprimer_Client(int.Parse(dgvclient.Rows[i].Cells[1].Value.ToString()));
                        }
                    }
                    ActualiseGrid();
                    MessageBox.Show("Suppresion réussi");
                }
                else
                {
                    MessageBox.Show("Suppresion Annulé");
                }
            }
        }

        private void comboRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxRech.Enabled = true;
            textBoxRech.Text = "";

        }

        private void textBoxRech_TextChanged(object sender, EventArgs e)
        {
            db = new dbStockContext();
            var listerecherche = db.Clients.ToList(); // => lists clients
            if (textBoxRech.Text != "")
            {
                switch (comboRecherche.Text)
                {
                    case "Nom":
                        listerecherche = listerecherche.Where(s => s.Nom_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        //=> la première lettre en majusculle ou en minusculle | (!= -1) existe dans la base donnée
                        break;
                    case "Prenom":
                        listerecherche = listerecherche.Where(s => s.Prenom_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        break;
                    case "Adresse":
                        listerecherche = listerecherche.Where(s => s.Adresse_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        break;
                    case "Telephone":
                        listerecherche = listerecherche.Where(s => s.Telephone_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        break;
                    case "Ville":
                        listerecherche = listerecherche.Where(s => s.Ville_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        break;
                    case "Pays":
                        listerecherche = listerecherche.Where(s => s.pays_client.IndexOf(textBoxRech.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                        break;
                }
            }
            //=> vider DGV Client
            dgvclient.Rows.Clear();
            //=> Ajouter listRecherche dans DGV Client
            foreach(var l in listerecherche)
            {
                dgvclient.Rows.Add(false, l.id_client, l.Nom_client, l.Prenom_client, l.Adresse_client, l.Telephone_client, l.Ville_client, l.pays_client);
            }


        }
        public string SelectVerif()
        {
            int NombrelingeSelect = 0;
            for (int i = 0; i < dgvclient.Rows.Count; i++)
            {
                if ((bool)dgvclient.Rows[i].Cells[0].Value == true)// => si linge est selectionner
                {
                    NombrelingeSelect++; //=> Nombrelinge +1

                }
            }
            if (NombrelingeSelect == 0)
            {
                return "Selectionner Client";
            }
            if (NombrelingeSelect > 1)
            {
                return "Selectionner Seulement 1 Client";
            }
            return null;

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {

            if (SelectVerif() != null)
            {
                MessageBox.Show(SelectVerif());
            }
            else
            {
                DPF.F_Ajouter_Modifier frmClient = new F_Ajouter_Modifier(this);
                frmClient.labelA.Text = "Modifier Client";
                frmClient.btnActualiser.Visible = false;
                for (int i = 0; i < dgvclient.Rows.Count; i++) //=> Verifier ligne selectionner
                {
                    if ((bool)dgvclient.Rows[i].Cells[0].Value == true) // => si linge est selectionner
                    {
                        frmClient.textNom.Text = dgvclient.Rows[i].Cells[2].Value.ToString();
                        frmClient.textPrenom.Text = dgvclient.Rows[i].Cells[3].Value.ToString();
                        frmClient.textAdresse.Text = dgvclient.Rows[i].Cells[4].Value.ToString();
                        frmClient.textTelephone.Text = dgvclient.Rows[i].Cells[5].Value.ToString();
                        frmClient.textVille.Text = dgvclient.Rows[i].Cells[6].Value.ToString();
                        frmClient.textPays.Text = dgvclient.Rows[i].Cells[7].Value.ToString();
                        frmClient.IDCLIENT = (int)dgvclient.Rows[i].Cells[1].Value;
                    }
                }

                frmClient.ShowDialog();
            }
        }
    }
}
