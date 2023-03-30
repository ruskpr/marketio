
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class ListingImage
    {
        public int Id { get; set; }

        public int ListingId { get; set; }
        public Listing Listing { get; set; }

        public byte[] ImageAsBytes { get; set; }

        public bool IsPrimaryImage { get; set; }

        public DateTime DateAdded { get; set; }


        [NotMapped]
        public string ImageAsBase64
        {
            get
            {
                return ImageAsBytes != null ? $"data:image/png;base64,{Convert.ToBase64String(ImageAsBytes)}" : null;
            }
        }
    }
}
