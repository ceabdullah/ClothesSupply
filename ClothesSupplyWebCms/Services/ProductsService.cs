using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClothesSupplyWebCms.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;

        public ProductsService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _apiClient = httpClient;
        }
    }
}
