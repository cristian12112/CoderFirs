using CoderFirs.DTOs;
using CoderFirs.SqlContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoderFirs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeersController : ControllerBase
    {
        private StoreContext _storeContext;

        public BeersController(StoreContext context) 
        {
            _storeContext = context;
        }

        [Route("~/api/agregarCampo")]
        [HttpGet]
        public async Task<IEnumerable<BeersDtos>> Get() => await _storeContext.Beers.Select(b => new BeersDtos 
        {
            Id = b.BrandId,
            Name = b.Name,
            Alcohol = b.Alcohol,
            BrandID = b.BrandId
        }).ToListAsync();

        [Route("~/api/buscarPorId")]
        [HttpGet]
        public async Task<ActionResult<BeersDtos>> GetBYId(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if( beer == null)
            {
                return NotFound("no se encontro datos");
            }
            var beerDto = new BeersDtos
            {
                Id = beer.BrandId,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandId
            };

            return Ok(beerDto);
        }
    }
}
