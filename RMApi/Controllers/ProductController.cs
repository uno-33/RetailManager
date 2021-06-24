﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Internal.Models;
using System.Collections.Generic;


namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Cashier")]
    public class ProductController : ControllerBase
    {
        private readonly IProductData productData;

        public ProductController(IConfiguration config, IProductData productData)
        {
            this.productData = productData;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            return productData.GetProducts();
        }
    }
}
