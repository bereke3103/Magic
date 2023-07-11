using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaApi.Models.Dto
{
    public class VillaNumberDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VilldID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
