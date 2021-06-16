using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            return sql.LoadData<ProductModel, dynamic>("spProduct_GetAll", new { }, "RMData");
        }
    }
}
