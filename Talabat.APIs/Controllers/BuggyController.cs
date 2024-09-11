using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    
    public class BuggyController : APIBaseController
    {
        private readonly StoreDbContext _DbContext;

        public BuggyController(StoreDbContext storeDbContext)
        {
            _DbContext = storeDbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult notFound()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("ServerError")]
        public ActionResult serverError()
        {
            var product = _DbContext.Products.Find(100);
            var ret = product.ToString();
            return Ok(ret);
        }

        [HttpGet("BadRequest")]
        public ActionResult getbadRequest() {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult getbadRequest(int id)
        {
            return Ok();
        }

    }
}
