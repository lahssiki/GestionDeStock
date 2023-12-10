using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class Class_Client
    {
        private dbStockContext db = new dbStockContext();
        private Client C;

        public bool Ajouter_Client(string Nom,string Prenom,string Adresse,string Telephone,string Ville,string Pays)
        {
            C = new Client();
            C.Nom_client = Nom;
            C.Prenom_client = Prenom;
            C.Adresse_client = Adresse;
            C.Telephone_client = Telephone;
            C.Ville_client = Ville;
            C.pays_client = Pays;
            //=> Verifier Client déja existe
            if (db.Clients.SingleOrDefault(s => s.Nom_client == Nom && C.Prenom_client == Prenom)==null)   //=> n'existe pas 
            {
                db.Clients.Add(C);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Suprimer_Client(int id)
        {
            C = new Client();
            C = db.Clients.SingleOrDefault(s => s.id_client == id);
            if (C != null)
            {
                db.Clients.Remove(C);
                db.SaveChanges();
            }
        }
        public void Modifier_Client(int idC, string Nom, string Prenom, string Adresse, string Telephone, string Ville, string Pays)
        {
            
            C = new Client();
            C = db.Clients.SingleOrDefault(s => s.id_client == idC);// verifier id_produit 
            if (C != null) // =>id_produit existe
            {
               
                C.Nom_client = Nom;
                C.Prenom_client = Prenom;
                C.Adresse_client = Adresse;
                C.Telephone_client = Telephone;
                C.Ville_client = Ville;
                C.pays_client = Pays;
                db.SaveChanges();
            }
        }
    }
}
