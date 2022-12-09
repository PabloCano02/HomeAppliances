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
    [Route("api/documenttypes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly ILogger<DocumentTypesController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DocumentTypesController(ILogger<DocumentTypesController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //Search
        [HttpGet]
        public async Task<ActionResult<List<DocumentTypeDTO>>> GetDocumentTypes()
        {
            var documentType = await _context.DocumentTypes.OrderBy(dt => dt.Description).ToListAsync();
            return _mapper.Map<List<DocumentTypeDTO>>(documentType);
        }

        //Search by parameter
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DocumentTypeDTO>> GetDocumentType(int id)
        {
            var documentType = await _context.DocumentTypes.FirstOrDefaultAsync(dt => dt.Id == id);

            if (documentType == null)
            {
                return NotFound();
            }

            return _mapper.Map<DocumentTypeDTO>(documentType);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentType>> PostDocumentType([FromBody] DocumentTypeCreationDTO documentTypeCreationDTO)
        {
            var documentType = _mapper.Map<DocumentType>(documentTypeCreationDTO);

            _context.Add(documentType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> PutDocumentType(int id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return BadRequest("No coinciden los campos a editar");
            }

            var exist = await _context.DocumentTypes.AnyAsync(dt => dt.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Update(documentType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            DocumentType documentType = await _context.DocumentTypes.FirstOrDefaultAsync(dt => dt.Id == id);

            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
