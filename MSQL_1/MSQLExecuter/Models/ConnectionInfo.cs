using System.Data.SqlClient;

public class ConnectionInfo
{
    public string Name { get; set; }
    public string Server { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IntegratedSecurity { get; set; }
    
    public SqlConnection CreateConnection()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = Server,
            InitialCatalog = Database,
            IntegratedSecurity = IntegratedSecurity
        };
        
        if (!IntegratedSecurity)
        {
            builder.UserID = Username;
            builder.Password = Password;
        }
        
        return new SqlConnection(builder.ConnectionString);
    }
} 