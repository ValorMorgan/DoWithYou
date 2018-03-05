using System.Collections.Generic;
using System.Linq;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoWithYou.API.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        #region VARIABLES
        private readonly IModelHandler<IUserModel, IUser, IUserProfile> _handler;
        #endregion

        #region CONSTRUCTORS
        public UsersController(IModelHandler<IUserModel, IUser, IUserProfile> handler)
        {
            _handler = handler;
        }
        #endregion

        [HttpDelete("{id}")]
        public void Delete(int id) { }

        [HttpGet]
        public IEnumerable<IUserModel> Get()
        {
            return _handler.GetMany<IUser>(users => users?.Where(u => u != null));
        }

        [HttpGet("{userId}")]
        public IUserModel Get(long userId)
        {
            return _handler.Get<IUser>(users => users.FirstOrDefault(u => u.UserID == userId));
        }

        [HttpPost]
        public void Post([FromBody] string value) { }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }
    }
}