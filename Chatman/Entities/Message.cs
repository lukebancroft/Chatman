using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Chatman
{
    public class Message
    {
        private int id;
        private string body;
        private int sender;
        private int receiver;
        private DateTime date;

        public Message() { }

        public Message(int id, string body, int sender, int receiver, DateTime date)
        {
            this.id = id;
            this.body = body;
            this.sender = sender;
            this.receiver = receiver;
            this.date = date;
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [MaxLength(255)]
        public string Body
        {
            get => body;
            set => body = value;
        }

        [ForeignKey(typeof(User))]
        public int Sender
        {
            get => sender;
            set => sender = value;
        }
        
        [ForeignKey(typeof(User))]
        public int Receiver
        {
            get => receiver;
            set => receiver = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }
    }
}