using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp9.Models
{
    public partial class Product
    {
        public Product()
        {
            this.Order_Details = new HashSet<OrderDetail>();
        }
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> Order_Details { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
