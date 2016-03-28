using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace easyWhatsAppToVMsg
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //const string regex = @"(\d{2}\.\d{2}\.\d{4} \d{2}\:\d{2}\:\d{2})\: ([^\:]*)\: (.*)";

        const string debug_source_folder = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Alte Whatsapp";
        const string debug_target_folder = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Alte Whatsapp";
        const string debug_address_file = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Alte Whatsapp\Contacts_Mobile_only.csv";
        const string debug_my_name = @"Mikolai Gütschow";

        /*Dictionary<string, string> addressbook_dict = new Dictionary<string, string>();
        string addressbook_current_name = "";
        string addressbook_current_tel = "";*/

        VMSG export = new VMSG();

        private ObservableCollection<Thread> _threads = new ObservableCollection<Thread>();
        public ObservableCollection<Thread> Threads { get { return _threads; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            //enable thread filtering
            ICollectionView threads_view = CollectionViewSource.GetDefaultView(_threads);
            threads_view.Filter = delegate (object obj)
            {
                if (String.IsNullOrEmpty(threads_filter.Text))
                    return true;

                var thread = (Thread)obj;
                return thread.Name.IndexOf(threads_filter.Text, 0, StringComparison.InvariantCultureIgnoreCase) > -1;
            };
            threads_filter.TextChanged += delegate
            {
                threads_view.Refresh();
            };

            //enable message filtering
            threads_list.SelectionChanged += delegate
            {
                if (!(threads_list.SelectedItem is Thread))
                    return;

                ICollectionView messages_view = CollectionViewSource.GetDefaultView(((Thread)threads_list.SelectedItem).Messages);
                messages_view.Filter = delegate(object obj)
                {
                    if (String.IsNullOrEmpty(messages_filter.Text))
                        return true;

                    var message = (Message)obj;
                    return message.Content.IndexOf(messages_filter.Text, 0, StringComparison.InvariantCultureIgnoreCase) > -1;
                };
                messages_filter.TextChanged += delegate
                {
                    messages_view.Refresh();
                };
                messages_filter_goto.Click += delegate
                {
                    messages_filter.Text = "";
                    messages_list.ScrollIntoView(messages_list.SelectedItem);
                };
            };
            

            source_folder.Text = debug_source_folder;
            target_folder.Text = debug_target_folder;
            address_file.Text = debug_address_file;
            my_name.Text = debug_my_name;
            replace_emojis.IsChecked = true;
        }




        private void source_browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = source_folder.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = dialog.SelectedPath;
                source_folder.Text = file;
                source_folder.ToolTip = file;
            }
        }

        private void address_browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.FileName = address_file.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = dialog.FileName;
                address_file.Text = file;
                address_file.ToolTip = file;
            }
        }

        private void target_browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = target_folder.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = dialog.SelectedPath;
                target_folder.Text = file;
                target_folder.ToolTip = file;
            }
        }

        private void max_number_sms_standard_Click(object sender, RoutedEventArgs e)
        {
            max_number_sms.Text = VMSG.current_output_max_standard.ToString();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Thread.replaceEmojis = replace_emojis.IsChecked == true;

            Thread.MyName = my_name.Text;
            if (String.IsNullOrEmpty(Thread.MyName))
            {
                MessageBox.Show("Bitte gib deinen Namen genau so an, wie er in den TXT-Dateien auftaucht!");
                return;
            }

            var source = source_folder.Text;
            if (!Directory.Exists(source))
            {
                MessageBox.Show("Der gewählte Pfad des Quellordners ist ungültig!\nQuellordner: " + source);
                return;
            }

            var files = Directory.GetFiles(source, "*.txt");
            if (files.Length == 0)
            {
                MessageBox.Show("Im gewählten Quellordner wurden keine TXT-Dateien gefunden!\nQuellordner: " + source);
                return;
            }

            Thread.importFormat = import_format.SelectedIndex;
            if (Thread.importFormat == -1)
            {
                MessageBox.Show("Bitte wählen Sie ein Import-Format aus!");
                return;
            }

            _threads.Clear();
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);
                if (lines.Length == 0)
                {
                    MessageBox.Show("In der TXT-Datei wurden keine Zeilen gefunden!\nDatei: " + file);
                    continue;
                }

                Thread thread = new Thread(file, lines);
                _threads.Add(thread);
            }
        }


        private void export_Click(object sender, RoutedEventArgs e)
        {
            var target = target_folder.Text;
            if (!Directory.Exists(target))
            {
                MessageBox.Show("Der gewählte Pfad des Zielordners ist ungültig!\nZielordner: " + target);
                return;
            }
            
            int output_max = VMSG.current_output_max_standard;
            try
            {
                output_max = Int32.Parse(max_number_sms.Text);
            }
            catch
            {
                MessageBox.Show("Die maximale Anzahl an Nachrichten sollte nur Zahlen enthalten!");
                return;
            }

            var addressbook = address_file.Text;
            if (!File.Exists(addressbook))
                MessageBox.Show("Die gewählte Adressbuch-Datei wurde nicht gefunden. Es wird nach jeder Nummer einzeln gefragt.\nAdressbuch-Datei: " + addressbook);

            VMSG.Start(output_max, target, addressbook, _threads.ToArray());

            MessageBox.Show("Alle Einzelchats wurden erfolgreich exportiert!");
        }
    }
}
