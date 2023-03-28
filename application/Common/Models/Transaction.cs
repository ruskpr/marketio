using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Transaction : IDbModel
    {
        [Key]
        public int Id { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public DateTime DateCreated { get; set; }

    }
}