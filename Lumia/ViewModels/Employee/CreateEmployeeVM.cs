using Lumia.Models;
using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels
{
    public class CreateEmployeeVM
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string? Surname { get; set; }
        public IFormFile Image{ get; set; }
        [Required]
        public int PositionId { get; set; }
        [StringLength(150)]
        public string? InstagramUrl { get; set; }
        [StringLength(150)]
        public string? FacebookUrl { get; set; }
        [StringLength(150)]
        public string? TwitterLink { get; set; }
        [StringLength(150)]
        public string? LinkedinLink { get; set; }
    }
}
