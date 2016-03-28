using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyWhatsAppToVMsg
{
    public class Message
    {
        private MessageType _type;
        private string _content;
        
        private DateTime _date;
        private string _contact = "Ich"; //only for group chats

        private Message(MessageType type, string content, DateTime date, string contact)
        {
            _type = type;
            _content = content;
            _date = date;
            if (type == MessageType.RECEIVED)
                _contact = contact;
        }

        public static Message NormalMessage(MessageType type, string content, DateTime date, string contact)
        {
            return new Message(type, content, date, contact);
        }
        public static Message MetaMessage(string content)
        {
            return new Message(MessageType.META, content, DateTime.MinValue, "");
        }

        public MessageType Type { get { return _type; } }
        public string Content { get { return _content; } }
        public DateTime UTCDate { get { return _date; } }
        public DateTime Date { get { return _date.ToLocalTime(); } }
        public string Contact { get { return _contact; } }

        public enum MessageType
        {
            RECEIVED,
            SENT,
            META
        }
    }
}
