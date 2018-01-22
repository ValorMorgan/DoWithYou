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
        public IList<IUser> Users { get; set; }
        #endregion

        #region CONSTRUCTORS
        public UsersModel(Owned<IModelRequestor<IUser, IUserProfile, IUserModel>> scope)
        {
            if (scope == null)
                throw new ArgumentNullException(nameof(scope));
            if (scope.Value == null)
                throw new ArgumentNullException($"{scope.Value} : {nameof(IModelRequestor<IUserModel, IUser, IUserProfile>)}<{nameof(IUser)}>");

            using (scope)
            {
                Users = scope.Value?.RequestModel(
                    users => users
                        .FirstOrDefault(u => u != null),
                    userProfiles => userProfiles
                        .FirstOrDefault(p => p != null));
            }
        }
        #endregion

        public void OnGet() { }
    }
}