using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System.Collections.Generic;
using System.Linq;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly IConfiguration config;

        public ProductData(IConfiguration config)
        {
            this.config = config;
        }

        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess(config);

            return sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");
        }

        public ProductModel GetProductById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(config);

            var p = new { Id = id };

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", p, "RMData").FirstOrDefault();

            return output;
        }
    }
}
