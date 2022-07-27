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
using System.Data;
using System.IO;

namespace DiffrentialLightPC
{
    /// <summary>
    /// Interaction logic for StatusPanel.xaml
    /// </summary>
    public partial class StatusPanel : UserControl
    {
        public StatusPanel()
        {
            InitializeComponent();
            InitDataTable();
            InitResultTable();
            InitStatusPanel();
        }

        private void InitResultTable()
        {
            Global.DT_test = new DataTable();
            Global.DT_test.Columns.Add("Test", typeof(string));
            Global.DT_test.Columns[0].ReadOnly = true;

            for (int i = 0; i < Global.IP_channel; i++ )
            {
                string name;
                name = (i + 1) + "ch";
                Global.DT_test.Columns.Add(name, typeof(string));
                Global.DT_test.Columns[i + 1].ReadOnly = true;
            }

            Global.DT_test.Rows.Add("Pass");
            Global.DT_test.Rows.Add("Fail");

            STATUS_dgTest.CanUserAddRows = false;
            STATUS_dgTest.CanUserResizeRows = false;
            STATUS_dgTest.CanUserResizeColumns = false;
            STATUS_dgTest.CanUserSortColumns = false;
            STATUS_dgTest.CanUserReorderColumns = false;
            STATUS_dgTest.ItemsSource = Global.DT_test.DefaultView;
        }

        private void InitDataTable()
        {
            STATUS_tbChannel.Text = Global.IP_channel.ToString();
            STATUS_tbStep.Text = Global.IP_step.ToString();
            STATUS_tbCycle.Text = Global.IP_cycle.ToString();

            Global.DT_table = new DataTable();
            Global.DT_table.Columns.Add("Step", typeof(int));
            Global.DT_table.Columns[0].ReadOnly = true;

            for (int i = 0; i < Global.IP_channel; i++)
            {
                string name;
                name = (i + 1) + "ch";
                Global.DT_table.Columns.Add(name, typeof(int));
            }

            for (int i = 0; i < Global.IP_step; i++)
            {
                Global.DT_table.Rows.Add(i + 1);
            }

            STATUS_dgData.CanUserAddRows = false;
            STATUS_dgData.CanUserResizeRows = false;
            STATUS_dgData.CanUserResizeColumns = false;
            STATUS_dgData.CanUserSortColumns = false;
            STATUS_dgData.CanUserReorderColumns = false;
            STATUS_dgData.ItemsSource = Global.DT_table.DefaultView;
        }


        private void SetDataTable(int num_ch, int num_step)
        {
            if ( Global.DT_table != null )
            {
                Global.DT_table.Dispose();
                Global.DT_table = new DataTable();
            }

            Global.DT_table.Columns.Add("Step", typeof(int));

            for ( int i = 0; i < num_ch; i++ )
            {
                string name;

                name = (i + 1) + "ch";
                Global.DT_table.Columns.Add(name, typeof(string));
            }

            for ( int i = 0; i < num_step; i++ )
            {
                Global.DT_table.Rows.Add();
            }

            STATUS_dgData.ItemsSource = Global.DT_table.DefaultView;

            if ( Global.DT_test != null )
            {
                Global.DT_test.Dispose();
                Global.DT_test = new DataTable();
            }

            Global.DT_test.Columns.Add("Test", typeof(string));

            for (int i = 0; i < num_ch; i++)
            {
                string name;
                name = (i + 1) + "ch";
                Global.DT_test.Columns.Add(name, typeof(string));
            }

            Global.DT_test.Rows.Add("Pass");
            Global.DT_test.Rows.Add("Fail");

            STATUS_dgTest.ItemsSource = Global.DT_test.DefaultView;

        }


        private void InitStatusPanel()
        {
            STATUS_tbChannel.Text = Global.IP_channel.ToString();
            STATUS_tbStep.Text = Global.IP_step.ToString();
            STATUS_tbCycle.Text = Global.IP_cycle.ToString();
        }

        private void STATUS_btnChannel_Click(object sender, RoutedEventArgs e)
        {
            if ( int.TryParse(STATUS_tbChannel.Text, out Global.IP_channel) )
            {
                SetDataTable(Global.IP_channel, Global.IP_step);
            }
        }

        private void STATUS_btnStep_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(STATUS_tbStep.Text, out Global.IP_step))
            {
                SetDataTable(Global.IP_channel, Global.IP_step);
            }
        }

        private void STATUS_btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void STATUS_btnStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void STATUS_btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "CSV file|*.csv";
            dialog.Title = "Open an CSV File";
            dialog.ShowDialog();
        }

        private void STATUS_btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();

            dialog.Filter = "CSV file|*.csv";
            dialog.Title = "Save an CSV File";

            dialog.ShowDialog();

            Console.WriteLine("{0}", dialog.FileName);

            if ( dialog.FileName != "" )
            {
                SaveTableToCsv(dialog.FileName);
            }
        }

        private void SaveTableToCsv(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);

            for ( int i = 0; i < Global.DT_table.Columns.Count; i++ )
            {
                writer.Write(Global.DT_table.Columns[i].ToString().Trim());
                if ( i < Global.DT_table.Columns.Count -1 )
                {
                    writer.Write(",");
                }
            }

            writer.Write(writer.NewLine);


            writer.Close();

            return;

            foreach ( DataRow row in Global.DT_table.Rows )
            {
                for ( int i = 0; i < Global.DT_table.Columns.Count; i++)
                {
                   // if ( !Convert.IsDBNull(row))


                }




            }
            




        }
    }
}
