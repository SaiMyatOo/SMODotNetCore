﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMODotNetCore.PizzaApi.Model
{
    [Table("Tbl_PizzaExtra")]
    public class PizzaExtraModel
    {
        [Key]
        public int PizzaExtraId {  get; set; }  
        public string PizzaExtraName { get; set; }
        public decimal Price { get; set; }  
    }
}