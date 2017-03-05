using System;
using System.Collections.Generic;
using System.Data;

namespace Evorine.Data.Abstractions
{
    public interface IConnector : IDisposable
    {
        IDataParameter this[string key] { get; }

        IDbCommand Command { get; }
        string CommandText { get; set; }
        IDbConnection Connection { get; }
        IDataReader DataReader { get; set; }
        IDataParameterCollection Parameters { get; }

        void Open();
        void Close();

        int ExecuteNonQuery();
        void ExecuteReader();
        void ExecuteReader(CommandBehavior behavior);
        object ExecuteScalar();

        bool Read();
        bool NextReader();

        IEnumerable<T> ReadAll<T>() where T : new();
        T ReadAndParse<T>() where T : class, new();
    }
}