using BusinessLogic.Library.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.ViewModels
{
    public class LoginResponseViewModel
    {
        public string UserName { get; set; }

        public Roles Role { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
