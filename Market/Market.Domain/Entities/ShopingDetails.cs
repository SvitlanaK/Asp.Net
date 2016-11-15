using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entities
{
  public  class ShopingDetails
    {
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please enter the first address line")]
        [Display(Name = "Line 1")]
        public string line1 { get; set; }
        [Display(Name = "Line 2")]

        public string line2 { get; set; }
        [Display(Name = "Line 3")]

        public string line3 { get; set; }
        [Required(ErrorMessage ="Please enter a city name")]
        public string City { get; set; }
        [Required(ErrorMessage ="Please enter a state name")]
        public string State { get; set; }
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
