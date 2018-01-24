using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoWithYou.Pages.Admin
{
    public class UsersModel : PageModel
    {
        #region PROPERTIES
        public IEnumerable<IUserModel> Users { get; set; }
        #endregion

        #region CONSTRUCTORS
        public UsersModel(Owned<IModelHandler<IUserModel, IUser, IUserProfile>> scope)
        {
            if (scope == null)
                throw new ArgumentNullException(nameof(scope));
            if (scope.Value == null)
                throw new ArgumentNullException($"{nameof(scope)}.{nameof(scope.Value)}");

            using (scope)
            {
                Users = scope.Value.GetMany(
                    users => users?.Where(u => u != null),
                    profiles => profiles?.Where(p => p != null));
            }
        }
        #endregion

        public void OnGet() { }
    }
}