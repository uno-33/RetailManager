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
    public class InventoryData : IInventoryData
    {
        private readonly IConfiguration config;
        private readonly ISqlDataAccess sql;

        public InventoryData(IConfiguration config, ISqlDataAccess sql)
        {
            this.config = config;
            this.sql = sql;
        }

        public List<InventoryModel> GetInventory()
        {
            return sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "RMData");
        }

        public void SaveInventoryRecord(InventoryModel item)
        {
            sql.SaveData("dbo.spInventory_Insert", item, "RMData");
        }
    }
}
