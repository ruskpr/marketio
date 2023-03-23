using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketIO.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100), MinLength(3)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        public DateTime? DOB { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastLogin { get; set; }

        public DateTime? LastLogout { get; set;}

        //public List<Transaction> Transactions { get; set; } = new List<Transaction>();  

        #region not mapped

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        #endregion
    }
}
