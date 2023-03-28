using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class UserRating : IDbModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Range(1, 5)] // 1 to 5 stars
        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
