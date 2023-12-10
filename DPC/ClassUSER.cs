using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class ClassUSER
    {
        private dbStockContext db = new dbStockContext();
        private Utilisateur U;

        public bool Ajouter_USER(string User, string motdepass)
        {
            U = new Utilisateur();
            U.NomUtilisateur = User;
            U.mot_de_pass = motdepass;

            //=> Verifier User déja existe
            if (db.Utilisateurs.SingleOrDefault(s => s.NomUtilisateur == User) == null)   //=> n'existe pas 
            {
                db.Utilisateurs.Add(U);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Suprimer_user(string id)
        {
            U = new Utilisateur();
            U = db.Utilisateurs.SingleOrDefault(s => s.NomUtilisateur == id);
            if (U != null)
            {
                db.Utilisateurs.Remove(U);
                db.SaveChanges();
            }
        }
    }
}
