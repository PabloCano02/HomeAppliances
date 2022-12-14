using HomeAppliances.Entities;
using HomeAppliances.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeAppliances.DTOs
{
    public class HomeApplianceCreationDTO
    {
        public int HomeApplianceTypeId { get; set; }

        public int BrandId { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        [Display(Name = "Tipo de electrodoméstico")]
        public HomeApplianceType HomeApplianceType { get; set; }

        [JsonIgnore]
        [Display(Name = "Marca")]
        public Brand Brand { get; set; }

        [JsonIgnore]
        [Display(Name = "Usuario")]
        public IdentityModel IdentityModel { get; set; }

        [JsonIgnore]
        public ICollection<HomeAppliancePhoto> HomeAppliancePhotos { get; set; }

        [Display(Name = "Modelo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Model { get; set; }

        [Display(Name = "Referencia")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Reference { get; set; }

        [Display(Name = "Color")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Color { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Price { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
