using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace House.Models {
    public class Products {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ShadeColor { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }

        public int ProductTypesId { get; set; }        
        [ForeignKey("ProductTypesId")]
        [Display(Name="Product Id Test")]
        public virtual ProductTypes ProductTypes { get; set; }

        public int SpecialTagsId { get; set; }
        [ForeignKey("SpecialTagsId")] 
        [Display(Name = "special tag")] 
        public virtual SpecialTags SpecialTags { get; set; }
    }
}