using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenIso8583Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NIBLISO.Web
{
    public class Program
    {
        const string IPADDRESS = "10.129.153.5";
        const int port = 14221;
        public static void Main(string[] args)
        {
            bimParser();
        }

        private static void bimParser()
        {
            //1. Set/Define Data
            //Message Type Identifier (Financial Message Request)
            var msg = new Iso8583();
            msg.MessageType = 4608;
            msg[Iso8583.Bit._003_PROC_CODE] = "400000";
            msg[Iso8583.Bit._004_TRAN_AMOUNT] = "0000000000020000";
            msg[Iso8583.Bit._011_SYS_TRACE_AUDIT_NUM] = "000000000005";
            msg[Iso8583.Bit._012_LOCAL_TRAN_DATETIME] = "20170104152225";
            msg[Iso8583.Bit._017_CAPTURE_DATE] = "20170104";
            msg[Iso8583.Bit._024_FUNC_CODE] = "200";
            msg[Iso8583.Bit._032_ACQ_INST_ID_CODE] = "504511";
            msg[Iso8583.Bit._034_EXTENDED_PRIMARY_ACCOUNT_NUMBER] = "XXX";
            msg[Iso8583.Bit._049_TRAN_CURRENCY_CODE] = "NPR";
            msg[Iso8583.Bit._102_ACCOUNT_ID_1] = "004        001     00105140008942";
            msg[Iso8583.Bit._103_ACCOUNT_ID_2] = "  004        999     999614201001";
            msg[Iso8583.Bit._123_RECEIPT_DATA] = "ATM";
            Connect(IPADDRESS, msg.ToMsg());

        }
        static void Connect(String server, byte[] message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.

                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                //  Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();
                Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                // Send the message to the connected TcpServer.
                stream.Write(message, 0, message.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                var data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                var msg = new Iso8583();

                Console.WriteLine("Received message : {0}", msg.Unpack(data, 0));
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
