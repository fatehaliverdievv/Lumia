using Lumia.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Lumia.Models
{
    public class Employee:BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string? Surname { get; set; }
        public string ImgUrl { get; set; }
        public Position Position { get; set; }
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
