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
    [Route("api/homeappliancetypes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class HomeApplianceTypesController : ControllerBase
    {
        private readonly ILogger<HomeApplianceTypesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeApplianceTypesController(ILogger<HomeApplianceTypesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<HomeApplianceTypeDTO>>> GetHomeApplianceTypes()
        {
            var homeApplianceType = await _context.HomeApplianceTypes.OrderBy(hat => hat.Name).ToListAsync();
            return _mapper.Map<List<HomeApplianceTypeDTO>>(homeApplianceType);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HomeApplianceTypeDTO>> GetHomeApplianceType(int id)
        {
            var homeApplianceType = await _context.HomeApplianceTypes.FirstOrDefaultAsync(hat => hat.Id == id);

            if (homeApplianceType == null)
            {
                return NotFound();
            }

            return _mapper.Map<HomeApplianceTypeDTO>(homeApplianceType);
        }

        [HttpPost]
        public async Task<ActionResult<HomeApplianceType>> PostHomeApplianceType([FromBody] HomeApplianceTypeCreationDTO homeApplianceTypeCreationDTO)
        {
            var homeApplianceType = _mapper.Map<HomeApplianceType>(homeApplianceTypeCreationDTO);

            _context.Add(homeApplianceType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeApplianceType>> PutHomeApplianceType(int id, HomeApplianceType homeApplianceType)
        {
            if (id != homeApplianceType.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.HomeApplianceTypes.AnyAsync(hat => hat.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(homeApplianceType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeApplianceType(int id)
        {
            HomeApplianceType homeApplianceType = await _context.HomeApplianceTypes.FirstOrDefaultAsync(hat => hat.Id == id);

            if (homeApplianceType == null)
            {
                return NotFound();
            }

            _context.HomeApplianceTypes.Remove(homeApplianceType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
