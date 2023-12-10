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
    public partial class UserDeconnecter : UserControl
    {

        private static UserDeconnecter UserD;
        public static UserDeconnecter Instance
        {
            get
            {
                if (UserD == null)
                {
                    UserD = new UserDeconnecter();
                }
                return UserD;
            }
        }
    
        public UserDeconnecter()
        {
            InitializeComponent();
        }
    }
}
