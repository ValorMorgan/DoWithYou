using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoWithYou.Pages.Admin
{
    public class UsersModel : PageModel
    {
        #region PROPERTIES
        public IEnumerable<IUser> Users { get; set; }
        #endregion

        #region CONSTRUCTORS
        public UsersModel(IDatabaseHandler<IUser> repository)
        {
            Users = repository?.Get(u => u?.Where(e => e != null));
        }
        #endregion

        public void OnGet() { }
    }
}