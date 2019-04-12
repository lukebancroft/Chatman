using SQLite;
using System;
using System.Collections.Generic;

namespace Chatman
{
    public class Database
    {
        private static Database dbInstance;
        public static SQLiteConnection connection = new SQLiteConnection("MsgDatabase.db5");

        private Database() { }

        public static Database DbInstance
        {
            get
            {
                if (dbInstance == null)
                {
                    dbInstance = new Database();
                    connection.CreateTable<Message>();
                    connection.CreateTable<User>();
                }
                return dbInstance;
            }
        }

        public SQLiteConnection Connection { get => connection; }

        public void clearDB()
        {
            Connection.Execute("DELETE FROM USER");
            Connection.Execute("DELETE FROM MESSAGE");
        }

        public List<User> getAllUsers() => Connection.Query<User>("SELECT * FROM USER");

        public List<User> getAllContacts() => Connection.Query<User>("SELECT * FROM USER WHERE ISCURRENTUSER = false");

        public List<Message> getAllMessages() => Connection.Query<Message>("SELECT * FROM MESSAGE");

        public User getUserById(int id)
        {
             return Connection.Query<User>("SELECT * FROM USER WHERE ID = ?", id)[0];
        }

        public User getUserByNickname(string nickname)
        {
            return Connection.Query<User>("SELECT * FROM USER WHERE NICKNAME = ?", nickname)[0];
        }

        public User getCurrentUser()
        {
            return Connection.Query<User>("SELECT * FROM USER WHERE ISCURRENTUSER = true")[0];
        }

        public bool checkUserExists(string nickname)
        {
            return (connection.ExecuteScalar<int>("SELECT count(*) FROM USER WHERE NICKNAME = ?", new Object[] { nickname }) == 1) ? true : false;
        }

        public void createNewUser(string nickName, string token, bool isCurrentUser)
        {
            Connection.Query<User>("INSERT INTO USER (NICKNAME, TOKEN, ISCURRENTUSER) " +
                "VALUES (?, ?, ?)", new Object[] { nickName, token, isCurrentUser });
        }

        public void createNewMessage(string body, int sender_id, int receiver_id, DateTime date)
        {
            Connection.Query<Message>("INSERT INTO MESSAGE (BODY, SENDER, RECEIVER, DATE) " +
                "VALUES (?, ?, ?, ?)", new Object[] { body, sender_id, receiver_id, date });
        }
    }
}
