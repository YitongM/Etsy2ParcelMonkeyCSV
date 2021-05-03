using Etsy2ParcelMonkeyCSV.Helper;
using Etsy2ParcelMonkeyCSV.Helpers;
using Etsy2ParcelMonkeyCSV.Mapping;
using Etsy2ParcelMonkeyCSV.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Etsy2ParcelMonkeyCSV
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Log the starting
            ILog logger = LogManager.GetLogger(typeof(Program));
            logger.Info("Starting the import");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please paste the Etsy CSV folder path on your drive:");
            Console.ForegroundColor = ConsoleColor.White;
            string importFolderPath = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please paste the Etsy file name with the file extension \".csv\":");
            Console.ForegroundColor = ConsoleColor.White;
            string importFilename = Console.ReadLine();

            string importFullPath = importFolderPath + "\\" + importFilename;

            logger.InfoFormat("Got the full file path: {0}", importFullPath);

            List<EtsyImport> orders;

            try
            {
                #region Import the Etsy data

                orders = CsvImportHelper.Import<EtsyImport>(importFullPath, typeof(EtsyMapping), true, false);
                WriteSuccessMessage(string.Format("{0} orders successfully imported.", orders.Count()));
                logger.InfoFormat("{0} orders successfully imported.", orders.Count());

                #endregion Import the Etsy data

                #region Prepare the export template

                // Filter only the orders which don't have a DatePosted yet, cuz those with a DatePosted are already shipped.
                List<EtsyImport> filteredOrders = orders.FindAll(o => o.DatePosted == string.Empty);
                Console.WriteLine(string.Format("Found {0} order(s) not posted yet. Only the orders without a posted date in the Etsy file will be exported.", filteredOrders.Count()));

                List<MonkeyExport> exportList = new List<MonkeyExport>();
                int exportCounter = 0;

                foreach (EtsyImport order in filteredOrders)
                {
                    // Convert country name to ISO country code (2 letters)
                    var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
                    RegionInfo filteredRegion = regions.FirstOrDefault(r => r.EnglishName.Contains(order.DeliveryCountry));
                    string countryCode = filteredRegion.TwoLetterISORegionName;

                    // Doc ParcelMonkey https://www.parcelmonkey.com/bulk-shipping/csv
                    // ParcelMonkey CSV spec https://s3-eu-west-1.amazonaws.com/international-live/files/bulk-shipping-parcel-monkey-csv-spec.pdf
                    exportList.Add(new MonkeyExport
                    {
                        CustomerShipmentReference = order.OrderID,
                        OrderDate = order.SaleDate,
                        Weight = ConfigurationManager.AppSettings["Weight"],
                        Length = ConfigurationManager.AppSettings["Length"],
                        Width = ConfigurationManager.AppSettings["Width"],
                        Height = ConfigurationManager.AppSettings["Height"],
                        SenderName = ConfigurationManager.AppSettings["SenderName"],
                        SenderEmail = ConfigurationManager.AppSettings["SenderEmail"],
                        SenderPhone = ConfigurationManager.AppSettings["SenderEmail"],
                        SenderAddressOrganisation = ConfigurationManager.AppSettings["SenderEmail"],
                        SenderAddressLine1 = ConfigurationManager.AppSettings["SenderAddressLine1"],
                        SenderAddressLine2 = ConfigurationManager.AppSettings["SenderAddressLine2"],
                        SenderAddressCity = ConfigurationManager.AppSettings["SenderAddressCity"],
                        SenderAddressProvince = ConfigurationManager.AppSettings["SenderAddressProvince"],
                        SenderAddressPostcode = ConfigurationManager.AppSettings["SenderAddressPostcode"],
                        SenderAddressCountryCode = ConfigurationManager.AppSettings["SenderAddressCountryCode"],
                        IsSenderAddressResidential = ConfigurationManager.AppSettings["IsSenderAddressResidential"],
                        CollectionNotes = "",
                        RecipientName = order.FullName,
                        RecipientEmail = "marjorie@spicyfrog64.com",
                        RecipientPhone = "0611400478",
                        DeliveryAddressOrganisation = "",
                        DeliveryAddressLine1 = order.Street1,
                        DeliveryAddressLine2 = order.Street2,
                        DeliveryAddressCity = order.DeliveryCity,
                        DeliveryAddressProvince = order.DeliveryState,
                        DeliveryAddressPostcode = order.DeliveryZipcode,
                        DeliveryAddressCountrycode = countryCode,
                        IsDeliveryAddressResidential = "Yes",
                        DeliveryNote = "",
                        CustomsInvoiceType = ConfigurationManager.AppSettings["CustomsInvoiceType"],
                        CustomsExportReason = ConfigurationManager.AppSettings["CustomsExportReason"],
                        CustomsExportType = ConfigurationManager.AppSettings["CustomsExportType"],
                        CodeCountryManufacture = ConfigurationManager.AppSettings["CodeCountryManufacture"],
                        SenderCustomsType = ConfigurationManager.AppSettings["SenderCustomsType"],
                        SenderCustomsTaxReference = ConfigurationManager.AppSettings["SenderCustomsTaxReference"],
                        SenderCustomsCompanyName = ConfigurationManager.AppSettings["SenderCustomsCompanyName"],
                        RecipientCustomsType = ConfigurationManager.AppSettings["RecipientCustomsType"],
                        RecipientCustomsTaxReference = "",
                        RecipientCustomsCompanyName = "",
                        CurrencyCode = ConfigurationManager.AppSettings["CurrencyCode"],
                        ProductDescription = ConfigurationManager.AppSettings["ProductDescription"],
                        ProductQuantity = order.NumberofItems,
                        ProductUnitPrice = (float.Parse(order.OrderValue, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(order.NumberofItems)).ToString()
                    });

                    exportCounter++;
                }

                try
                {
                    DateTime today = DateTime.Now;
                    string exportPath = string.Format("{0}\\Exports\\", Directory.GetCurrentDirectory());
                    string exportFilename = string.Format("exported-orders_{0}.csv", today.ToString("ddMMyyyy-HHmm"));
                    HttpResponseMessage response = CsvExportHelper.Export<MonkeyExport>(exportList, typeof(MonkeyMapping), exportFilename, true);

                    var stream = AsyncHelper.RunSync(async () => await response.Content.ReadAsStreamAsync());

                    // Saving file to the desired path
                    using (var file = File.Create(exportPath + exportFilename))
                    {
                        CopyStream(stream, file);
                    }

                    WriteSuccessMessage(string.Format("NICE!!! CSV file with {0} order(s) exported successfully!!", exportCounter));
                    logger.InfoFormat("NICE!!! CSV file with {0} order(s) exported successfully!!", exportCounter);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(":) If you find this little tool useful, buy me a coffee: https://paypal.me/opaleLBC");
                    Console.WriteLine("Thank you! ^_^");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    WriteErrorMessage(ex.Message);
                    logger.ErrorFormat("Error during exporting. Error message: {0}", ex.Message);
                }

                #endregion Prepare the export template
            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex.Message);
                logger.ErrorFormat("Error during importing. Error message: {0}", ex.Message);
            }

            Console.ReadLine();
        }

        private static void WriteErrorMessage(string errorMessage)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("/!\\ ERROR: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("Error message: {0}", errorMessage));
            // Reset Console colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void WriteSuccessMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            // Reset Console colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[input.Length];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            // Reset input position to beginning to use it later
            input.Seek(0, SeekOrigin.Begin);
        }
    }
}