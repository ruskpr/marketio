using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Message : IDbModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public DateTime DateSent { get; set; }

        public bool ReadByReceiver { get; set; }
    }
}
