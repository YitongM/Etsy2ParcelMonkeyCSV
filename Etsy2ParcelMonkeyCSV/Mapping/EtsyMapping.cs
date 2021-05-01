using CsvHelper.Configuration;
using Etsy2ParcelMonkeyCSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etsy2ParcelMonkeyCSV.Mapping
{
    public class EtsyMapping : CsvClassMap<EtsyImport>
    {
        public EtsyMapping()
        {
            Map(x => x.SaleDate).Index(0).Name("Sale Date");
            Map(x => x.OrderID).Index(1).Name("Order ID");
            Map(x => x.BuyerUserID).Index(2).Name("Buyer User ID");
            Map(x => x.FullName).Index(3).Name("Full Name");
            Map(x => x.FirstName).Index(4).Name("First Name");
            Map(x => x.LastName).Index(5).Name("Last Name");
            Map(x => x.NumberofItems).Index(6).Name("Number of Items");
            Map(x => x.PaymentMethod).Index(7).Name("Payment Method");
            Map(x => x.DatePosted).Index(8).Name("Date Posted");
            Map(x => x.Street1).Index(9).Name("Street 1");
            Map(x => x.Street2).Index(10).Name("Street 2");
            Map(x => x.DeliveryCity).Index(11).Name("Delivery City");
            Map(x => x.DeliveryState).Index(12).Name("Delivery State");
            Map(x => x.DeliveryZipcode).Index(13).Name("Delivery Zipcode");
            Map(x => x.DeliveryCountry).Index(14).Name("Delivery Country");
            Map(x => x.Currency).Index(15).Name("Currency");
            Map(x => x.OrderValue).Index(16).Name("Order Value");
            Map(x => x.CouponCode).Index(17).Name("Coupon Code");
            Map(x => x.CouponDetails).Index(18).Name("Coupon Details");
            Map(x => x.DiscountAmount).Index(19).Name("Discount Amount");
            Map(x => x.DeliveryDiscount).Index(20).Name("Delivery Discount");
            Map(x => x.Delivery).Index(21).Name("Delivery");
            Map(x => x.Salestax).Index(22).Name("Sales tax");
            Map(x => x.Ordertotal).Index(23).Name("Order total");
            Map(x => x.Status).Index(24).Name("Status");
            Map(x => x.CardProcessingFees).Index(25).Name("Card Processing Fees");
            Map(x => x.OrderNet).Index(26).Name("Order Net");
            Map(x => x.AdjustedOrderTotal).Index(27).Name("Adjusted Order Total");
            Map(x => x.AdjustedCardProcessingFees).Index(28).Name("Adjusted Card Processing Fees");
            Map(x => x.AdjustedNetOrderAmount).Index(29).Name("Adjusted Net Order Amount");
            Map(x => x.Buyer).Index(30).Name("Buyer");
            Map(x => x.OrderType).Index(31).Name("Order Type");
            Map(x => x.PaymentType).Index(32).Name("Payment Type");
            Map(x => x.InPersonDiscount).Index(33).Name("InPerson Discount");
            Map(x => x.InPersonLocation).Index(34).Name("InPerson Location");
            Map(x => x.SKU).Index(35).Name("SKU");
        }
    }
}