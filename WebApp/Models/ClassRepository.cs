using System.Data;
using Dapper;

namespace WebApp.Models;
public class ClassRepository : BaseRepository{
    public ClassRepository(IDbConnection connection) : base(connection)
    {
    }

    public IEnumerable<Class> GetClasses(){
        return connection.Query<Class>("SELECT * FROM Class");
    }
    public Class? GetClass(string id){
        return connection.QuerySingleOrDefault<Class>("SELECT * FROM Class WHERE ClassId = @Id", new { Id = id });
    }
}