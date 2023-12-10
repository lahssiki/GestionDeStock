using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace GestionDeStock.DPF
{
    public partial class UserListCommandes : UserControl
    {
        private static UserListCommandes UserCommandes;
        private dbStockContext db;
        public static UserListCommandes Instance
        {
            get
            {
                if (UserCommandes == null)
                {
                    UserCommandes = new UserListCommandes();
                }
                return UserCommandes;
            }
        }

        public UserListCommandes()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        // Rempir DGV Commande
        public void remplirdata()
        {
            dgvCommandes.Rows.Clear();

            // Afficher Nom et Prenom De Client.
            Client c = new Client();
            string NomPrenom;
            foreach(var lc in db.commandes)
            {
                c = db.Clients.Single(s => s.id_client == lc.id_client);
                NomPrenom = c.Nom_client + " " + c.Prenom_client;

                dgvCommandes.Rows.Add(lc.Id_commande,lc.date_commande,NomPrenom,lc.Total_HT,lc.TVA,lc.TOTAL_TTC);
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DPF.F_Detail_Commande frmCo = new F_Detail_Commande();
            frmCo.ShowDialog();
        }

        private void UserListCommandes_Load(object sender, EventArgs e)
        {
            remplirdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            remplirdata();
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            // recherche entre deux date
            var listComm = db.commandes.ToList(); // list Commandes
            if (dgvCommandes.Rows.Count != 0)
            {
                listComm = listComm.Where(s => s.date_commande.Date >= dateTimeDebut.Value.Date && s.date_commande.Date <= dateTimeFin.Value.Date).ToList();

                // remplir DGV commande
                dgvCommandes.Rows.Clear();
                // Afficher Nom et Prenom De Client.
                Client c = new Client();
                string NomPrenom;
                foreach (var lc in listComm)
                {
                    c = db.Clients.Single(s => s.id_client == lc.id_client);
                    NomPrenom = c.Nom_client + " " + c.Prenom_client;

                    dgvCommandes.Rows.Add(lc.Id_commande, lc.date_commande, NomPrenom, lc.Total_HT, lc.TVA, lc.TOTAL_TTC);
                }
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            DPR.F_Rapport frmrap = new DPR.F_Rapport();
            db = new dbStockContext();
            try
            {
                //verifier 
                // Commande Selectionner
                int idcommande = (int)dgvCommandes.CurrentRow.Cells[0].Value;
                var commande = db.commandes.Single(s => s.Id_commande == idcommande);
                //Client
                var Clinet_commande = db.Clients.Single(s => s.id_client == commande.id_client);
                //Detail Commande 
                var Detail_commande = db.details_commande.Where(s => s.Id_commande == idcommande).ToList();
                //
                //Ajouter list Detail dans DataSource De repport
                frmrap.reportV.LocalReport.ReportEmbeddedResource = "GestionDeStock.DPR.RPT_Commande.rdlc";
                frmrap.reportV.LocalReport.DataSources.Add(new ReportDataSource("DataCommande", Detail_commande));
                // Ajouter les information de Client
                ReportParameter Nom = new ReportParameter("NomClient", Clinet_commande.Nom_client+" "+ Clinet_commande.Prenom_client);
                ReportParameter Adresse = new ReportParameter("AdresseC", Clinet_commande.Adresse_client);
                ReportParameter Telephone = new ReportParameter("TelephoneC", Clinet_commande.Telephone_client);
                // Ajouter info Commande
                ReportParameter NComm = new ReportParameter("IDCommande", idcommande.ToString());
                ReportParameter DateCommande = new ReportParameter("DateCommande", commande.date_commande.ToString());
                // Ajouter ToTal ht /TVA /Total ttc
                ReportParameter totalht = new ReportParameter("Totalht", commande.Total_HT);
                ReportParameter Tva = new ReportParameter("Tva", commande.TVA);
                ReportParameter Totalttc = new ReportParameter("Totalttc", commande.TOTAL_TTC);
                //enregistrer les Valeurs
                frmrap.reportV.LocalReport.SetParameters(new ReportParameter[] { Nom, Adresse, Telephone, NComm, DateCommande, totalht, Tva, Totalttc });
                frmrap.reportV.RefreshReport();
                frmrap.ShowDialog();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
