using System.Net;
using System.Net.Mail;
using System.Text;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;

namespace SportShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAdress = "orders@example.com";
        public string MailFromAddress = "sportShop@example.com";
        public bool UseSsl = true;
        public string Username = "UserSmtp";
        public string Password = "PasswordSmtp";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sport_shop_emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            _emailSettings = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username,_emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("New order")
                    .AppendLine("--------")
                    .AppendLine("Products:");

                foreach (var cartLine in cart.Lines)
                {
                    var subtotal = cartLine.Product.Price*cartLine.Quantity;
                    body.AppendFormat("{0} x {1} (value: {2:c}", cartLine.Quantity, cartLine.Product.Name, subtotal);
                }

                body.AppendFormat("Total value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("--------")
                    .AppendLine("Shipping for:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("----------")
                    .AppendFormat("Gift packaging: {0}", shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(_emailSettings.MailFromAddress,_emailSettings.MailToAdress,"New order",body.ToString());


                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                //smtpClient.Send(mailMessage);
            }
        }
    }
}
