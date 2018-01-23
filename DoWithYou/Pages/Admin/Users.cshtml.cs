using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
using DoWithYou.Interface.Entity;
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
        public UsersModel(Owned<IDatabaseHandler<IUser>> scope)
        {
            if (scope == null)
                throw new ArgumentNullException(nameof(scope));
            if (scope.Value == null)
                throw new ArgumentNullException($"{scope.Value} : {nameof(IDatabaseHandler<IUser>)}<{nameof(IUser)}>");

            using (scope)
            {
                Users = scope.Value?.GetMany(users => users
                    .Where(u => u != null)
                    .OrderBy(u => u.UserID)
                    .ThenBy(u => u.Username));
            }
        }
        #endregion

        public void OnGet() { }
    }
}