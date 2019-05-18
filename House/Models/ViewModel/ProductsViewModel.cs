using System.Collections;
using System.Collections.Generic;

namespace House.Models.ViewModel {
    public class ProductsViewModel {
        public Products Products { get; set; }
        public IEnumerable<ProductTypes> ProductTypes { get; set; }
        public IEnumerable<SpecialTags> SpecialTags { get; set; }
    }
}