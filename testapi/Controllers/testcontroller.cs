using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testcontroller : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return "Yes";
        }
    }
}
