using SQLite;
using System;
using System.IO;
using Teste.Database;
using Teste.Droid.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSQLite))]

namespace Teste.Droid.Providers
{
    public class AndroidSQLite : ISQLite
    {
        private string _fileName = "sqliteDB.db3";

        public AndroidSQLite() { }

        public SQLiteConnection GetConnection() => new SQLiteConnection(GetPath());

        private string GetPath()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folderPath, _fileName);
            return filePath;
        }
    }
}