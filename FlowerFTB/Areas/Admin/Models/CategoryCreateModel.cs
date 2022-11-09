using System.ComponentModel.DataAnnotations;

namespace FlowerFTB.Areas.Admin.Models
{
    public class CategoryCreateModel
    {
        [Required, MaxLength(20)]
        public string? Name { get; set; }
        [Required, MaxLength(150)]
        public string? Description { get; set; }
    }
}
