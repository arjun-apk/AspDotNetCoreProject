using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public string UserId
        {
            get => "1234567890";
        }

        [NonAction]
        public T2 MapData<T1, T2>(T1 data)
        {
            return ExpressMapper.Mapper.Map<T1, T2>(data);
        }

        [NonAction]
        public IActionResult OkHttpResult<T1, T2> (T1 data)
        {
            var viewData = MapData<T1, T2>(data);
            return StatusCode((int)HttpStatusCode.OK, viewData);
        }

        [NonAction]
        public IActionResult CreatedHttpResult(string message = "Created successfully")
        {
            return StatusCode((int)HttpStatusCode.Created, message);
        }

        [NonAction]
        public IActionResult UpdatedHttpResult(string message = "Updated successfully")
        {
            return StatusCode((int)HttpStatusCode.OK, message);
        }

        [NonAction]
        public IActionResult DeletedHttpResult(string message = "Deleted successfully")
        {
            return StatusCode((int)HttpStatusCode.OK, message);
        }

        [NonAction]
        public IActionResult BadRequestHttpResult(string message = "Bad Request")
        {
            return StatusCode((int)HttpStatusCode.BadRequest, message);
        }

        [NonAction]
        public IActionResult InternalServerErrorHttpResult(string message = "Internal Server Error")
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, message);
        }
    }
}
