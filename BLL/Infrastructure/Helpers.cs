using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;

namespace BLL.Infrastructure
{
    /// <summary>
    /// Содержит вспомогательные методы
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Переводит unix формат(секунды) в DateTime
        /// </summary>
        /// <param name="timestamp">Секунды</param>
        /// <returns>Дата</returns>
        public static DateTime ToDate(this int timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Переводит unix формат(секунды) в DateTime
        /// </summary>
        /// <param name="timestamp">Секунды</param>
        /// <returns>Дата</returns>
        public static DateTime ToDate(this long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Переводит дату в unix формат.
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Int unix дата в секундах.</returns>
        public static int ToUnixTime(this DateTime date)
        {
            DateTime zero = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = date - zero;
            return (int)Math.Truncate(span.TotalSeconds);
        }

        /// <summary>
        /// Переводит объект obj в JSON
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Строку в формате JSON</returns>
        public static string ToJSON(this object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                // _logger.Debug("BLL.Infrastructure.Helper.ToJSON : Не удалось конвертировать объект в JSON");
                return string.Empty;
            }

        }

        public static string ToSql<TEntity>(this IQueryable<TEntity> query)
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var enumeratorType = enumerator.GetType();
            var selectFieldInfo = enumeratorType.GetField("_selectExpression", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException($"cannot find field _selectExpression on type {enumeratorType.Name}");
            var sqlGeneratorFieldInfo = enumeratorType.GetField("_querySqlGeneratorFactory", BindingFlags.NonPublic | BindingFlags.Instance) ?? throw new InvalidOperationException($"cannot find field _querySqlGeneratorFactory on type {enumeratorType.Name}");
            var selectExpression = selectFieldInfo.GetValue(enumerator) as SelectExpression ?? throw new InvalidOperationException($"could not get SelectExpression");
            var factory = sqlGeneratorFieldInfo.GetValue(enumerator) as IQuerySqlGeneratorFactory ?? throw new InvalidOperationException($"could not get IQuerySqlGeneratorFactory");
            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);
            var sql = command.CommandText;
            return sql;
        }

        /// <summary>
        /// Получает md5
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Строку в md5</returns>
        public static string getMd5Hash(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
