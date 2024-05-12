using System.Data.SqlClient;

namespace SMODotNetCore.RestApi.ConnectionManager
{
    public static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "11-02NOTE",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true
    };
    }
}
