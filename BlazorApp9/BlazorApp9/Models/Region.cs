using System.ComponentModel.DataAnnotations;

namespace BlazorApp9.Models
{
    public partial class Region
    {
        public Region()
        {
            this.Territories = new HashSet<Territory>();
        }
        [Key]
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
    
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
