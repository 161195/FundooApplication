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
        MessageQueue msmq = new MessageQueue();
        public void Sender(string token)
        {
            msmq.Path = @".\private$\Tokens";

            try
            {
                if (!MessageQueue.Exists(msmq.Path))
                {
                    MessageQueue.Create(msmq.Path);
                }

                msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

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
            string Subject = "Link to reset your FundooNotes App Credentials";
            string body = JwtDecode(token);
            // mail sending code smtp 
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mayuritesting0123@gmail.com", "testing@95"),
                EnableSsl = true,
            };

            smtpClient.Send("mayuritesting0123@gmail.com", body, Subject, token);

            // msmq receiver
            msmq.BeginReceive();
        }
        public static string JwtDecode(string token)
        {
            var jwtDecode = token;
            var handler = new JwtSecurityTokenHandler();
            var decoded = handler.ReadJwtToken((jwtDecode));
            var result = decoded.Claims.FirstOrDefault().Value;
            return result;
        }
    }

}

