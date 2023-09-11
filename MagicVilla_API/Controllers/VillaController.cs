using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("id:int",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]       
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            VillaDTO villa = VillaStore.VillaList.FirstOrDefault(x => x.ID == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrearVilla([FromBody]VillaDTO villa)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest();
            }
            if (villa.ID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villa.ID = VillaStore.VillaList.OrderByDescending(x => x.ID).FirstOrDefault().ID + 1;

            VillaStore.VillaList.Add(villa);

            return CreatedAtRoute("GetVilla",new {id = villa.ID},villa);
        }

    }
}
