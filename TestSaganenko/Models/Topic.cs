using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSaganenko.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTopic { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public User _user { get; set; }
        public Category _category { get; set; }
        public ICollection<Post> Posts { get; set; }

    }
}
