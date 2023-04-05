using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class ListingTag : IDbModel
    {
        [Key]
        public int Id { get; set; }

        public int ListingId { get; set; }

        [Required]
        [Column (TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}