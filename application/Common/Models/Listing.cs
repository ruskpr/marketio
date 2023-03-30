using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Listing : IDbModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column (TypeName = "varchar(2000)")]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public ListingCategory Category { get; set; }

        [Required]
        public List<ListingTag> ListingTags { get; set; }

        [Required]
        public List<ListingImage> ListingImages { get; set; }

        [Required]
        [Column (TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public bool IsNegotiable { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }

}
