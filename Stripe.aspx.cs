using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;

namespace StripePaymentSample
{
    public partial class Stripe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

       

          
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string tokenId = GetTokenId();
            string CustomerId = GetCustomerId(tokenId, "prince@gmail.com", "Prince kuamr", "littleprince");
            string chargeid = ChargeCustomer(CustomerId, true, 1000, 123);

            if (!string.IsNullOrEmpty(chargeid))
            {
                paymentmessage.Text = "Payment Done sucessfully with charge Id" + chargeid;
            }
            else
            {
                paymentmessage.Text = "SOME ERROR OCCURED"; 
            }


        }
        public string ChargeCustomer(string tokenId, bool capture, double? amount, long cartId)
        {
            string message = "";
            StripeConfiguration.ApiKey = "sk_test_NBBwzubTNLQUD0hen0RGqpZy00Jf1PT0gS";
            try
            {


                var myCharge = new ChargeCreateOptions();
                myCharge.Amount = Convert.ToInt64(amount);
                myCharge.Currency = "aud";
                myCharge.Description = capture == true ? "Capture of charge " + amount + " for " + tokenId : "Authorisation for charge " + amount + " for " + tokenId;
                myCharge.Capture = capture;
                myCharge.Customer = tokenId;
                // myCharge.Source = tokenId;
                var chargeService = new ChargeService();
                var stripeCharge = chargeService.Create(myCharge);
                message = stripeCharge.Id;
            }
            catch (StripeException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;

        }
        public string GetTokenId()
        {
            string message = "";
            StripeConfiguration.ApiKey = "sk_test_NBBwzubTNLQUD0hen0RGqpZy00Jf1PT0gS";
            var myToken = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {

                    Number = "4111111111111111",
                    ExpYear = Convert.ToInt64(23),
                    ExpMonth = Convert.ToInt64(1),
                    Cvc = "111",
                    AddressCountry = "Australia",
                    AddressLine1 = "Prince",
                    AddressLine2 = "Prince",
                    AddressCity = "sydney",
                    AddressState = "sydney",
                    AddressZip = "4026",
                    Name = "Prince"
                }
            };
            try
            {
                var tokenService = new TokenService();
                Token stripeToken = tokenService.Create(myToken);
                message = stripeToken.Id;
            }
            catch (StripeException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            
            return message;
        }

        public string GetCustomerId(string tokenId, string email, string fullname, string buisnessname)
        {
            string message = "";
            StripeConfiguration.ApiKey = "sk_test_NBBwzubTNLQUD0hen0RGqpZy00Jf1PT0gS";
            try
            {
                // Create a Customer:
                var customerOptions = new CustomerCreateOptions
                {
                    Source = tokenId,
                    Email = email,
                    Name = fullname + " ," + buisnessname

                };
                var customerService = new CustomerService();
                Customer customer = customerService.Create(customerOptions);
                message = customer.Id;
            }
            catch (StripeException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return message;
        }


    }
}