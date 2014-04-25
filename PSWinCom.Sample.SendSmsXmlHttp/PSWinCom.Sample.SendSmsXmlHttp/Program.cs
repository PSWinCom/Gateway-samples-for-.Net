using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using log4net;

namespace PSWinCom.Sample.SendSmsXmlHttp
{
    /// <summary>
    /// Sample application for sending a SMS with the PSWinCom SMS Gateway XML by HTTP API
    /// https://wiki.pswin.com/Gateway%20XML%20API.ashx
    /// </summary>
    class Program
    {
        private static ILog log = LogManager.GetLogger("Default");
 
        static void Main(string[] args)
        {                    
            string xml = "<?xml version=\"1.0\"?><SESSION><CLIENT>USERNAMEHERE</CLIENT><PW>PASSWORDHERE</PW><MSGLST><MSG><ID>1</ID><TEXT>Hei! æøå ÆØÅ</TEXT><SND>PSWinCom</SND><RCV>471234567</RCV></MSG></MSGLST></SESSION>";

            byte[] byteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(xml);
            
            var request = WebRequest.Create("https://secure.pswin.com/XMLHttpWrapper/process.aspx");

            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = byteArray.Length;  
           
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
          
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            
            log.Debug(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}