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

        public ControlPanel()
        {
            InitializeComponent();

            Global.SP_port = new SerialPort();
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
                if (Global.SP_port.IsOpen == false && CONNECTION_cbPort.Text != "" )
                {
                    Console.WriteLine("CONNECTION Open");
                    Global.SP_port.PortName = CONNECTION_cbPort.Text;
                    Global.SP_port.BaudRate = 115200;
                    Global.SP_port.DataBits = 8;
                    Global.SP_port.StopBits = StopBits.One;
                    Global.SP_port.Parity = Parity.None;
                    Global.SP_port.DataReceived += new SerialDataReceivedEventHandler(SerialPort_OnDataReceived);
                    Global.SP_port.Open();
                    
                    CONNECTION_tbStatus.Text = "Connected";
                }
                else
                {
                    Console.WriteLine("Connected already or not selecting port");
                }
            }
            else if (sender == CONNECTION_btnClose)
            {
                if (Global.SP_port.IsOpen == true )
                {
                    Console.WriteLine("CONNECTION Close");
                    Global.SP_port.Close();
                    CONNECTION_tbStatus.Text = "Disconnected";
                }
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
        }

        private void SerialPort_OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readBytes;
            char[] buffer = new char[SERIAL_PACKET_SIZE];

            readBytes = Global.SP_port.Read(buffer, 0, SERIAL_PACKET_SIZE);


            Dispatcher.Invoke(new Action(delegate {
                //LOG_txtInfo.Text += new string(buffer, 0, readBytes);
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

            Global.SP_port.Write(data, 0, SERIAL_PACKET_SIZE);
        }
    }
}
