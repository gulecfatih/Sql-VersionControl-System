using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlVersionControlSystem.Dal
{
    public class SpParameter
    {
        public Dictionary<string, SqlParameter> parameter;
        public string _spName;
        /// <summary>
        /// spName Doldurur Ve Parameter oluşturur
        /// </summary>
        /// <param name="spName"></param>
        public SpParameter(String spName)
        {
            _spName = spName;
            parameter = new Dictionary<string, SqlParameter>();
        }
        /// <summary>
        /// Paramter değerlerini Gönderir
        /// </summary>
        public IEnumerable<SqlParameter> Parameters
        {
            get { return parameter.Values; }
        }
        /// <summary>
        /// sql parametrelerini Set Eden Fonksiyon
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sqlDbType"></param>
        /// <param name="parameterDirection"></param>
        /// <param name="value"></param>
        public void Parameter(string name, SqlDbType sqlDbType, ParameterDirection parameterDirection, object value)
        {
            SqlParameter sqlParameter = new SqlParameter(name, value);
            sqlParameter.SqlDbType = sqlDbType;
            sqlParameter.Value = value != DBNull.Value ? value : DBNull.Value;
            sqlParameter.Direction = parameterDirection;
            parameter.Add(name, sqlParameter);
        }
        /// <summary>
        /// Int parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Int(string name, int? value)
        {
            Parameter(name, SqlDbType.Int, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Int parametresini gönderir gönderilen değeri çeker sonra geri çeker
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void IntOutput(string name, int? value)
        {
            Parameter(name, SqlDbType.Int, ParameterDirection.InputOutput, value);
        }
        /// <summary>
        /// String parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void String(string name, string value)
        {
            Parameter(name, SqlDbType.VarChar, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Decimal parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Decimal(string name, decimal value)
        {
            Parameter(name, SqlDbType.Decimal, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Date parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Date(string name, DateTime value)
        {
            Parameter(name, SqlDbType.Date, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Long parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Long(string name, long value)
        {
            Parameter(name, SqlDbType.BigInt, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Char parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Char(string name, char value)
        {
            Parameter(name, SqlDbType.Char, ParameterDirection.Input, value);
        }
        /// <summary>
        /// Bool parameter gönderir
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Bool(string name, bool value)
        {
            Parameter(name, SqlDbType.Bit, ParameterDirection.Input, value);
        }
    }
}
