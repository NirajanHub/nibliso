using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NIBLISO.Web.Models;
using OpenIso8583Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NIBLISO.Web.Controllers
{
    public class HomeController : Controller
    {

        const string IPADDRESS = "10.129.153.5";
        const int port = 14221;
        public void isoMessageSend([FromBody] Dictionary<int, String> isoMessageClass)
        {

        }
        private void balanceEnquiry(string[] request)
        {
            var msg = new Iso8583();
            msg.MessageType = 4608;
            msg[Iso8583.Bit._003_PROC_CODE] = request[3];
            msg[Iso8583.Bit._004_TRAN_AMOUNT] = request[4]; ;
            msg[Iso8583.Bit._011_SYS_TRACE_AUDIT_NUM] = request[11]; ;
            msg[Iso8583.Bit._012_LOCAL_TRAN_DATETIME] = request[12]; ;
            msg[Iso8583.Bit._017_CAPTURE_DATE] = request[17]; ;
            msg[Iso8583.Bit._024_FUNC_CODE] = request[24]; ;
            msg[Iso8583.Bit._032_ACQ_INST_ID_CODE] = request[32]; ;
            msg[Iso8583.Bit._034_EXTENDED_PRIMARY_ACCOUNT_NUMBER] = request[34]; ;
            msg[Iso8583.Bit._049_TRAN_CURRENCY_CODE] = request[49]; ;
            msg[Iso8583.Bit._102_ACCOUNT_ID_1] = request[102]; ;
            msg[Iso8583.Bit._123_RECEIPT_DATA] = request[123]; ;
            //  Connect(IPADDRESS, msg.ToMsg());
        }

        private void fundTransfer(string[] request)
        {
            var msg = new Iso8583();
            msg.MessageType = 4608;
            msg[Iso8583.Bit._003_PROC_CODE] = request[3];
            msg[Iso8583.Bit._004_TRAN_AMOUNT] = request[4]; 
            msg[Iso8583.Bit._011_SYS_TRACE_AUDIT_NUM] = request[11]; 
            msg[Iso8583.Bit._012_LOCAL_TRAN_DATETIME] = request[12]; 
            msg[Iso8583.Bit._017_CAPTURE_DATE] = request[17]; 
            msg[Iso8583.Bit._024_FUNC_CODE] = request[24]; 
            msg[Iso8583.Bit._032_ACQ_INST_ID_CODE] = request[32]; 
            msg[Iso8583.Bit._034_EXTENDED_PRIMARY_ACCOUNT_NUMBER] = request[34]; 
            msg[Iso8583.Bit._049_TRAN_CURRENCY_CODE] = request[49]; 
            msg[Iso8583.Bit._102_ACCOUNT_ID_1] = request[102]; 
            msg[Iso8583.Bit._103_ACCOUNT_ID_2] = request[103]; 
            msg[Iso8583.Bit._123_RECEIPT_DATA] = request[123]; 
            //  Connect(IPADDRESS, msg.ToMsg());
        }
    }
}
