using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class Class_Produit
    {
        private dbStockContext db = new dbStockContext();
        private Produit pr;
        // => Ajouter Produit
        public bool Ajouter_Produit(string NomP, int Quantite, string prix,int idCategorie)
        {
            pr = new Produit();
            pr.Nom_produit = NomP;
            pr.Quantite_produit = Quantite;
            pr.Prix_produit = prix;
            pr.Id_categorie = idCategorie;
            //=>Verifier Produit déja existe 
            if(db.Produits.SingleOrDefault(a => a.Nom_produit ==NomP)== null)
            {
                db.Produits.Add(pr);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }

        }
        public void Modifier_Produit(int idP,string NomP, int Quantite, string prix, int idCategorie)
        {
            pr = new Produit();
            pr = db.Produits.SingleOrDefault(s => s.Id_produit == idP);// verifier id_produit 
            if (pr != null) // =>id_produit existe
            {
                pr.Nom_produit = NomP;
                pr.Quantite_produit = Quantite;
                pr.Prix_produit = prix;
                pr.Id_categorie = idCategorie;
                db.SaveChanges();
            }
        }

        //=> Supprimer produit 
        public void Supprimer_Produit(int id)
        {
            pr = new Produit();
            pr = db.Produits.SingleOrDefault(s => s.Id_produit == id);
            if (pr != null)
            {
                db.Produits.Remove(pr);
                db.SaveChanges();
            }
        }

    }
}
