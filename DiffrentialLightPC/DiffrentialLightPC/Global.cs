using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO.Ports;  // for serial ports

namespace DiffrentialLightPC
{
    public static class Global
    {
        public static SerialPort SP_port = null;

        public static DataTable DT_table = null;
        public static DataTable DT_test = null;

        public static int IP_channel = 18;
        public static int IP_step = 10;
        public static int IP_cycle = 10;
    }
}
