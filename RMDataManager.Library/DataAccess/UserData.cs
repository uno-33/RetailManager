using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Internal.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess sql;

        public UserData(ISqlDataAccess sql)
        {
            this.sql = sql;
        }

        public List<UserModel> GetUserById(string id)
        {
            return sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id = id }, "RMData");
        }
    }
}
