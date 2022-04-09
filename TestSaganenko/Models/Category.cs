using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSaganenko.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public ICollection<Topic> Topics { get; set; }
    }
}
