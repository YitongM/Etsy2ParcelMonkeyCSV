using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Etsy2ParcelMonkeyCSV.Helpers
{
    public class CsvImportHelper
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CsvImportHelper));

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="classMapType"></param>
        /// <param name="hasHeader"></param>
        /// <param name="willThrowOnMissingField">indique si lève une exception quand un champ est manquant</param>
        /// <returns></returns>
        public static List<T> Import<T>(string path, Type classMapType, bool hasHeader = true, bool willThrowOnMissingField = true)
        {
            CsvHelper.Configuration.CsvConfiguration csvCommonConfiguration = new CsvHelper.Configuration.CsvConfiguration
            {
                Delimiter = ",",
                HasHeaderRecord = hasHeader,
                WillThrowOnMissingField = willThrowOnMissingField
            };

            StreamReader file = new StreamReader(path, Encoding.GetEncoding("windows-1252"));
            var csv = new CsvHelper.CsvReader(file, csvCommonConfiguration);
            csv.Configuration.RegisterClassMap(classMapType);
            List<T> list = new List<T>();
            while (csv.Read())
            {
                T record;
                try
                {
                    record = csv.GetRecord<T>();
                }
                catch (CsvHelper.TypeConversion.CsvTypeConverterException ex)
                {
                    #region Logging

                    if (logger.IsErrorEnabled)
                    {
                        logger.Error(string.Format("L'import de la ligne {0} ({1}) a échoué", csv.Row, typeof(T)), ex);
                    }

                    #endregion Logging

                    continue;
                }

                var context = new ValidationContext(record, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(record, context, results, true);
                if (isValid)
                {
                    list.Add(record);
                }
                else
                {
                    var obj = new
                    {
                        Record = record,
                        ValidationResult = results.Select(x => new { MemberNames = x.MemberNames.ToArray(), ErrorMessage = x.ErrorMessage })
                    };

                    #region Logging

                    if (logger.IsErrorEnabled)
                    {
                        logger.Error(string.Format("L'import de la ligne {0} ({1}) a échoué", csv.Row, typeof(T)));
                    }

                    #endregion Logging
                }
            }

            return list;
        }
    }
}