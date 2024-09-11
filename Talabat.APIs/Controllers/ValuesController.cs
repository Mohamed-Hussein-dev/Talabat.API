using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("erorr/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ValuesController : ControllerBase
    {
        public ActionResult notFindEndPoint(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
