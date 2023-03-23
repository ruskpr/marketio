using System.ComponentModel.DataAnnotations;

namespace MarketIO.Shared.Models
{
    public class Transaction
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