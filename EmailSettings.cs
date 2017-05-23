using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entites;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Concrete
{
  public class EmailSettings
    {
        public string MailToAddress = "abanoubsobii@gmail.com";
        public string MailFromAddress = "abanoubsobii@gmail.com";
        public bool UseSsl = true;
        public string Username = "abanoubsobii@gmail.com";
        public string Password = "magedzaher";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\orders_bookstore_email";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emaisetting;
        public EmailOrderProcessor(EmailSettings setting)
        {
            emaisetting = setting;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingdetails)
        {
            using (var Smpt  = new SmtpClient())
            {
                Smpt.EnableSsl = emaisetting.UseSsl;
                Smpt.Host = emaisetting.ServerName;
                Smpt.Port = emaisetting.ServerPort;
                Smpt.UseDefaultCredentials = false;
                Smpt.Credentials = new NetworkCredential(emaisetting.Username, emaisetting.Password);
                if (emaisetting.WriteAsFile)
                {
                    Smpt.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    Smpt.PickupDirectoryLocation = emaisetting.FileLocation;
                    Smpt.EnableSsl = false;
                }

            
            StringBuilder body = new StringBuilder()
                .AppendLine("A New Order Has been Submitted")
                .AppendLine("-------------------")
                .AppendLine("Books:");
            foreach (var line in cart.Lines)
            {
                var subtotal = line.Quantity * line.Book.price;
                body.AppendFormat("{0}x{1} subtotal : {2 :c}", line.Quantity, line.Book.Title, subtotal);
            }
            body.AppendFormat("Total : {0:c}", cart.ComputeTotalValue())
                .AppendLine("------------")
                .AppendLine("Ship to")
                .AppendLine(shippingdetails.Name)
                .AppendLine(shippingdetails.Line1)
                .AppendLine(shippingdetails.Line2)
                .AppendLine(shippingdetails.Country)
                .AppendLine(shippingdetails.City)
                .AppendLine(shippingdetails.State)
                .AppendLine("------------")
                .AppendFormat("Gift Wrap :{0}", shippingdetails.GiftWrap ? "yes" : "No");
            MailMessage mailmessage = new MailMessage(emaisetting.MailFromAddress, emaisetting.MailToAddress, "New order subnitted", body.ToString());
            if (emaisetting.WriteAsFile)
                mailmessage.BodyEncoding = Encoding.ASCII;
                try
                {
                    Smpt.Send(mailmessage);
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }
    }
}
