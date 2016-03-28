using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace easyWhatsAppToVMsg
{
    class VMSG
    {
        public const int current_output_max_standard = 200;

        static Dictionary<string, string> addressbook = new Dictionary<string, string>();
        static StringBuilder current_output = new StringBuilder();
        static int current_output_count = 0;
        static int current_output_max = current_output_max_standard;
        static string current_output_file;
        static int current_output_idx = 0;

        public static void Start(int output_max, string targetFolder, string addressbookFile, Thread[] threads)
        {
            addressbook.Clear();
            string[] lines = File.ReadAllLines(addressbookFile, Encoding.UTF8);

            if (lines.Length == 0)
                MessageBox.Show("In der Adressbuch-Datei wurden keine Zeilen gefunden! Es wird nach jeder Nummer einzeln gefragt.\nDatei: " + addressbook);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                Match match = Regex.Match(line, "([^,]*),([^,]*)");
                if (!match.Success)
                {
                    MessageBox.Show("Die Zeile " + (i + 1) + " der Adressbuch-Datei hat nicht das richtige Format! Diese Zeile wurde nicht eingelesen.\nDatei: " + addressbook + "\nZeile: " + line);
                    continue;
                }

                var n = match.Groups[1].Value;
                var t = match.Groups[2].Value;

                if (!String.IsNullOrEmpty(t))
                {
                    if (addressbook.ContainsKey(n))
                        MessageBox.Show("Der Name " + n + " ist laut Adressbuch-Datei mehrdeutig!\n Das kann zu erheblichen Problemen beim Import führen. Die Zeile " + (i + 1) + " wird beim Einlesen ignoriert.\nDatei: " + addressbook + "\nZeile: " + line);
                    else
                        addressbook.Add(n, t);
                }
            }

            if (addressbook.Count == 0)
                MessageBox.Show("In der Adressbuch-Datei wurden keine Einträge gefunden! Es wird nach jeder Nummer einzeln gefragt.\nDatei: " + addressbook);

            current_output.Clear();
            current_output_count = 0;
            current_output_idx = 0;
            current_output_max = output_max;

            var target = targetFolder;
            if (target.EndsWith("\\"))
                target = target.Substring(0, target.Length - 2);
            current_output_file = target + "\\WhatsApp_messages";

            foreach (var thread in threads)
            {
                if (thread.IsGroup)
                    continue;

                if (!addressbook.ContainsKey(thread.Name))
                {
                    string number = "";
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.DialogResult.Cancel;
                    while (dr != System.Windows.Forms.DialogResult.OK)
                        dr = InputBox.Show("", "\"" + thread.Name + "\" als Gesprächspartner gefunden.\nBitte geben Sie die entsprechende Telefonnummer ein (mit Ländervorwahl und ohne Leerzeichen, Bsp. +4917678959947).", ref number);
                    while (!Regex.IsMatch(number, @"\+\d{7,15}"))
                        InputBox.Show("", "\"" + thread.Name + "\" als Gesprächspartner gefunden.\nDie Telefonnummer scheint ungültig zu sein. Versuchen Sie es bitte erneut!", ref number);

                    addressbook.Add(thread.Name, number);
                }

                foreach (var message in thread.Messages)
                {
                    current_output.AppendLine("BEGIN:VMSG");
                    current_output.AppendLine("VERSION: 1.1");
                    current_output.AppendLine("BEGIN:VCARD");
                    current_output.AppendLine("TEL:" + addressbook[thread.Name]);
                    current_output.AppendLine("END:VCARD");
                    current_output.AppendLine("BEGIN:VBODY");
                    current_output.AppendLine("X-BOX:" + (message.Type == Message.MessageType.SENT ? "SENDBOX" : "INBOX"));
                    current_output.AppendLine("X-READ:READ");
                    current_output.AppendLine("X-SIMID:0");
                    current_output.AppendLine("X-LOCKED:UNLOCKED");
                    current_output.AppendLine("X-TYPE:SMS");
                    current_output.AppendLine("Date:" + message.UTCDate.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture));
                    current_output.AppendLine("Subject;ENCODING=QUOTED-PRINTABLE;CHARSET=UTF-8:" + encodeMessage(message.Content));
                    current_output.AppendLine("END:VBODY");
                    current_output.AppendLine("END:VMSG");

                    current_output_count++;

                    if (current_output_count == current_output_max)
                    {
                        //save to vmsg-file
                        saveToVMSG();
                        current_output.Clear();
                        current_output_count = 0;
                        current_output_idx++;
                    }
                }
            }

            saveToVMSG();
        }

        private static string encodeMessage(string m)
        {
            StringBuilder s = new StringBuilder();
            QPEncoder qp = new QPEncoder(s);
            qp.write(Encoding.UTF8.GetBytes(m));
            return s.ToString();
        }

        private static void saveToVMSG()
        {
            StreamWriter txt = new StreamWriter(current_output_file + "_" + current_output_idx + ".vmsg", false, Encoding.ASCII);
            txt.Write(current_output.ToString());
            txt.Close();
        }
    }
}
