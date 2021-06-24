using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly IProductData productData;
        private readonly ISqlDataAccess sql;

        public SaleData(IProductData productData, ISqlDataAccess sql)
        {
            
            this.productData = productData;
            this.sql = sql;
        }

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            decimal taxRate = ConfigHelper.GetTaxRate() / 100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = productData.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the DB.");
                }

                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                detail.Tax = (productInfo.IsTaxable) ? (detail.PurchasePrice * taxRate) : (0);

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;
            
            try
            {
                sql.StartTransaction("RMData");

                sql.SaveDataInTransaction("dbo.spSale_Insert", sale);

                sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup",
                    new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

                foreach (var item in details)
                {
                    item.SaleId = sale.Id;
                    sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                }

                sql.CommitTransaction();
            }
            catch
            {
                sql.RollbackTransaction();
                throw;
            }
            
        }

        public List<SaleReportModel> GetSaleReport()
        { 
            return sql.LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new { }, "RMData");
        }
    }
}
