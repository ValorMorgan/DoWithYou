using System.Linq;
using DoWithYou.API.Controllers.Base;
using DoWithYou.Interface.Entity;
using DoWithYou.Interface.Model;
using DoWithYou.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoWithYou.API.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : BaseController<IUserModel, IUser>
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

        [HttpDelete]
        public IActionResult Delete([FromBody] IUserModel value) =>
            ExecuteAction(_handler.Delete, value);

        // TODO: Id should be retrieved from session / token, not a parameter
        [HttpGet("{id}")]
        public IActionResult Get(long id) =>
            ExecuteFunction(_handler.Get, users => users?.FirstOrDefault(u => u.UserID == id));

        // TODO: Check we don't collide with existing data (unless we want duplicates?)
        [HttpPut]
        public IActionResult Insert([FromBody] IUserModel value) =>
            ExecuteAction(_handler.Insert, value);

        [HttpPost]
        public IActionResult Update([FromBody] IUserModel value) =>
            ExecuteAction(_handler.Update, value);
    }
}