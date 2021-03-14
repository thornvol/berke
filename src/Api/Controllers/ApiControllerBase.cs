using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BerkeGaming.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        /// <summary>
        /// Common way to handle success/failures of async tasks.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        protected async Task<IActionResult> ExecuteTaskHandler<T>(Task<T> task)
        {
            try
            {
                var results = await task;
                return Ok(results);
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.ToString());
            }
        }
    }
}
