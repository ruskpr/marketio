
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class ListingImage
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public Listing Listing { get; set; }

        public string ImageAsBase64 { get; set; }

        public bool IsPrimaryImage { get; set; }

        public DateTime DateAdded { get; set; }
               
    }
}
