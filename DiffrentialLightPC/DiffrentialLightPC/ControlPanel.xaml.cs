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
            mSerialPort = new SerialPort();
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
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_OnDataReceived);
                mSerialPort.Open();
            }
            else if (sender == CONNECTION_btnClose)
            {
                Console.WriteLine("CONNECTION Close");
            }
            else if ( sender == PWM_btnDuty )
            {
                String[] args = new String[2];
                int ch = PWM_cbChannel.SelectedIndex + 1;

                args[0] = ch.ToString();
                args[1] = PWM_txtDuty.Text;

                SendCommand("setPWMDuty", args);
            }
            else if ( sender == DAC_btnSet )
            {
                String[] args = new string[2];
                int ch = DAC_cbChannel.SelectedIndex + 1;

                args[0] = ch.ToString();
                args[1] = DAC_txtData.Text;

                SendCommand("setDAC", args);
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
            else if ( sender == LOG_btnClear )
            {
                LOG_txtInfo.Text = "";
            }
        }

        private void SerialPort_OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readBytes;
            char[] buffer = new char[SERIAL_PACKET_SIZE];

            readBytes = mSerialPort.Read(buffer, 0, SERIAL_PACKET_SIZE);


            Dispatcher.Invoke(new Action(delegate {
                LOG_txtInfo.Text += new string(buffer, 0, readBytes);
            }));
        }

        public void SendCommand(String cmd, String[] args)
        {
            char[] data = new char[SERIAL_PACKET_SIZE];
            String command;

            command = cmd;
            command += " ";

            for ( int i = 0; i < args.Length; i++ )
            {
                command += args[i];
                command += " ";
            }
            
            for ( int i = 0; i < SERIAL_PACKET_SIZE; i++ )
            {
                if ( i < command.Length )
                {
                    data[i] = command[i];
                }
                else
                {
                    data[i] = '\0';
                }
            }

            mSerialPort.Write(data, 0, SERIAL_PACKET_SIZE);
        }
    }
}
