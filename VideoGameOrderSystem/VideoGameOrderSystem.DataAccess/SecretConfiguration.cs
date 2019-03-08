using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.DataAccess
{
    public class SecretConfiguration
    {
        public static string ConnectionString = "Server=tcp:troha1902sql.database.windows.net,1433;" +
            "Initial Catalog=OrderSystem;Persist Security Info=False;" +
            "User ID=ctroha;Password=Samurai7!;MultipleActiveResultSets=False;" +
            "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
