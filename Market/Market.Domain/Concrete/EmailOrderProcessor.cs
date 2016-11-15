using System;
using System.Net;
using System.Net.Mail;
using Market.Domain.Abstract;
using Market.Domain.Entities;
using System.Text;
namespace Market.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "skarapugina@mail.ua";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPass";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"C:\Users\Светлана\Desktop\Mac\Market\Mail";
    }
  public  class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings mailSttings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            mailSttings = settings;
        }
        public void ProcessOrder(Cart cart, ShopingDetails shopDetails)
        {
            using(var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = mailSttings.UseSsl;
                smtpClient.Host = mailSttings.ServerName;
                smtpClient.Port = mailSttings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(mailSttings.Username, mailSttings.Password);
                if (mailSttings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = mailSttings.FileLocation;
                    smtpClient.EnableSsl = false;

                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items: ");
                foreach(var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity, line.Product.Name, subtotal);
                }
                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalVAlue())
                .AppendLine("---")
                .AppendLine("Ship to:")
                .AppendLine(shopDetails.Name)
                .AppendLine(shopDetails.line1)
                .AppendLine(shopDetails.line2 ?? "")
                .AppendLine(shopDetails.line3 ?? "")
                .AppendLine(shopDetails.City)
                .AppendLine(shopDetails.State ?? "")
                .AppendLine(shopDetails.Zip)
                .AppendLine("---")
                .AppendFormat("Gift wrap: {0}", shopDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    mailSttings.MailFromAddress,
                    mailSttings.MailToAddress,
                    "new order submitted!",
                    body.ToString()
                    );
                if (mailSttings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
