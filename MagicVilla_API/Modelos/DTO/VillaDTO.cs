using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.DTO
{
    public class VillaDTO
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(5)]
        public string Nombre { get; set; }
    }
}
