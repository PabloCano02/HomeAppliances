using HomeAppliances.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeAppliances.DTOs
{
    public class HomeAppliancePhotoDTO
    {
        public int Id { get; set; }

        public int HomeApplianceId { get; set; }

        [JsonIgnore]
        public HomeAppliance HomeAppliance { get; set; }

        [Display(Name = "Foto")]
        public string ImageId { get; set; }
    }
}
