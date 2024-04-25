using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMODotNetCore.RestApi.Models
{
    [Table("Tbl_Blog")]
    public class BlogDto
    {
        [Key]
        public int BlogID { get; set; } 
        public string? BlogTitle { get; set;}
        public string? BlogAuthor { get; set;}
        public string? BlogContent { get; set;}

    }
}
