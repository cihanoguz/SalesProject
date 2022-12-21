using System;
using System.Collections.Generic;

namespace Mobiliva.RabbitMQ.Receiving.Services.Notifications
{
    public interface INotificationManager
    {
        string ReadFileContent(string FileName);
        string SendMail(string Subject, string To /*, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData*/);
        void SendSmpt(string Subject, string To/*, string FileName, Dictionary<string, string> Recipients, dynamic ExtraData*/);
    }
}