using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlVersionControlSystem.Dal
{
    internal class DbQueryExecute:Connection
    {
        public static SqlConnection _connection;

        public DbQueryExecute()
        {
            _connection = ConnectionOpen();
        }
        private static SqlCommand CreateSqlCommand(string command)
        {
            SqlCommand commands = new SqlCommand(command, _connection);
            commands.CommandType = CommandType.Text;
            return commands;
        }

        private static SqlCommand CreateSqlCommand(string spName, IEnumerable<SqlParameter> parameter)
        {
            SqlCommand command = new SqlCommand(spName, _connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter sp in parameter)
            {
                command.Parameters.Add(sp);
            }
            return command;
        }

        /// <summary>
        /// Komutu alıp SqlDataReader tipinden geri dönen metod. 
        /// </summary>
        /// <param name="sorgu"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(SpParameter spParameter)
        {
            SqlCommand command = CreateSqlCommand(spParameter._spName, spParameter.Parameters);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Gelen Değer Başarılı mı Başarısız mı Onun Kontrolünü yapıyor
        /// </summary>
        /// <param name="sorgu"></param>
        /// <returns></returns>
        public bool ExecuteNonQuery(SpParameter spParameter)
        {
            SqlCommand command = CreateSqlCommand(spParameter._spName, spParameter.Parameters);
            return command.ExecuteNonQuery() == 1 ? true : false;
        }
        /// <summary>
        /// Gelen tek değeri döndürür
        /// </summary>
        /// <param name="spParameter"></param>
        /// <returns></returns>
        public object ExecuteScalar(SpParameter spParameter)
        {
            SqlCommand command = CreateSqlCommand(spParameter._spName, spParameter.Parameters);
            return command.ExecuteScalar();
        }

        public object ExecuteScalar(string spParameter)
        {
            SqlCommand command = CreateSqlCommand(spParameter);
            return command.ExecuteScalar();
        }

        public DataTable GetData(string spParameter)
        {
            SqlCommand command = CreateSqlCommand(spParameter);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

    }
}
