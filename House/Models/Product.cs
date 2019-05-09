using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace House.Models {
    public class Product {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  Image { get; set; }
        public string  ShadeColor{ get; set; }
        public double Price { get; set; }
        public bool  Available { get; set; }

        [Display(Name = "Product type")]
        public int ProductTypesId { get; set; }

        [ForeignKey("ProductTypesId")] public virtual ProductTypes ProductTypes { get; set; }
        
        [Display(Name = "special tag")] public int SpecialTagsId { get; set; }
        [ForeignKey("SpecialTagsId")]public virtual SpecialTags SpecialTags { get; set; }


    }
}