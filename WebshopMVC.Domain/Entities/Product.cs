using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebshopMVC.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter a product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Provide a description")]
        public string Description { get; set; }

		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Enter a short summary")]
		public string Summary { get; set; }

        [Required(ErrorMessage = "Determine the price per unit")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Only positive, non-zero prices are allowed")]
        public decimal Price { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string Image { get; set; }

		//[HiddenInput]
		//public byte[] ImageData { get; set; }
		//[HiddenInput]
		//public string ImageMimeType { get; set; }
    }
}
