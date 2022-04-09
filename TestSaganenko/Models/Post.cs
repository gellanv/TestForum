using System;
using System.ComponentModel.DataAnnotations;

namespace TestSaganenko.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string TextPost { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePost { get; set; }
        public string? UserId { get; set; }
        public int TopicId { get; set; }

        public User _user { get; set; }
        public Topic _topic { get; set; }

    }
}
