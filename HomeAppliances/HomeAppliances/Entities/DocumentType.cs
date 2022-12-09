using HomeAppliances.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeAppliances.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de documento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<IdentityModel> IdentityModels { get; set; }
    }
}
