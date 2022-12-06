using AutoMapper;
using HomeAppliances.Data;
using HomeAppliances.DTOs;
using HomeAppliances.Entities;
using HomeAppliances.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeAppliances.Controllers
{
    [ApiController]
    [Route("api/homeapliancephotos")]
    public class HomeAppliancePhotosController : ControllerBase
    {
        private readonly ILogger<HomeAppliancePhotosController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string _container = "Files";

        public HomeAppliancePhotosController(ILogger<HomeAppliancePhotosController> logger, ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<HomeAppliancePhotoDTO>>> GetHomeAppliancePhotos()
        {
            var homeAppliancePhoto = await _context.HomeAppliancePhotos.OrderBy(hap => hap.Id).ToListAsync();
            return _mapper.Map<List<HomeAppliancePhotoDTO>>(homeAppliancePhoto);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<HomeAppliancePhotoDTO>> GetHomeAppliancePhoto(int id)
        {
            var homeAppliancePhoto = await _context.HomeAppliancePhotos.FirstOrDefaultAsync(hap => hap.Id == id);

            if (homeAppliancePhoto == null)
            {
                return NotFound();
            }

            return _mapper.Map<HomeAppliancePhotoDTO>(homeAppliancePhoto);
        }

        [HttpPost]
        public async Task<ActionResult<HomeAppliancePhoto>> PostHomeAppliancePhoto([FromForm] HomeAppliancePhotoCreationDTO homeAppliancePhotoCreationDTO)
        {
            var file = _mapper.Map<HomeAppliancePhoto>(homeAppliancePhotoCreationDTO);

            if (homeAppliancePhotoCreationDTO.ImageId != null)
            {
                file.ImageId = await _fileStorage.SaveFile(_container, homeAppliancePhotoCreationDTO.ImageId);
            }

            _context.Add(file);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHomeAppliancePhoto(int id)
        {
            HomeAppliancePhoto homeAppliancePhoto = await _context.HomeAppliancePhotos.FirstOrDefaultAsync(hap => hap.Id == id);

            if (homeAppliancePhoto == null)
            {
                return NotFound();
            }

            _context.Remove(homeAppliancePhoto);
            await _context.SaveChangesAsync();
            return NoContent(); //204
        }
    }
}
