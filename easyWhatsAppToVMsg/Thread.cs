using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace easyWhatsAppToVMsg
{
    public class Thread
    {
        public static string MyName;
        public static bool replaceEmojis = false;

        /* importFormat > used for Regex
         * 0 = Windows Phone
         * 1 = Android
         */
        public static int importFormat = -1;

        //TODO: as array for multiple languages!
        private static string regexDuoName = @"WhatsApp Chat mit ((.*)-\d|.*)(-\d)?\.txt";
        private static string regexGroupName = @"WhatsApp Chat ((.*)-\d|.*)(-\d)?\.txt";

        private static string[] parseDate = new string[]
        {
            @"dd.MM.yyyy HH:mm:ss", //WP
            @"dd.MM.yyyy, HH:mm" //Android
        };
        private static string[] regexMessageLine = new string[]
        {
            @"(\d{2}\.\d{2}\.\d{4} \d{2}\:\d{2}\:\d{2})\: ([^\:]*)\: (.*)", //WP
            @"(\d{2}\.\d{2}\.\d{4}, \d{2}\:\d{2}) - ([^\:]*)\: (.*)" //Android
        };

        private static string[][] regexMetaLine = new string[][]
        {
            //WP
            new string[]
            {
                //group created
                ".* hat diese Gruppe erstellt",
                "Du hast diese Gruppe erstellt",
                ".* hat Gruppe \".*\" erstellt",
                "Du hast die Gruppe \".*\" erstellt",
                //group ended
                ".* hat die Gruppe aufgelöst",
                "Du hast die Gruppe aufgelöst",
                //user added
                ".* wurde hinzugefügt",
                "Du wurdest hinzugefügt",
                ".* hat .* hinzugefügt",
                "Du hast .* hinzugefügt",
                //user removed
                ".* wurde entfernt",
                "Du wurdest entfernt",
                ".* hat ‪.* entfernt",
                "Du hast .* entfernt",
                //user number changed
                ".* geändert zu .*",
                //change subject
                ".* hat den Betreff zu \".*\" geändert",
                "Du hast den Betreff zu \".*\" geändert",
                //change group icon
                ".* hat das Gruppenbild geändert",
                "Du hast das Gruppenbild geändert",
                //remove group icon
                ".* hat das Gruppenbild gelöscht",
                "Du hast das Gruppenbild gelöscht"
                //TODO: hier fehlt garantiert noch was!
            },
            //Android
            new string[]
            {
                //group created
                ".* hat die Gruppe \".*\" erstellt",
                "Du hast die Gruppe \".*\" erstellt",
                //group ended
                ".* hat diese Gruppe aufgelöst",
                "Du hast diese Gruppe aufgelöst",
                //user added
                ".* wurde hinzugefügt",
                "Du wurdest hinzugefügt",
                ".* hat .* hinzugefügt",
                "Du hast .* hinzugefügt",
                //user removed
                ".* wurde entfernt",
                "Du wurdest entfernt",
                ".* hat ‪.* entfernt",
                "Du hast .* entfernt",
                //user number changed to
                ".* hat zu .* gewechselt",
                //subject changed
                ".* hat den Betreff zu \".*\" geändert",
                "Du hast den Betreff zu \".*\" geändert",
                //group icon changed
                ".* hat das Gruppenbild geändert",
                "Du hast das Gruppenbild geändert",
                //group icon removed
                "Gruppenbild wurde gelöscht",
                ".* hat das Gruppenbild gelöscht",
                "Du hast das Gruppenbild gelöscht"
                //TODO: hier fehlt garantiert noch was!
            }
        };

        private string _name;
        private string _path;
        private bool _loaded = false;
        private string[] _lines;
        private bool _isGroup = false;
        private List<Message> _messages = new List<Message>();

        public Thread(string path, string[] lines)
        {
            _path = path;
            _lines = lines;

            var s = path.Substring(_path.LastIndexOf('\\')+1);
            Match match = Regex.Match(s, regexDuoName);
            if (!match.Success)
            {
                match = Regex.Match(s, regexGroupName);
                if (match.Success)
                    _isGroup = true;
            }

            if (!match.Success)
                ;
            else if (String.IsNullOrEmpty(match.Groups[2].Value))
                _name = match.Groups[1].Value;
            else //case of {name}-1.txt
                _name = match.Groups[2].Value;
        }

        public string Name { get { return _name; } }
        public string Path { get { return _path; } }
        public bool IsGroup { get { return _isGroup; } }

        public List<Message> Messages
        {
            get
            {
                if (!_loaded)
                    LoadAllMessages();
                return _messages;
            }
        }

        private void LoadAllMessages()
        {
            string content = "";
            DateTime date = new DateTime();
            string name = "";
            Message.MessageType type = Message.MessageType.META;

            string debug_log = "";

            bool error = false;
            for (int i = 0; i < _lines.Length; i++)
            {
                var line = _lines[i];

                Match match = Regex.Match(line, regexMessageLine[importFormat]);
                if (!match.Success && _isGroup)
                {
                    //only relevant for group thread
                    bool found = false;
                    for (int j = 0; j < regexMetaLine.Length; j++)
                    {
                        match = Regex.Match(line, regexMetaLine[importFormat][j]);
                        if (match.Success)
                        {
                            _messages.Add(Message.MetaMessage(line));
                            type = Message.MessageType.META;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        continue;
                    else
                        debug_log += line;
                }

                if (!match.Success)
                {
                    if (i == 0)
                    {
                        MessageBox.Show("Die (erste Zeile der) TXT-Datei hat nicht das erwartete Format!\nDatei: ");
                        error = true;
                        break;
                    }
                    //add to previous message!
                    if (type == Message.MessageType.META)
                        MessageBox.Show("Es scheint ein Fehler in der Datei vorzuliegen. An dieser Stelle dürfte definitiv keine Nachricht weitergehen! Die Zeile wird ignoriert.\nDatei: " + _path + " (Zeile " + (i + 1) + ")\nZeile: " + line);
                    else
                        content += "\r\n" + Emoticons.replaceEmojis(line);
                    continue;
                }

                //found new message > save old one to result
                if (type != Message.MessageType.META)
                    _messages.Add(Message.NormalMessage(type, content, date, name));

                var d = match.Groups[1].Value;
                var n = match.Groups[2].Value;
                var m = match.Groups[3].Value;

                date = DateTime.ParseExact(d, parseDate[importFormat], CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal);
                content = Emoticons.replaceEmojis(m);
                name = n;

                if (name == MyName)
                    type = Message.MessageType.SENT;
                else
                    type = Message.MessageType.RECEIVED;
            }
            if (!error)
            {
                //save last message to result
                _messages.Add(Message.NormalMessage(type, content, date, name));
            }

            _loaded = true;
        }
    }
}

