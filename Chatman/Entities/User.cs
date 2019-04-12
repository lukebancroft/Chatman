using SQLite;
using System;

namespace Chatman
{
    public class User
    {
        [Save]
        private int id;
        [Save]
        private string nickname;
        [Save]
        private string token;
        [Save]
        private bool isCurrentUser;

        public User() { }

        public User(int id, string nickname, string token, bool isCurrentUser)
        {
            this.id = id;
            this.nickname = nickname;
            this.token = token;
            this.isCurrentUser = isCurrentUser;
        }

        public override string ToString()
        {
            return "User" + Id;
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => id;
            set => id = value;
        }

        [MaxLength(50)]
        public string Nickname
        {
            get => nickname;
            set => nickname = value;
        }

        public string Token
        {
            get => token;
            set => token = value;
        }

        public bool IsCurrentUser
        {
            get => isCurrentUser;
            set => isCurrentUser = value;
        }
    }
}