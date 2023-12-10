using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class Class_Commande__DetailCommande
    {
        private dbStockContext db = new dbStockContext();
        private commande clsCommande;
        private details_commande clsD;
        public int IDCommande;

        // sauvegarder Commande
        public void Ajouter_Commande(DateTime dateCom, int idclient, string totalht, string tva, string totalttc)
        {
            clsCommande = new commande();
            clsCommande.date_commande = dateCom.Date;
            clsCommande.id_client = idclient;
            clsCommande.Total_HT = totalht;
            clsCommande.TVA = tva;
            clsCommande.TOTAL_TTC = totalttc;
            db.commandes.Add(clsCommande);
            db.SaveChanges();
           
            IDCommande = clsCommande.Id_commande;  // <=id commande ajouter

        }
        public void Ajouter_Detail(int idProduit, string NomProduit, int Quant, string prix, string total)
        {
            clsD = new details_commande();
            clsD.Id_commande = IDCommande;
            clsD.Id_produit = idProduit;
            clsD.Nom_Produit = NomProduit;
            clsD.Quantite = Quant;
            clsD.prix = prix;
            clsD.total = total;
            db.details_commande.Add(clsD);
            db.SaveChanges();

        }
    }
}
