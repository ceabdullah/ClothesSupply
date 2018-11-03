using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ClothesSupplyWebCms.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double? Price { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
