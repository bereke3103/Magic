using System.ComponentModel.DataAnnotations;

namespace MagicVillaWeb.Models.Dto
{
    public class VillaNumberUpdateDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VilldID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
