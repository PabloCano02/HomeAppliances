﻿using System.ComponentModel.DataAnnotations;

namespace HomeAppliances.DTOs
{
    public class BrandCreationDTO
    {
        [Display(Name = "Marca")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}