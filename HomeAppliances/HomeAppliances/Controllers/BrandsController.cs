using AutoMapper;
using HomeAppliances.Data;
using HomeAppliances.DTOs;
using HomeAppliances.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeAppliances.Controllers
{
    [ApiController]
    [Route("api/brands")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class BrandsController : ControllerBase
    {
        private readonly ILogger<BrandsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandsController(ILogger<BrandsController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;

        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<BrandDTO>>> GetBrands()
        {
            var brand = await _context.Brands.OrderBy(b => b.Name).ToListAsync();
            return _mapper.Map<List<BrandDTO>>(brand);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandDTO>> GetBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
            {
                return NotFound();
            }

            return _mapper.Map<BrandDTO>(brand);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand([FromBody] BrandCreationDTO brandCreationDTO)
        {
            var brand = _mapper.Map<Brand>(brandCreationDTO);

            _context.Add(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.Brands.AnyAsync(b => b.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            Brand brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
