using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagement.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        //i cant use for loop with  IEnumerable
        public List<RoleViewModel> Roles { get; set; }

    }
}
