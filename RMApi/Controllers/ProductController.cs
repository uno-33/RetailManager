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
    //[Authorize(Roles = "Cashier")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration config;

        public ProductController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData(config);

            return data.GetProducts();
        }
    }
}
