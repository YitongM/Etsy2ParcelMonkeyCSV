namespace Etsy2ParcelMonkeyCSV.Models
{
    public class MonkeyExport
    {
        public string CustomerShipmentReference { get; set; }

        public string OrderDate { get; set; }

        public string Weight { get; set; }

        public string Length { get; set; }

        public string Width { get; set; }

        public string Height { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string SenderPhone { get; set; }

        public string SenderAddressOrganisation { get; set; }

        public string SenderAddressLine1 { get; set; }

        public string SenderAddressLine2 { get; set; }

        public string SenderAddressCity { get; set; }

        public string SenderAddressProvince { get; set; }

        public string SenderAddressPostcode { get; set; }

        public string SenderAddressCountryCode { get; set; }

        public string IsSenderAddressResidential { get; set; }

        public string CollectionNotes { get; set; }

        public string RecipientName { get; set; }

        public string RecipientEmail { get; set; }

        public string RecipientPhone { get; set; }

        public string DeliveryAddressOrganisation { get; set; }

        public string DeliveryAddressLine1 { get; set; }

        public string DeliveryAddressLine2 { get; set; }

        public string DeliveryAddressCity { get; set; }

        public string DeliveryAddressProvince { get; set; }

        public string DeliveryAddressPostcode { get; set; }

        public string DeliveryAddressCountrycode { get; set; }

        public string IsDeliveryAddressResidential { get; set; }

        public string DeliveryNote { get; set; }

        public string CustomsInvoiceType { get; set; }

        public string CustomsExportReason { get; set; }

        public string CustomsExportType { get; set; }

        public string CodeCountryManufacture { get; set; }

        public string SenderCustomsType { get; set; }

        public string SenderCustomsTaxReference { get; set; }

        public string SenderCustomsCompanyName { get; set; }

        public string RecipientCustomsType { get; set; }

        public string RecipientCustomsTaxReference { get; set; }

        public string RecipientCustomsCompanyName { get; set; }

        public string CurrencyCode { get; set; }

        public string ProductDescription { get; set; }

        public string ProductQuantity { get; set; }

        public string ProductUnitPrice { get; set; } // On Etsy, we have "Order value" and "Number of items", so we can calculate an average unit price.
    }
}