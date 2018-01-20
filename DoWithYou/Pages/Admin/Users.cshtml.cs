using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
using DoWithYou.Interface.Data.Entity;
using DoWithYou.Interface.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoWithYou.Pages.Admin
{
    public class UsersModel : PageModel
    {
        #region PROPERTIES
        public IList<IUser> Users { get; set; }
        #endregion

        #region CONSTRUCTORS
        public UsersModel(Owned<IDatabaseHandler<IUser>> ownedScope)
        {
            if (ownedScope == null)
                throw new ArgumentNullException(nameof(ownedScope));

            using (ownedScope)
                Users = ownedScope.Value?.GetMany(users => users
                    .Where(u => u != null)
                    .OrderBy(u => u.UserID)
                    .ThenBy(u => u.Username));
        }
        #endregion

        public void OnGet() { }
    }
}