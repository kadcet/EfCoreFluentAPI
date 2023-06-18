using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public class Product:BaseEntity
	{
        //[StringLength(128)]
        //[Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Her Product in mutlaka Category si olsun
        public int? CategoryId { get; set; }

		public float TAX { get; set; }

        public decimal IncludeTax => Price * Convert.ToDecimal(TAX);

        // Bir Product Category si gelsin diye koyduk. Navigation Property
        public Category Category { get; set; }

        
    }
}
