using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Projekt.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebAPI_Projekt.Controllers
{

    [Route("api/GeoComments")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {
        [HttpGet("Id")]
        public async Task<ActionResult<GeoMessage>> GetGeoComment(int id)
        {
            //test var message = new GeoMessage();
                return Ok();
        }
    }
}
