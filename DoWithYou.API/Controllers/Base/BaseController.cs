using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DoWithYou.API.Controllers.Base
{
    public class BaseController<TModel, TEntity> : Controller
    {
        protected IActionResult ExecuteAction(Action<TModel> action, TModel value)
        {
            if (value == null)
                return BadRequest();

            action(value);

            return Ok();
        }

        protected IActionResult ExecuteFunction(Func<TModel> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func), "Function cannot be null.");

            return Ok(func());
        }

        protected IActionResult ExecuteFunction(Func<Func<IQueryable<TEntity>, TEntity>, TModel> func, Func<IQueryable<TEntity>, TEntity> value)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func), "Function cannot be null.");

            if (value == null)
                return BadRequest();

            return Ok(func(value));
        }

        protected IActionResult ExecuteFunction(Func<Func<IQueryable<TEntity>, IEnumerable<TEntity>>, IEnumerable<TModel>> func, Func<IQueryable<TEntity>, IEnumerable<TEntity>> value)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func), "Function cannot be null.");

            if (value == null)
                return BadRequest();

            return Ok(func(value));
        }
    }
}