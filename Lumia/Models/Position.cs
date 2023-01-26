using System.ComponentModel.DataAnnotations;
using Lumia.Models.Base;

namespace Lumia.Models
{
    public class Position : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
