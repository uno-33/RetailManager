using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System.Collections.Generic;
using System.Linq;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess sql;

        public ProductData(ISqlDataAccess sql)
        {
            this.sql = sql;
        }

        public List<ProductModel> GetProducts()
        {
            return sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");
        }

        public ProductModel GetProductById(int id)
        {
            var p = new { Id = id };

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", p, "RMData").FirstOrDefault();

            return output;
        }
    }
}
