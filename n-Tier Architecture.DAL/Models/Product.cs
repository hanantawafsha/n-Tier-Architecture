﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DAL.Models
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public string MainImage { get; set; }
        //image relation
        public List<ProductImage> SubImages { get; set; } = new List<ProductImage>();
        public List<Review> Reviews { get; set; } = new List<Review> { };
    }
}
