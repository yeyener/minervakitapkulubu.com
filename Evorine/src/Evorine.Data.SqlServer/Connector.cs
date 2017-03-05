using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Evorine.Data.Abstractions;
using Evorine.Data.ObjectMapper;

namespace Evorine.Data.SqlServer
{
    public class Connector : IConnector
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader dataReader;

        public IDbConnection Connection { get { return connection; } }
        public IDbCommand Command { get { return command; } }

        public IDataReader DataReader
        {
            get
            {
                return dataReader;
            }
            set
            {
                dataReader = (SqlDataReader)value;
            }
        }

        public string CommandText
        {
            get { return command.CommandText; }
            set { command.CommandText = value; }
        }

        public IDataParameterCollection Parameters
        {
            get
            {
                return command.Parameters;
            }
        }

        public IDataParameter this[string key]
        {
            get
            {
                return command.Parameters["key"];
            }
        }


        public Connector(IConnectionStringProvider connectionStringProvider)
        {
            connection = new SqlConnection(connectionStringProvider.GetDefaultConnectionString());
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
        }


        #region Connection Methods

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        #endregion Connection Methods

        #region Execute Methods

        public int ExecuteNonQuery()
        {
            if (connection.State == ConnectionState.Closed) Open();
            return command.ExecuteNonQuery();
        }

        public void ExecuteReader()
        {
            ExecuteReader(CommandBehavior.Default);
        }

        public void ExecuteReader(CommandBehavior behavior)
        {
            if (connection.State == ConnectionState.Closed) Open();
            dataReader = command.ExecuteReader();
        }

        public object ExecuteScalar()
        {
            if (connection.State == ConnectionState.Closed) Open();
            return command.ExecuteScalar();
        }

        #endregion Execute Methods


        #region Reading

        public bool Read()
        {
            return dataReader.Read();
        }

        public bool NextReader()
        {
            return dataReader.NextResult();
        }

        public T ReadAndParse<T>() where T : class, new()
        {
            if (Read())
            {
                var om = ObjectMapper<T>.Instantiate();
                return om.Map(DataReader);
            }
            return null;
        }

        public IEnumerable<T> ReadAll<T>() where T : new()
        {
            var om = ObjectMapper<T>.Instantiate();

            List<T> list = new List<T>();
            while (Read())
                list.Add(om.Map(DataReader));

            DataReader.Close();
            return list;
        }

        #endregion


        public void Dispose()
        {
            if (dataReader != null)
            {
                dataReader.Dispose();
            }
            connection.Close();
            command.Dispose();
            connection.Dispose();
        }        
    }
}