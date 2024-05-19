using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMODotNetCore.PizzaApi.Model
{
    [Table("Tbl_PizzaOrderDetails")]
    public class PizzaOrderDetailsModel
    {
        [Key]
        public int PizzaOrderDeitalId { get; set; }
        public string PizzaOrderInvoiceNo { get; set; } 
        public int PizzaExtraId { get; set; }
    }
}
