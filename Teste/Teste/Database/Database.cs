using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using Teste.Models;
using Xamarin.Forms;

namespace Teste.Database
{
    public sealed class Database
    {
        private readonly SQLiteConnection _conexao;
        private static object _locker = new object();

        private static readonly Lazy<Database> Lazy = new Lazy<Database>(() => new Database());
        public static Database Current => Lazy.Value;

        public Database()
        {
            _conexao = DependencyService.Get<ISQLite>().GetConnection();
            _conexao.CreateTable<Cotacao>();
        }

        public bool Save(object obj)
        {
            try
            {
                lock (_locker)
                {
                    if (_conexao.Update(obj) == 0)
                        return _conexao.Insert(obj) != 0;

                    return true;
                }
            }
            catch (SQLiteException) { return false; }
        }

        public bool SaveAll(IEnumerable listaObj)
        {
            try
            {
                lock (_locker)
                {
                    _conexao.RunInTransaction(() =>
                    {
                        foreach (var dto in listaObj)
                        {
                            if (_conexao.Update(dto) == 0)
                                _conexao.Insert(dto);
                        }
                    });

                    _conexao.Commit();

                    return true;
                }
            }
            catch (SQLiteException) { return false; }
            catch (Exception ex) { return false; }
        }

        public int Delete(object obj)
        {
            try
            {
                lock (_locker)
                    return _conexao.Delete(obj);
            }
            catch (SQLiteException) { return 0; }
        }

        public int DeleteAll(IEnumerable listaObj)
        {
            try
            {
                foreach (var item in listaObj)
                    _conexao.Delete(item);

                return 1;
            }
            catch (SQLiteException) { return 0; }
        }

        public IEnumerable<Cotacao> GetCotacoes()
        {
            lock (_locker)
                return _conexao.Table<Cotacao>().ToList();
        }

        public Cotacao GetCotacao(decimal codigoCotacao)
        {
            lock (_locker)
                return _conexao.Table<Cotacao>().FirstOrDefault(a => a.CodigoCotacao == codigoCotacao);
        }
    }
}
