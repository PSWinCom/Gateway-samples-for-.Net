using System;
using System.Collections.Generic;
using System.Threading;
using log4net;
using PSWinCom.PollingApiExampleConsoleApp.PollingApi;

namespace PSWinCom.PollingApiExampleConsoleApp
{
    class Program
    {
        private static ILog log = LogManager.GetLogger("Receiver");

        static void Main(string[] args)
        {
            log.Info("Starting sample app");
            PollSoapClient client = new PollSoapClient();

            // Run the sample for some minutes, normally we would have a windows service running
            for (int i = 0; i < 60; i++)
            {                
                var fetchResponse = client.FetchNextMessage("USERNAME", "PASSWORD", true);

                if (fetchResponse.StatusCode == 200)
                {
                    log.InfoFormat("Service found message with id: {0}, which must be acked.", fetchResponse.MessageID);
                    
                    var ackResult = client.AcknowledgeMessage("USERNAME", "PASSWORD", fetchResponse.MessageID);

                    if (ackResult.StatusCode == 210)
                    {
                        log.InfoFormat("Message was successfully acked, at this we can safely store retrieved message");
                        var message = fetchResponse.SMSMessage;
                        log.InfoFormat("SMS from {0} to {1} with text: {2}", message.SenderNumber, message.SenderNumber, message.Text);
                    }
                    else
                        log.Error("Failed to ack message!");
                }
                else            
                    log.WarnFormat("Service responded {0}, indicating no message can be retrieved.", fetchResponse.StatusCode);
                             
                Thread.Sleep(5000);
            }
        }
    }
}