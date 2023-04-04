using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class ListingCategory : IDbModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}