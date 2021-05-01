using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Etsy2ParcelMonkeyCSV.Helpers
{
    public static class CsvExportHelper
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CsvImportHelper));

        public static HttpResponseMessage Export<T>(List<T> list, Type classMapType, string fileName, bool hasHeader = true)
        {
            CsvHelper.Configuration.CsvConfiguration csvCommonConfiguration = new CsvHelper.Configuration.CsvConfiguration
            {
                Delimiter = ",",
                HasHeaderRecord = hasHeader
            };

            MemoryStream ms = new MemoryStream();
            StreamWriter swriter = new StreamWriter(ms, Encoding.GetEncoding("windows-1252"));
            var csv = new CsvHelper.CsvWriter(swriter, csvCommonConfiguration);
            csv.Configuration.RegisterClassMap(classMapType);
            if (hasHeader)
            {
                csv.WriteHeader<T>();
            }
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    csv.WriteRecord(list[i]);
                }
                catch (Exception ex)
                {
                    #region Logging

                    if (logger.IsErrorEnabled)
                    {
                        logger.Error(string.Format("L'export de la ligne {0} a échoué", i), ex);
                    }

                    #endregion Logging
                }
            }

            //Flush du writer et remise à zéro du pointeur de lecture du stream
            swriter.Flush();
            ms.Position = 0;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            return response;
        }
    }
}