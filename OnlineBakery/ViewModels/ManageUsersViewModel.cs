using System.Collections.Generic;
using OnlineBakery.Models;

namespace OnlineBakery.ViewModels
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; }

        public ApplicationUser[] Everyone { get; set;}
    }
}