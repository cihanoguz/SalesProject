using System;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Mobiliva.RabbitMQ.Receiving.Services.Notifications
{
    public class NotificationManager : INotificationManager
    {
        private readonly IConfiguration _configuration;

        public NotificationManager(IConfiguration configuration)
        {
            configuration = _configuration;
        }

        private string fromMail { get { return _configuration["MailSettingsForSmtp:FromMail"].ToString(); } }
        private string smtpHost { get { return _configuration["MailSettingsForSmtp:SmtpHost"].ToString(); } }
        private string mailAdressPassword { get { return _configuration["MailSettingsForSmtp:MailAdressPasword"].ToString(); } }


        public string ReadFileContent(string FileName)
        {

            using (StreamReader reader = File.OpenText("./wwwroot/Mailing/" + FileName))
            {
                string fileContent = reader.ReadToEnd();
                if (fileContent != null && fileContent != "")
                {
                    return fileContent;
                }
            }

            return "";
        }

        /// <summary>
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="FromName"></param>
        /// <param name="FileName"></param>
        /// <param name="Recipients"></param>
        /// <returns></returns>
        public string SendMail(string Subject, string To /* ,string? FileName, Dictionary<string, string> Recipients, dynamic? ExtraData = null */)
        {
            try
            {
                SendSmpt(Subject, To/*, FileName, Recipients*/);
                // todo log, when,to,content,which user if login

            }
            catch (Exception ex)
            {
                // todo log ex.Message,when,which user if login
            }
            return "";
        }

        public void SendSmpt(string Subject, string To/*, string FileName, Dictionary<string, string> Recipients, dynamic? ExtraData = null*/)
        {
            
            MailAddress fromAdress = new MailAddress(fromMail);
            string mailHost = smtpHost;
            string mailPassword = mailAdressPassword;
            var toAdress = new MailAddress(To);

          /*  string MailContent = ReadFileContent(FileName);
            MailContent = MailContent.Replace("%recipient.FullName%", Recipients["recipient.FullName"]);
            foreach (var item in Recipients)
            {
                MailContent = MailContent.Replace("%" + item.Key + "%", item.Value);
            }*/

            using (var smtp = new System.Net.Mail.SmtpClient
            {
                Host = mailHost,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAdress.Address, mailPassword)
            })

            using (var message = new MailMessage(fromAdress, toAdress) { Subject = Subject/*, Body = MailContent*/, IsBodyHtml = true })
            {
                smtp.Send(message);
            }

        }


    }
}


