using SQLite;

namespace Teste.Database
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}