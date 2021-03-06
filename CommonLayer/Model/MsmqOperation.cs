using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace CommonLayer.Model
{
    public class MsmqOperation
    {
        MessageQueue msmq = new MessageQueue();  //MessageQueue provides access to a queue on a message queue server.
        public void Sender(string token)
        {
            msmq.Path = @".\private$\Tokens";
            try
            {
                if (!MessageQueue.Exists(msmq.Path))
                {
                    MessageQueue.Create(msmq.Path);
                }
                msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) }); //Formatter is used when object is written to or read from the body
                msmq.ReceiveCompleted += Msmq_ReceiveCompleted;
                msmq.Send(token);
                msmq.BeginReceive();
                msmq.Close();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
       
        private void Msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = msmq.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            // mail sending code smtp 
            string mailReceiver = GetEmailFromToken(token).ToString();
            MailMessage message = new MailMessage("mayuritesting0123@gmail.com", mailReceiver);
            string bodymessage = $"<a href=http://localhost:4200/resetPassword/{token} click me</a>" + "Token-> : " + token;
            ;
            message.Subject = "Sending Email Using Asp.Net & C#";     
            message.Body = bodymessage;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("mayuritesting0123@gmail.com", "testing@95");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1; //get or set credentials which used to authenticate the sender
            try 
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            msmq.BeginReceive();
        }
        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decoded = handler.ReadJwtToken((token));
            var result = decoded.Claims.FirstOrDefault().Value;
            return result;
        }
    }

}

