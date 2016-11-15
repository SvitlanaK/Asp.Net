using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Market.Domain.Entities
{
     public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
       // [Required(ErrorMessage ="Введите имя продукта")]
        public string Name { get; set; }
       // [Required(ErrorMessage = "Введите описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage ="Цена не может быть отрицательной")]
        public decimal Price { get; set; }
      //  [Required(ErrorMessage = "Введите название категории")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageType { get; set; }
    }
}
