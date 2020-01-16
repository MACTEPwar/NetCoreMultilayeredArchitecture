using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DAL.Infrastructure
{
    /// <summary>
    /// Содержит объект для работы с конфигурацией
    /// </summary>
    public static class Config
    {
        private static IConfiguration _config;
        /// <summary>
        /// Инициализирует конфиг
        /// </summary>
        /// <param name="config"></param>
        public static void Instanse(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Получить значение
        /// </summary>
        /// <typeparam name="T">Тип получаемого значения</typeparam>
        /// <param name="key">Название переменной</param>
        /// <returns>Значение с конфига</returns>
        public static T Get<T>(string key)
        {
            try
            {
                var c = _config[key];
                return (T)Convert.ChangeType(c, typeof(T));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //TODO: не работает Config.Set((string,string) obj)
        /// <summary>
        /// Сохраняет переменуую в конфиг
        /// </summary>
        /// <param name="obj">(Параметр,значение)</param>
        public static void Set((string, string) obj)
        {
            try
            {
                _config.Bind(obj.Item1, obj.Item2);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
