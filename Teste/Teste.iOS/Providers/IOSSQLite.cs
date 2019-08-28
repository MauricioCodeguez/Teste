using SQLite;
using System;
using System.IO;
using Teste.Database;
using Teste.iOS.Providers;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSSQLite))]

namespace Teste.iOS.Providers
{
    public class IOSSQLite : ISQLite
    {
        private string _fileName = "sqliteDB.db3";

        public IOSSQLite() { }

        public SQLiteConnection GetConnection() => new SQLiteConnection(GetPath());

        private string GetPath()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(folderPath, _fileName);
            return filePath;
        }
    }
}