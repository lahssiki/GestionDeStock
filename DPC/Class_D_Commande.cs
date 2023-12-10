using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeStock.DPC
{
     public class Class_D_Commande
     {
        // sauvgarder les produit Commander dans listes
        public static List<Class_D_Commande> listDetial = new List<Class_D_Commande>();

        public int Id { get; set; }
        public string Nom { get; set; }
        public int Quantite { get; set; }
        public string Prix { get; set; }
        public string Total { get; set; }
    }
}
