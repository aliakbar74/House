using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace House.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Test")]
        public string Name { get; set; }

    }
}
