using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using PSWinCom.Gateway.Client;

namespace PSWinComGatewaySendSMSConsoleApp
{
    class Program
    {
        private static ILog log = LogManager.GetLogger("Default");


        static void Main(string[] args)
        {
            log.Info("Sending message");

            try
            {
                var client = new SMSClient();
                client.Username = "username";
                client.Password = "password";

                var message = new Message();
                message.ReceiverNumber = "4799495153";
                message.SenderNumber = "PSWinCom";
                message.Text = "PSWinCom Gateway sample message";

                client.Messages.Add(0, message);
                client.SendMessages();

                log.Info("Sending message finished");
            }
            catch (Exception e)
            {
                log.Error("Exception when sending SMS", e);
            }
        }
    }
}
