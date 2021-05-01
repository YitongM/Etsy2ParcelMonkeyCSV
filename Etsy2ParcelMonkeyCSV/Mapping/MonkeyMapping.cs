using CsvHelper.Configuration;
using Etsy2ParcelMonkeyCSV.Models;

namespace Etsy2ParcelMonkeyCSV.Mapping
{
    public class MonkeyMapping : CsvClassMap<MonkeyExport>
    {
        public MonkeyMapping()
        {
            Map(x => x.CustomerShipmentReference).Index(0).Name("Customer Shipment Reference");
            Map(x => x.OrderDate).Index(1).Name("Ordered Date");
            Map(x => x.Weight).Index(2).Name("Weight");
            Map(x => x.Length).Index(3).Name("Length");
            Map(x => x.Width).Index(4).Name("Width");
            Map(x => x.Height).Index(5).Name("Height");
            Map(x => x.SenderName).Index(6).Name("Sender Name");
            Map(x => x.SenderEmail).Index(7).Name("Sender Email");
            Map(x => x.SenderPhone).Index(8).Name("Sender Phone");
            Map(x => x.SenderAddressOrganisation).Index(9).Name("Sender Address Organisation");
            Map(x => x.SenderAddressLine1).Index(10).Name("Sender Address Line 1");
            Map(x => x.SenderAddressLine2).Index(11).Name("Sender Address Line 2");
            Map(x => x.SenderAddressCity).Index(12).Name("Sender Address Town / City");
            Map(x => x.SenderAddressProvince).Index(13).Name("Sender Address State / County / Province");
            Map(x => x.SenderAddressPostcode).Index(14).Name("Sender Address Postcode");
            Map(x => x.SenderAddressCountryCode).Index(15).Name("Sender Country Code");
            Map(x => x.IsSenderAddressResidential).Index(16).Name("Sender Address Residential");
            Map(x => x.CollectionNotes).Index(17).Name("Collection Notes");
            Map(x => x.RecipientName).Index(18).Name("Recipient Name");
            Map(x => x.RecipientEmail).Index(19).Name("Recipient Email");
            Map(x => x.RecipientPhone).Index(20).Name("Recipient Phone");
            Map(x => x.DeliveryAddressOrganisation).Index(21).Name("Delivery Address Organisation");
            Map(x => x.DeliveryAddressLine1).Index(22).Name("Delivery Address Line 1");
            Map(x => x.DeliveryAddressLine2).Index(23).Name("Delivery Address Line 2");
            Map(x => x.DeliveryAddressCity).Index(24).Name("Delivery Address Town / City");
            Map(x => x.DeliveryAddressProvince).Index(25).Name("Delivery Address State / County / Province");
            Map(x => x.DeliveryAddressPostcode).Index(26).Name("Delivery Address Postcode");
            Map(x => x.DeliveryAddressCountrycode).Index(27).Name("Delivery Country Code");
            Map(x => x.IsDeliveryAddressResidential).Index(28).Name("Delivery Address Residential");
            Map(x => x.DeliveryNote).Index(29).Name("Delivery Notes");
            Map(x => x.CustomsInvoiceType).Index(30).Name("Customs Invoice Type");
            Map(x => x.CustomsExportReason).Index(31).Name("Customs Export Reason");
            Map(x => x.CustomsExportType).Index(32).Name("Customs Export Type");
            Map(x => x.CodeCountryManufacture).Index(33).Name("Code of Country of Manufacture");
            Map(x => x.SenderCustomsType).Index(34).Name("Sender Customs Type");
            Map(x => x.SenderCustomsTaxReference).Index(35).Name("Sender Customs Tax Reference");
            Map(x => x.SenderCustomsCompanyName).Index(36).Name("Sender Customs Company Name");
            Map(x => x.RecipientCustomsType).Index(37).Name("Recipient Customs Type");
            Map(x => x.RecipientCustomsTaxReference).Index(38).Name("Recipient Customs Tax Reference");
            Map(x => x.RecipientCustomsCompanyName).Index(39).Name("Recipient Customs Company Name");
            Map(x => x.CurrencyCode).Index(40).Name("Product Price Currency Code");
            Map(x => x.ProductDescription).Index(41).Name("Product Description");
            Map(x => x.ProductQuantity).Index(42).Name("Product Quantity");
            Map(x => x.ProductUnitPrice).Index(43).Name("Product Unit Price");
        }
    }
}