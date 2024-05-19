using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMODotNetCore.PizzaApi.Model
{
    [Table("Tbl_Pizza")]
    public class PizzaModel
    {
        [Key]
        public int PizzaId { get; set; }
        public string Pizza { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public string PriceStr { get { return "$" + Price; } }
    }
}
