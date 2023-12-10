using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class Class_Connexion
    {
        public bool ConnexionValide(dbStockContext db,string User,string mot_de_passe)
        {
            Utilisateur u = new Utilisateur();
            u.NomUtilisateur = User;
            u.mot_de_pass = mot_de_passe;
            if(db.Utilisateurs.SingleOrDefault(s=>s.NomUtilisateur==User && s.mot_de_pass == mot_de_passe)!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
