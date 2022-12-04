using HomeAppliances.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeAppliances.DTOs
{
    public class HomeApplianceTypeCreationDTO
    {
        [Display(Name = "Tipo de electrodoméstico")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<HomeAppliance> HomeAppliances { get; set; }
    }
}
