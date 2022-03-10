/* ------------------------------------------------------------------------------ *
 * @project     DifferntialLightPC
 * @author      Eungdoo Kim
 * @version     v0.0
 * @date        2022-03-08
 * @brief       Control DiffrentialLight embedded board.
 * ------------------------------------------------------------------------------ *
 * 2022-03-08   Create.
 * ------------------------------------------------------------------------------ */

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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Threading;

namespace DiffrentialLightPC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int SERIAL_PACKET_SIZE = 128;


        private SerialPort mSerialPort;

        public MainWindow()
        {
            InitializeComponent();
            mSerialPort = new SerialPort();
        }

        private void ComboBox_OnDropDownOpened(object sender, EventArgs e)
        {            
            CONNECTION_cbPort.Items.Clear();
            String[] names = SerialPort.GetPortNames();
            for ( int i = 0; i < names.Length; i++ )
            {
                CONNECTION_cbPort.Items.Add(names[i]);
            }
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if ( sender == CONNECTION_btnOpen )
            {
                Console.WriteLine("CONNECTION Open");
                mSerialPort.PortName = CONNECTION_cbPort.Text;
                mSerialPort.BaudRate = 115200;
                mSerialPort.DataBits = 8;
                mSerialPort.StopBits = StopBits.One;
                mSerialPort.Parity   = Parity.None;
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_OnDataReceived);
                mSerialPort.Open();
            }
            else if ( sender == CONNECTION_btnClose )
            {
                Console.WriteLine("CONNECTION Close");
            }
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if ( sender == INFO_txtCommand )
            {
                if ( e.Key == Key.Return && mSerialPort.IsOpen )
                {
                    char[] command = new char[SERIAL_PACKET_SIZE];
                    Console.WriteLine(INFO_txtCommand.Text);
                    char[] txt = INFO_txtCommand.Text.ToCharArray();
                    for ( int i = 0; i < SERIAL_PACKET_SIZE; i++ )
                    {
                        if ( i < txt.Length )
                        {
                            command[i] = txt[i];
                        }
                        else
                        {
                            command[i] = '\0';
                        }
                    }
                    mSerialPort.Write(command, 0, SERIAL_PACKET_SIZE);
                }
            }
        }

        private void SerialPort_OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readBytes;
            char[] buffer = new char[SERIAL_PACKET_SIZE];

            readBytes = mSerialPort.Read(buffer, 0, SERIAL_PACKET_SIZE);


            Dispatcher.Invoke(new Action(delegate {
                INFO_txtLog.Text += new string(buffer, 0, readBytes);
            }));
        }
    }


}
