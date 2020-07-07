using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required]
        public string TipoDocumento { get; set; }
        [Required]
        public string NroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Pais Pais { get; set; }
        [Required]
        public int PaisId { get; set; }
        public string Nacionalidad { get; set; }
        public Sexo Sexo { get; set; }
        [Required]
        public int SexoId { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}