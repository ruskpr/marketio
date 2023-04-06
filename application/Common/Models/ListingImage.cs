
namespace Common.Models
{
    public class ListingImage : IDbModel
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        //public Listing Listing { get; set; }

        public string ImageAsBase64 { get; set; }

        public int Index { get; set; }

        public DateTime DateAdded { get; set; }
               
    }
}
