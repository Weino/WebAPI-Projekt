using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.Data;
using WebAPI_Projekt.DTO;
using WebAPI_Projekt.Models;
using static WebAPI_Projekt.DTO.GeoMessageDTO;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebAPI_Projekt.Controllers
{

    [Route("api/v1/geo-comments")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {

        private readonly GeoMessageDbContext _ctx;
        private readonly UserManager<User> _userManager;
        public GeoMessageController(GeoMessageDbContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }


        //GET api/GeoMessage - Will get you all geomessages posted
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeoMessageDTO>>> Get()
        {
            return await _ctx.GeoMessages.Select(messages =>
                new GeoMessageDTO
                {
                    Message = messages.Message,
                    Longitude = messages.Longitude,
                    Latitude = messages.Latitude
                })
                .ToListAsync();
        }


        //Get api/GeoMessage by Id
        [HttpGet("{id}")]
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

        //Post your new GeoMessage
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GeoMessageDTO>> PostGeoMessage([FromBody] GeoMessageDTO geoMessages)
        {
            var user = await 

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
