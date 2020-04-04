using System;
using System.Collections.Generic;
using System.IO;
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

namespace INICreator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupComboBoxes();
        }

        private void SetupComboBoxes()
        {
            string[] Regions = { "EU", "US", "JP" };
            string[] Type = { "Auto", "SRAM", "Flash", "EEPROM" };
            string[] Size = { "512", "2048", "4K", "16K" };
            cbRegion.ItemsSource = Regions;
            cbRegion.SelectedIndex = 0;
            cbType.ItemsSource = Type;
            cbType.SelectedIndex = 0;
            cbSize.ItemsSource = Size;
            cbSize.SelectedIndex = 0;

        }

        private void rbEnRumble_Click(object sender, RoutedEventArgs e)
        {
            Useable(rbEnMemPack, false);
        }

        private void rbEnMemPack_Click(object sender, RoutedEventArgs e)
        {
            Useable(rbEnRumble, false);
        }

        private void rbDisMemPack_Click(object sender, RoutedEventArgs e)
        {
            Useable(rbEnRumble, true);
        }

        private void rbDisRumble_Click(object sender, RoutedEventArgs e)
        {
            Useable(rbEnMemPack, true);
        }

        private void Useable(RadioButton thing, bool canUse)
        {
            thing.IsEnabled = canUse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = $@"{ Directory.GetCurrentDirectory() }\{tbGN.Text} [{ cbRegion.SelectedItem.ToString()}].ini";
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine($";{tbGN.Text} {cbRegion.SelectedItem.ToString()}");
            sw.WriteLine("[RomOption]");
            sw.WriteLine($"BackupType = {cbType.SelectedIndex}");
            sw.WriteLine($"BackupSize = {cbSize.SelectedItem.ToString()}");
            if (rbEnVSync.IsChecked == true)
            {
                sw.WriteLine($"RetraceByVsync = 1");
            }
            if (rbEnTimer.IsChecked == true)
            {
                sw.WriteLine($"UseTimer = 1");
            }
            if(rbEnTBoot.IsChecked == true)
            {
                sw.WriteLine($"TrueBoot = 1");

            }
            if(rbEnExpansion.IsChecked == true)
            {
                sw.WriteLine("RamSize = 0x800000");
            }
            if(rbEnRumble.IsChecked == true)
            {
                sw.WriteLine("Rumble = 1");
            }
            else if(rbEnMemPack.IsChecked == true)
            {
                sw.WriteLine("MemPack = 1");
            }
            sw.Close();
            string argument = "/select, \"" + path + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
    }
}
