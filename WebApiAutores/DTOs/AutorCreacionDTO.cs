﻿using System.ComponentModel.DataAnnotations;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe exceder los {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
