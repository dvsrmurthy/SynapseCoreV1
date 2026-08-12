using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Enums
{
    public enum SmscConnectionType : int
    {
        HTTP = 1,
        SMPP = 2,
        GSM = 3,
    }

    public enum ProtocolModeType:int
    {
        Transciever=2,
        Receiver=3,
        Transmitter=1,
    }

    public enum AutoConnection:int
    {
        On=1,
        Off=2,
    }

    public enum LogCommunication:int
    {
        On=1,
        Off=2,
    }

    public enum Deliveryreport:int
    {
        On=1,
        Off=2,
    }
   
    


}
