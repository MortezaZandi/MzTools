using System.Data.SqlClient;

public class SqlServerInfo
{
    public string ServerName { get; set; }
    public string Instance { get; set; }
    public string CatalogName { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }

    public string GetConnectionString()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = string.IsNullOrEmpty(Instance) ? ServerName : $"{ServerName}\\{Instance}",
            InitialCatalog = CatalogName,
            UserID = UserId,
            Password = Password
        };

        return builder.ConnectionString;
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Instance) ? ServerName : $"{ServerName}\\{Instance}";
    }
}