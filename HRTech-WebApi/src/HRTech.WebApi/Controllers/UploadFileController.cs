using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var result = GetFileInfo(Request.Form.Files[0]);
            return Ok(result);
        }
    }
}