using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace easyWhatsAppToVMsg
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string regex = @"(\d{2}\.\d{2}\.\d{4} \d{2}\:\d{2}\:\d{2})\: ([^\:]*)\: (.*)";
        const int current_output_max_standard = 200;

        const string debug_source_folder = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Alte Whatsapp";
        const string debug_target_folder = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Alte Whatsapp";
        const string debug_address_file = @"D:\_Mikolai\Meine Dokumente\__Backups\__Neues Handy\Contacts_Mobile_only.csv";
        const string debug_my_name = @"Mikolai Gütschow";

        Dictionary<string, string> addressbook_dict = new Dictionary<string, string>();
        string addressbook_current_name = "";
        string addressbook_current_tel = "";

        StringBuilder current_output = new StringBuilder();
        int current_output_count = 0;
        int current_output_max = current_output_max_standard;
        string current_output_file;
        int current_output_idx = 0;

        public MainWindow()
        {
            InitializeComponent();

            source_folder.Text = debug_source_folder;
            target_folder.Text = debug_target_folder;
            address_file.Text = debug_address_file;
            my_name.Text = debug_my_name;
            replace_emojis.IsChecked = true;
        }


        public string encodeMessage(string m) {
            StringBuilder s = new StringBuilder();
            QPEncoder qp = new QPEncoder(s);
            if ((bool)replace_emojis.IsChecked)
                qp.write(Emoticons.replaceEmojis(m.ToCharArray()));
            else
                qp.write(Encoding.UTF8.GetBytes(m));
            return s.ToString();
        }

        public void appendSMS(string number, bool sent, DateTime date, string message) {
            current_output.AppendLine("BEGIN:VMSG");
            current_output.AppendLine("VERSION: 1.1");
            current_output.AppendLine("BEGIN:VCARD");
            current_output.AppendLine("TEL:" + number);
            current_output.AppendLine("END:VCARD");
            current_output.AppendLine("BEGIN:VBODY");
            current_output.AppendLine("X-BOX:" + (sent ? "SENDBOX" : "INBOX"));
            current_output.AppendLine("X-READ:READ");
            current_output.AppendLine("X-SIMID:0");
            current_output.AppendLine("X-LOCKED:UNLOCKED");
            current_output.AppendLine("X-TYPE:SMS");
            current_output.AppendLine("Date:" + date.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture));
            current_output.AppendLine("Subject;ENCODING=QUOTED-PRINTABLE;CHARSET=UTF-8:" + message);
            current_output.AppendLine("END:VBODY");
            current_output.AppendLine("END:VMSG");

            current_output_count++;

            if (current_output_count == current_output_max) {
                //save to vmsg-file
                saveToVMSG();
                current_output.Clear();
                current_output_count = 0;
                current_output_idx++;
            }
        }

        public void saveToVMSG() {
            StreamWriter txt = new StreamWriter(current_output_file + "_" + current_output_idx + ".vmsg", false, Encoding.ASCII);
            txt.Write(current_output.ToString());
            txt.Close();
        }

        private void source_browse_Click(object sender, RoutedEventArgs e) {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            switch (dialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = dialog.SelectedPath;
                    source_folder.Text = file;
                    source_folder.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    source_folder.Text = null;
                    source_folder.ToolTip = null;
                    break;
            }
        }

        private void address_browse_Click(object sender, RoutedEventArgs e) {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            switch (dialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = dialog.FileName;
                    address_file.Text = file;
                    address_file.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    address_file.Text = null;
                    address_file.ToolTip = null;
                    break;
            }
        }

        private void target_browse_Click(object sender, RoutedEventArgs e) {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            switch (dialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = dialog.SelectedPath;
                    target_folder.Text = file;
                    target_folder.ToolTip = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    target_folder.Text = null;
                    target_folder.ToolTip = null;
                    break;
            }
        }

        private void max_number_sms_standard_Click(object sender, RoutedEventArgs e) {
            max_number_sms.Text = current_output_max_standard.ToString();
        }

        private void start_Click(object sender, RoutedEventArgs e) {
            current_output.Clear();
            current_output_count = 0;
            current_output_idx = 0;

            var myself = my_name.Text;
            if (String.IsNullOrEmpty(myself)) {
                MessageBox.Show("Bitte gib deinen Namen genau so an, wie er in den TXT-Dateien auftaucht!");
                return;
            }

            try {
                current_output_max = Int32.Parse(max_number_sms.Text);
            } catch {
                MessageBox.Show("Die maximale Anzahl an Nachrichten sollte nur Zahlen enthalten!");
                return;
            }

            var addressbook = address_file.Text;
            addressbook_dict.Clear();
            if (File.Exists(addressbook)) {
                string[] lines = File.ReadAllLines(addressbook, Encoding.UTF8);

                if (lines.Length == 0)
                    MessageBox.Show("In der Adressbuch-Datei wurden keine Zeilen gefunden! Es wird nach jeder Nummer einzeln gefragt.\nDatei: " + addressbook);

                for (int i = 0; i < lines.Length; i++) {
                    var line = lines[i];
                    Match match = Regex.Match(line, "([^,]*),([^,]*)");
                    if (!match.Success) {
                        MessageBox.Show("Die Zeile " + (i + 1) + " der Adressbuch-Datei hat nicht das richtige Format! Diese Zeile wurde nicht eingelesen.\nDatei: " + addressbook + "\nZeile: " + line);
                        continue;
                    }

                    var n = match.Groups[1].Value;
                    var t = match.Groups[2].Value;

                    if (!String.IsNullOrEmpty(t)) {
                        if (addressbook_dict.ContainsKey(n))
                            MessageBox.Show("Der Name " + n + " ist laut Adressbuch-Datei mehrdeutig!\n Das kann zu erheblichen Problemen beim Import führen. Die Zeile " + (i+1) + " wird beim Einlesen ignoriert.\nDatei: " + addressbook + "\nZeile: " + line);
                        else
                            addressbook_dict.Add(n, t);
                    }
                }

                if (addressbook_dict.Count == 0)
                    MessageBox.Show("In der Adressbuch-Datei wurden keine Einträge gefunden! Es wird nach jeder Nummer einzeln gefragt.\nDatei: " + addressbook);

            } else
                MessageBox.Show("Die gewählte Adressbuch-Datei wurde nicht gefunden. Es wird nach jeder Nummer einzeln gefragt.\nAdressbuch-Datei: " + addressbook);

            var target = target_folder.Text;
            if (target.EndsWith("\\"))
                target = target.Substring(0, target.Length - 2);
            current_output_file = target + "\\WhatsApp_messages";
            if (!Directory.Exists(target)) {
                MessageBox.Show("Der gewählte Pfad des Zielordners ist ungültig!\nZielordner: " + target);
                return;
            }

            var source = source_folder.Text;
            if (!Directory.Exists(source)) {
                MessageBox.Show("Der gewählte Pfad des Quellordners ist ungültig!\nQuellordner: " + source);
                return;
            }

            var files = Directory.GetFiles(source, "*.txt");
            if (files.Length == 0) {
                MessageBox.Show("Im gewählten Quellordner wurden keine TXT-Dateien gefunden!\nQuellordner: " + source);
                return;
            }

            foreach (var file in files) {
                string message = "";
                DateTime date = new DateTime();
                bool sent = false;

                string[] lines = File.ReadAllLines(file);

                if (lines.Length == 0) {
                    MessageBox.Show("In der TXT-Datei wurden keine Zeilen gefunden!\nDatei: " + file);
                    continue;
                }

                var name = "";
                //find first line with not sent!
                foreach (var line in lines) {
                    Match match = Regex.Match(line, regex);
                    if (!match.Success)
                        continue;

                    name = match.Groups[2].Value;
                    if (name != myself) {
                        if (!addressbook_dict.ContainsKey(name)) {
                            string number = "";
                            System.Windows.Forms.DialogResult dr = System.Windows.Forms.DialogResult.Cancel;
                            while (dr == System.Windows.Forms.DialogResult.Cancel)
                                dr = InputBox.Show("", "\"" + name + "\" als Gesprächspartner gefunden.\nBitte geben Sie die entsprechende Telefonnummer ein (mit Ländervorwahl und ohne Leerzeichen, Bsp. +4917678959947).", ref number);
                            while (!Regex.IsMatch(number, @"\+\d{7,15}"))
                                InputBox.Show("", "\"" + name + "\" als Gesprächspartner gefunden.\nDie Telefonnummer scheint ungültig zu sein. Versuchen Sie es bitte erneut!", ref number);

                            addressbook_dict.Add(name, number);
                        }
                        break;
                    }
                } //TODO: what if no message from anotherone?!!

                addressbook_current_name = name;
                addressbook_current_tel = addressbook_dict[name];

                bool error = false;
                for (int i=0; i<lines.Length; i++) {
                    var line = lines[i];

                    Match match = Regex.Match(line, regex);
                    if (!match.Success) {
                        if (i == 0) {
                            MessageBox.Show("Die (erste Zeile der) TXT-Datei hat nicht das erwartete Format!\nDatei: " + file);
                            error = true;
                            break;
                        }
                        //add to previous message!
                        message += "\r\n" + encodeMessage(line);
                        continue;
                    } else if (i > 0) {
                        //found new message > save old one to result
                        appendSMS(addressbook_current_tel, sent, date, message);
                    }
                    var d = match.Groups[1].Value;
                    var n = match.Groups[2].Value;
                    var m = match.Groups[3].Value;

                    date = DateTime.ParseExact(d, "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal);
                
                    message = encodeMessage(m);

                    sent = n == myself;

                    if (!sent && n != addressbook_current_name) {
                        MessageBox.Show("Unbekannter Name gefunden: " + n + "\nWird abgebrochen!");
                        return;
                    }
                }
                if (!error) {
                    //save last message to result
                    appendSMS(addressbook_current_tel, sent, date, message);
                }
            }

            saveToVMSG();
        }
    }
}
