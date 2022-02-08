using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.Data;
using WebAPI_Projekt.DTO;
using WebAPI_Projekt.Models;
using static WebAPI_Projekt.DTO.GeoMessageDTO;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebAPI_Projekt.Controllers
{

    [Route("api/GeoComments")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {

        private readonly GeoMessageDbContext _ctx;
        public GeoMessageController(GeoMessageDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("Id")]
        public async Task<ActionResult<GeoMessage>> GetGeoComment(int id)
        {
            var geoMessage = await _ctx.GeoMessages.FirstOrDefaultAsync(c => c.Id == id);
            if (geoMessage == null)
                return StatusCode(StatusCodes.Status404NotFound, new { message = $"That Location Not Found" });

            var geoMessageDTO = new GeoMessageDTO
            {
                Message = geoMessage.Message,
                Longitude = geoMessage.Longitude,
                Latitude = geoMessage.Latitude,
            };

            return Ok(geoMessageDTO);
        }

        [HttpPost]
        public async Task<ActionResult<GeoMessageDTO>> Post([FromBody] GeoMessageDTO geoMessages)
        {
            var geoMessage = await _ctx.AddAsync(new GeoMessage()
            {
                Message = geoMessages.Message,
                Longitude = geoMessages.Longitude,
                Latitude = geoMessages.Latitude
            });
            await _ctx.SaveChangesAsync();
            return CreatedAtAction("GetGeoComment" , new { id = geoMessage.Entity.Id } , geoMessages);
        }
    }
}
