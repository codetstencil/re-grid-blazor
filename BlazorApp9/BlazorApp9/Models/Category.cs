using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp9.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public byte[] Picture { get; set; }
    
        public virtual ICollection<Product> Products { get; set; }
    }
}
