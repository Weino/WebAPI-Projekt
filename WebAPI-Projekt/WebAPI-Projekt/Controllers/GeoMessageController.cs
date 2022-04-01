using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Projekt.Data;
using WebAPI_Projekt.DTO;
using WebAPI_Projekt.Models;
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
        public async Task<ActionResult<GeoMessageDTO>> PostGeoMessage([FromBody] GeoMessage geoMessages)
        {
            var user = await _userManager.GetUserAsync(this.User);

            var geoMessage = geoMessages.ToModel(user);
            _ctx.GeoMessages.Add(geoMessage);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction("GetGeoComment" , new { id = geoMessage.Id } , geoMessages);
        }
    }
}
