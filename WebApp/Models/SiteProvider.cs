using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models;
public class SiteProvider{
    IDbConnection connection = null!;
    string connectionString;
    public SiteProvider(IConfiguration configuration) 
        => connectionString = configuration.GetConnectionString("AES") ?? throw new Exception("Not found AES");
    protected IDbConnection Connection => connection ??= new SqlConnection(connectionString);
    ClassRepository _class = null!;
    public ClassRepository Class => _class ??= new ClassRepository(Connection);
    StudentRepository student = null!;
    public StudentRepository Student => student ??= new StudentRepository(Connection);
}