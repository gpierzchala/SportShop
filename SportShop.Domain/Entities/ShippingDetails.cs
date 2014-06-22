using System.ComponentModel.DataAnnotations;

namespace SportShop.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Line addres 1 cannot be empty")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "City cannot be empty")]
        public string City { get; set; }
        [Required(ErrorMessage = "State cannot be empty")]
        public string State { get; set; }
        public string Zip {get; set; }
        [Required(ErrorMessage = "Country cannot be empty")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
