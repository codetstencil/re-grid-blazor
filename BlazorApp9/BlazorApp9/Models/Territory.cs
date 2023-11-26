using System.ComponentModel.DataAnnotations;

namespace BlazorApp9.Models
{
    public partial class Territory
    {
        public Territory()
        {
            this.Employees = new HashSet<EmployeeTerritories>();
        }
        [Key]
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
    
        public virtual Region Region { get; set; }
        public virtual ICollection<EmployeeTerritories> Employees { get; set; }
    }
}
