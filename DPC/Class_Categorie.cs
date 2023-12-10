using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
    class Class_Categorie
    {
        private dbStockContext db = new dbStockContext();
        private categorie cat;
        //=> fonction ajouter Categorie
        public bool Ajouter_Categorie(string NomCat)
        {
            cat = new categorie();
            cat.Nom_categorie = NomCat;
            if(db.categories.SingleOrDefault(s=>s.Nom_categorie == NomCat)== null)
            {
                db.categories.Add(cat);
                db.SaveChanges();
                return true;
            }
            else // le nom de Categorie deja existe
            {
                return false;
            }
        }
        // => Supprimer Categorie
        public void Suprimer_Categorie(int id)
        {
            cat = new categorie();
            cat = db.categories.SingleOrDefault(s => s.Id_categorie == id);
            if (cat != null)
            {
                db.categories.Remove(cat);
                db.SaveChanges();
            }
        }  
    }
}
    