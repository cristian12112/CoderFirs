using CoderFirs.DTOs;
using CoderFirs.Models;
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

        [Route("~/api/agregar")]
        [HttpPost]

        public async Task<ActionResult<BeersDtos>> Add(BeersInsertDtos insertDtos)
        {
            var beerEntity = new BeerModels()
            {
                Name    = insertDtos.Name,
                BrandId = insertDtos.BrandID,
                Alcohol = insertDtos.Alcohol
            };
            await _storeContext.Beers.AddAsync(beerEntity);

            await _storeContext.SaveChangesAsync();

            var BeerDto = new BeersDtos
            {
                Id= beerEntity.BeerId,
                Name = beerEntity.Name,
                Alcohol = beerEntity.Alcohol,
                BrandID = beerEntity.BrandId
            };

            return CreatedAtAction(nameof(GetBYId), new { id = beerEntity.BeerId }, BeerDto);
        }
    }
}
