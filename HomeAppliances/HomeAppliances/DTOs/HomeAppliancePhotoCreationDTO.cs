using HomeAppliances.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeAppliances.DTOs
{
    public class HomeAppliancePhotoCreationDTO
    {
        public int HomeApplianceId { get; set; }

        [Display(Name = "Foto")]
        public IFormFile ImageId { get; set; }
    }
}
