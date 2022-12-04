using AutoMapper;
using HomeAppliances.Data;
using HomeAppliances.DTOs;
using HomeAppliances.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeAppliances.Controllers
{
    [ApiController]
    [Route("api/homeappliances")]
    public class HomeAppliancesController : ControllerBase
    {
        private readonly ILogger<HomeAppliancesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HomeAppliancesController(ILogger<HomeAppliancesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<HomeApplianceDTO>>> GetHomeAppliances()
        {
            var homeAppliance = await _context.HomeAppliances.OrderBy(ha => ha.Id).ToListAsync();
            return _mapper.Map<List<HomeApplianceDTO>>(homeAppliance);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HomeApplianceDTO>> GetHomeAppliance(int id)
        {
            var homeAppliance = await _context.HomeAppliances.FirstOrDefaultAsync(ha => ha.Id == id);

            if (homeAppliance == null)
            {
                return NotFound();
            }

            return _mapper.Map<HomeApplianceDTO>(homeAppliance);
        }

        [HttpPost]
        public async Task<ActionResult> PostHomeAppliance([FromBody] HomeApplianceCreationDTO homeApplianceCreationDTO)
        {
            var homeAppliance = _mapper.Map<HomeAppliance>(homeApplianceCreationDTO);

            _context.Add(homeAppliance);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeAppliance>> PutHomeAppliance(int id, HomeAppliance homeAppliance)
        {
            if (id != homeAppliance.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.HomeAppliances.AnyAsync(ha => ha.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(homeAppliance);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeAppliance(int id)
        {
            HomeAppliance homeAppliance = await _context.HomeAppliances.FirstOrDefaultAsync(ha => ha.Id == id);

            if (homeAppliance == null)
            {
                return NotFound();
            }

            _context.HomeAppliances.Remove(homeAppliance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
