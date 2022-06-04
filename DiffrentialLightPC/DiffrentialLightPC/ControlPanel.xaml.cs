using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;  // for serial ports

namespace DiffrentialLightPC
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : UserControl
    {
        private const int SERIAL_PACKET_SIZE = 128;
        private SerialPort mSerialPort;

        public ControlPanel()
        {
            InitializeComponent();
        }

        private void ComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            CONNECTION_cbPort.Items.Clear();
            String[] names = SerialPort.GetPortNames();
            for (int i = 0; i < names.Length; i++)
            {
                CONNECTION_cbPort.Items.Add(names[i]);
            }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender == CONNECTION_btnOpen)
            {
                Console.WriteLine("CONNECTION Open");
                mSerialPort.PortName = CONNECTION_cbPort.Text;
                mSerialPort.BaudRate = 115200;
                mSerialPort.DataBits = 8;
                mSerialPort.StopBits = StopBits.One;
                mSerialPort.Parity = Parity.None;
//                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_OnDataReceived);
                mSerialPort.Open();
            }
            else if (sender == CONNECTION_btnClose)
            {
                Console.WriteLine("CONNECTION Close");
            }
            else if ( sender == LUMI_btnSet )
            {


            }
            else if ( sender == LUMI_btnInc )
            {


            }
            else if ( sender == LUMI_btnDec )
            {


            }
            else if ( sender == LUMI_btnMin )
            {

            }
            else if ( sender == LUMI_btnMax )
            {


            }
            else if ( sender == RST_btnFac )
            {



            }
            else if ( sender == RST_btnUser )
            {



            }
        }


    }
}
